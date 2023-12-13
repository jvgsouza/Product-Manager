using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;
using System.Text;

namespace Usuario.API.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<LogMiddleware> _logger;

        public LogMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
            _logger = _serviceProvider.GetService<ILogger<LogMiddleware>>()!;
        }

        public async Task Invoke(HttpContext context, ILogger<LogMiddleware> logger)
        {
            var log = await GetLogData(context);

            logger.LogInformation(log.ToString()!);

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
