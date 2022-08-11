using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using System.Data.Entity;
using System.Linq.Expressions;

namespace NLayer.Caching
{
    public class ProductServiceWithCaching : IProductService // decreater design pattern'a yakın proxy design pattern'a benzer . Implementasyon da farklılık olan design pattern'lardır.
    {
        private const string CacheProductKey = "productsCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache = null, IProductRepository productRepository = null, IUnitOfWork unitOfWork = null)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;


            if (!_memoryCache.TryGetValue(CacheProductKey, out _)) // geriye boolean döner. Sadece true/false olduğunu öğreniyoruz. Data var mı yok mu onu öğrenmeye çalışıyoruz.Cache'deki datayı almak istemiyoruz.
            {
                _memoryCache.Set(CacheProductKey, _productRepository.GetProductWithCategory().Result);
            }
        }
        // Open-closed prensibine uygun caching yapıyoruz.
        public async Task<Product> AddAsync(Product entity)
        {
            await _productRepository.AddAsync(entity); // Çok sık erişeceğimiz ama çok sık güncellemeyeceğimiz data en iyi caching'dir.
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();

            return entity;

        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _productRepository.AddRangeAsync(entities); // Çok sık erişeceğimiz ama çok sık güncellemeyeceğimiz data en iyi caching'dir.
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();

            return entities;
        }

        public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync() // List Ienumerable Interface'ini implemente ettiği için List yerine IEnumerable yazdık. 
        {
            var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
            return Task.FromResult(products); // Burada cache döndük.
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                throw new NotFoundException($"{typeof(Product).Name}({id}) not found");
            }
            return Task.FromResult(product); // await kullanmıyoruz. Static method üzerinden Task.FromResult dönüyoruz.
        }

        public Task<List<ProductWithCategoryDto>> GetProductWithCategory()
        {
            var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey); // Burada ise , DTO ve customResponse istediği için döndük. 


            var productsWithCategoryDto = _mapper.Map<List<ProductWithCategoryDto>>(products);

            return Task.FromResult(productsWithCategoryDto); // Geriye bir Task durumlarda , yani async dönmemiz gereken durumlarda, ama method içerisinde await kullanmadığımızda Task.FromResult kullanırız. 
        }

        public async Task RemoveAsync(Product entity)
        {
            _productRepository.Remove(entity);
            await _unitOfWork.CommitAsync();

            await CacheAllProductsAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            _productRepository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            _productRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable(); // EFCore'dan değil cache'den bu sorguyu çekiyoruz.
        }

        public async Task CacheAllProductsAsync()
        {
            _memoryCache.Set(CacheProductKey, await _productRepository.GetAll().ToListAsync());
        }
    }
}
