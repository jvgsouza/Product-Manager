using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Usuario.Infra.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
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
                QueryString = context.Request.QueryString,
                Query = context.Request.Query,
                Protocol = context.Request.Protocol,
                PathBase = context.Request.PathBase,
                Path = context.Request.Path,
                Method = context.Request.Method,
                RouteValues = context.Request.RouteValues,
                Host = context.Request.Host,
                Body = body,
                Headers = context.Request.Headers
            };

            await _next(context);
        }
    }
}
