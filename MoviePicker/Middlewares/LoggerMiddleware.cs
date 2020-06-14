using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace MoviePicker.Middlewares
{
    public static class LoggerExtensions
    {
        public static TelemetryEvent GetTelemetryEventDisposable(this ILogger logger, string taskName)
        {
            return new TelemetryEvent(logger, taskName);
        }
    }
    public class TelemetryEvent : IDisposable
    {
        private string _taskName { get; set; }
        private Stopwatch _watch { get; set; }
        private ILogger _logger { get; set; }
        private bool _disposed { get; set; } = false;
        public TelemetryEvent(ILogger logger, string taskName)
        {
            _taskName = taskName;
            _logger = logger;
            _logger.LogInformation($"Started {_taskName}.");
            _watch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _watch.Stop();
                _logger.LogInformation($"Finished {_taskName}. Elapsed {_watch.ElapsedMilliseconds} ms.");
                _disposed = true;
            }

            _logger.LogWarning($"Task event {_taskName} already disposed!");
        }
    }

    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<LoggerMiddleware> logger)
        {
            try
            {
                using var task = (logger).GetTelemetryEventDisposable($"[{context.Request.Method}] {context.Request.Path}");
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e, logger);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            logger.LogError(exception.ToString());
            object payload = null;

            string result = JsonConvert.SerializeObject(new { ok = false, error = exception.Message, payload });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(result);
        }


    }
}