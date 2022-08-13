
using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Core.Services
{
    public interface IProductService : IService<Product>
    {
        Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategory(); // MVC'de DTO dönmek uygun değildi. CustomResponse dönüyorduk. Eski haline getirdik. 
    }
}
