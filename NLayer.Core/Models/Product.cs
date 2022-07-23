using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } //Uygulamada null hatasını almamak için default olarak nullable geliyor . Reference tiplerde geliyor.

        public int Stock { get; set; } //Value tip olduğu için default değerleri olduğu için nullable olmuyor.

        public decimal Price { get; set; }

        public int CategoryId { get; set; } //foreign key Category_Id Fr Core bunu foreign key algılamaz. O yüzden isimlendirme yaparken birleşik yazdık. 

        public Category Category { get; set; }

        public ProductFeature ProductFeature { get; set; } //Navigation

    }
}
