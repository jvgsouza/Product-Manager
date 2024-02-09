using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Usuario.API.Middleware
{
    [ExcludeFromCodeCoverage]
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var log = await GetLogData(context);

            Log.Logger.Information(log.ToString()!);

            await _next(context);
        }

        private async static Task<object> GetLogData(HttpContext context)
        {
            var request = context.Request;
            var body = "";
            if (request.Method == HttpMethods.Post && request.ContentLength > 0)
            {
                request.EnableBuffering();
                var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                await request.Body.ReadAsync(buffer, 0, buffer.Length);
                body = Encoding.UTF8.GetString(buffer);

                request.Body.Position = 0;
            }

            var requestJson = new
            {
                context.Request.QueryString,
                context.Request.Query,
                context.Request.Protocol,
                context.Request.PathBase,
                context.Request.Path,
                context.Request.Method,
                context.Request.RouteValues,
                context.Request.Host,
                Body = body,
                context.Request.Headers,
                Date = DateTime.UtcNow
            };

            return requestJson;
        }
    }
}
