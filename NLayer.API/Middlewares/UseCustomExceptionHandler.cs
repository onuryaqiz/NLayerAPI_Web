using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTOs;
using NLayer.Service.Exceptions;
using System.Text.Json;

namespace NLayer.API.Middlewares
{
    public static class UseCustomExceptionHandler // Extension method yazabilmek için class static olmak zorundadır.
    {
        public static void UseCustomException(this IApplicationBuilder app) // program.cs'deki var app=builder.Build();'deki app IApplicationBuilder.

        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context => // sonlandırıcı middleware

                {
                    context.Response.ContentType = "application/json";

                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();


                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400, // client kaynaklı hata ise 400 atıyoruz. 
                        _ => 500 // başka bir hata ise 500 dön .
                    };
                    context.Response.StatusCode = statusCode;


                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response)); // Kendi middleware'miz olduğu için serialize işlemi yapıyoruz. 

                });

            });
        }

    }
}
