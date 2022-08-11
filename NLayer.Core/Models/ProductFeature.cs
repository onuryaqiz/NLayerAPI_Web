namespace NLayer.Core.Models
{
    public class ProductFeature //Product'a bağlı olduğu için BaseEntity'yi miras olarak almadık.
    {

        public int Id { get; set; }

        public string Color { get; set; }
        public int Height { get; set; }

        public int Width { get; set; }

        public int ProductId { get; set; } //foreign key

        public Product Product { get; set; }

    }
}
