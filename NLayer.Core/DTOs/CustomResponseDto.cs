using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; } // T datası aldığımız için T data alıyoruz.

        public List<string> Errors { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; } // Client'ın bilmesi gerekmiyor. O yüzden JsonIgnore ekledik.

        public static CustomResponseDto<T> Success(int statusCode, T data) // Geriye yeni nesneler dönecek. Fail veya error'da nesne dönecek. Static o yüzden yaptık.

        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Data = data };
        }

        public static CustomResponseDto<T> Success(int statusCode) // Static factory method. Hangi sınıf dönülmesi isteniyorsa static methodlar tanımlayarak instance'lar dönüyoruz.
                                                                   // nesne üretmek olayını bu sınıf içerisinde gerçekleştiyoruz. Ayrı class ve Interface'ler yerine bu şekilde kolayca yapabiliriz.
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };

        }

        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors) // Birden fazla hata olabilir diye string dönüyor.
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errors };
        }

        public static CustomResponseDto<T> Fail(int statusCode, string error) // geriye bir error dönüyor.
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { error } };
        }
    }
}
