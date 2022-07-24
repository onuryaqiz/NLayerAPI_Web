using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{

    public class ProductsController : CustomBaseController // Business kod yani Service kodu buraya Best Practice için yazılmayacak. Olabildiğince temiz olmalı.
    {
        // Controller sadece servisleri bilir. Kesinlikle repository miras almayacak.
        private readonly IMapper _mapper;
        private readonly IService<Product> _service;
        private readonly IProductService _productService;
        public ProductsController(IService<Product> service, IMapper mapper, IProductService productService)
        {
            _service = service;
            _mapper = mapper;
            _productService = productService;
        }

        // GET api/products/GetProductsWithCategory , 2 adet Get olunca Framework hata fırlatmasın diye metodun ismini belirttik.
        [HttpGet("GetProductsWithCategory")] //"[action]" da yapabiliriz. Metodun ismini action çağıracak. 
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return CreateActionResult(await _productService.GetProductWithCategory()); // Controller içerisinde action metodlarda minimum kod bulundurduk.
        }







        // GET api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();

            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList()); // IEnumerable yani bir Entity döneceği için mapper'ı da ekledik. // Generic olduğu için DTO'yu burada ekledik.

            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));

            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }
        // Örnek : api/products/5 olan data gelir.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);

            var productsDtos = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDtos));


        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto)); // Dto geldiği için product'u productDto 'ya dönüştür.
            var productsDtos = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDtos)); // Update işlemi gerçekleştiği için 201 durum kodu verdik. 200 de olabilir. Fakat Best Practice için 201 daha uygun.

        }

        [HttpPut]

        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productDto)); // Geriye bir şey dönülmediği için var product ve Map kaldırıldı.

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); // T Data almadığı için NoContentDto eklendi.


        }
        // DELETE api/products/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }
    }
}
