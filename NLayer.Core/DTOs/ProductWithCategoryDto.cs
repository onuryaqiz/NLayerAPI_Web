using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class ProductWithCategoryDto : ProductDto
    {
        // Repository'ler geriye entity dönerken , Servisler direkt olarak API'ın isteyeceği DTO'yu oto olarak dönüyor.
        public CategoryDto Category { get; set; }

    }
}
