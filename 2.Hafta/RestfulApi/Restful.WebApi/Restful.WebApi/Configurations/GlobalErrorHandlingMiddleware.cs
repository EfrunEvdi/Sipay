using Restful.WebApi.Exceptions;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using UnauthorizedAccessException = Restful.WebApi.Exceptions.UnauthorizedAccessException;

namespace Restful.WebApi.Configurations
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        public static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            var stackTrace = string.Empty;
            string message;

            var exceptionType = exception.GetType();

            if (exceptionType == typeof(BadRequestException))
            {
                message = exception.Message;
                status = HttpStatusCode.BadRequest;
                stackTrace = exception.StackTrace;
            }

            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                status = HttpStatusCode.Unauthorized;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }

            else if (exceptionType == typeof(NotFoundException))
            {
                message = exception.Message;
                status = HttpStatusCode.NotFound;
                stackTrace = exception.StackTrace;
            }

            else
            {
                status = HttpStatusCode.InternalServerError;
                message = exception.Message;
                stackTrace = exception.StackTrace;
            }

            var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(exceptionResult);
        }

        // Bu GlobalErrorHandlingMiddleware sınıfı, ASP.NET Core uygulamalarında genel hata yönetimi işlevselliği sağlar. Bu ara yazılım bileşeni, gelen istekleri işlerken oluşan hataları yakalar ve istemciye uygun bir hata yanıtı döndürür.

        //Middleware, ASP.NET Core HTTP taleplerinin işlendiği bir boru hattıdır ve her bir middleware, gelen talebi alır, üzerinde işlemler yapar ve sonraki middleware'e aktarır. GlobalErrorHandlingMiddleware, talebi sonraki middleware'e iletmek için _next örneği kullanır.Ancak, eğer bir hata oluşursa, hatayı işlemek için HandleExceptionAsync yöntemini çağırır.


        // HandleExceptionAsync yöntemi, hata türüne göre uygun bir HTTP durum kodu ve hata yanıtı oluşturur. Örneğin, eğer yakalanan hata bir BadRequestException ise, HTTP durum kodu BadRequest olarak ayarlanır ve istemciye hata mesajı ve stack trace bilgileri içeren bir JSON yanıtı döndürülür.Aynı şekilde, UnauthorizedAccessException ve NotFoundException gibi belirli hata türleri için de uygun HTTP durum kodları ve yanıtlar oluşturulur.


        // Sonuç olarak, bu middleware, uygulamanızda oluşan hataları merkezi bir şekilde ele alır ve istemcilere uygun hata yanıtları sağlar. Bu, hata işleme ve raporlama sürecini basitleştirir ve kodunuzun tekrar etmesini önler.
    }
}
