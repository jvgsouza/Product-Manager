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

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;

            //ConfigureLogging();
        }

        public async Task Invoke(HttpContext context, ILogger<LogMiddleware> logger)
        {
            ConfigureLogging();
            var log = await GetLogData(context);

            Log.Information(log.ToString()!);
            Log.CloseAndFlush();

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

        private static void ConfigureLogging()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            string con = configuration.GetSection("ElasticConfiguration:Uri").Value!;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(con))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
                })
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
