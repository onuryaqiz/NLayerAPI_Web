using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public abstract class BaseDto
    {

        public int Id { get; set; } //EntityId yerine sadece Id yazdık. FrCore Primary Key olarak algılamıyor. Custom isim yerine FrCore anlayacağı şekilde yazmak gerekiyor.
        public DateTime CreatedDate { get; set; }
    }
}
