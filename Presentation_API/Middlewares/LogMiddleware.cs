
namespace Presentation_API.Middlewares
{
    public class LogMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("İstek geldi");


            await next(context);

            Console.WriteLine("İşlem bitti");
        }
    }
}
