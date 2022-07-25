using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Filters
{
    // Eğer bir attribute'a bir filter'a constructor'ında parametre geçiyorsak () ile kullanamıyoruz. ServiceFilter üzerinden kullanmamız gerekiyor.
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity // Dinamik olacak o yüzden <T> aldık
    {

        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service) // Eğer bir filter construstor tarafında herhangi bir class ve interface DI olarak geçiyorsa , program.cs'de eklemek gerekiyor.
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) // next herhangi bir filter'a takılmıyorsa next ile devam edecek.
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();

            if (idValue == null)
            {
                await next.Invoke(); // Devam et
                return;
            }
            var id = (int)idValue;
            var anyEntity = await _service.AnyAsync(x => x.Id == id);

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name}({id}) not found "));
        }
    }
}
