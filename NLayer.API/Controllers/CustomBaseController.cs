using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction] // Endpoint olmadığı için Swagger'ın Endpoint olarak algılamaması için NoAction ekledik.
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204) // 204 : NoContent HTTP durum kodu. 
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode// Status 200 ise 200 , 404 dönüyorsa geriye 404 döneceğiz.
            };
        }
    }
}
