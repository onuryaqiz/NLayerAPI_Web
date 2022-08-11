using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductWithCategory() //Tek bir metodun gelmesinin sebebi : GenericRepository'yi miras aldık ve sadece IProductRepository'deki metod geldi.
        {
            // Eager loading : Include metodu ile Eager loading ile daha datayı çekerken category'lerin alınmasını istedik.
            // Lazy loading  : Product'a bağlı category'i de ihtiyaç olduğunda çekersek lazy loading .
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }
    }
}
