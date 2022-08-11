namespace NLayer.Core.DTOs
{
    public class ProductWithCategoryDto : ProductDto
    {
        // Repository'ler geriye entity dönerken , Servisler direkt olarak API'ın isteyeceği DTO'yu oto olarak dönüyor.
        public CategoryDto Category { get; set; }

    }
}
