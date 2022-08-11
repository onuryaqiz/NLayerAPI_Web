namespace NLayer.Core.DTOs
{
    public class ProductDto : BaseDto
    {

        public string Name { get; set; } //Uygulamada null hatasını almamak için default olarak nullable geliyor . Reference tiplerde geliyor.

        public int Stock { get; set; } //Value tip olduğu için default değerleri olduğu için nullable olmuyor.

        public decimal Price { get; set; }

        public int CategoryId { get; set; }
    }
}
