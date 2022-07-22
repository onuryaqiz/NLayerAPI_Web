using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    public abstract class BaseEntity //nesne örneği alınmasın diye abstract yaptık. 
    {
        public int Id { get; set; } //EntityId yerine sadece Id yazdık. FrCore Primary Key olarak algılamıyor. Custom isim yerine FrCore anlayacağı şekilde yazmak gerekiyor.
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; } //İlk kayıt eklendiğinde null olması lazım . 


    }
}
