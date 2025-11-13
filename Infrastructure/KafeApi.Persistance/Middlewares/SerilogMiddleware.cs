using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeApi.Persistance.Middlewares
{
    public  class SerilogMiddleware
    {
        private readonly RequestDelegate _next;

        public SerilogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();//zamanlayıcı başlat

            var request = context.Request;
            var ip = context.Connection.RemoteIpAddress?.ToString();

            Log.Logger.Information("Incoming Request: {Method} {Path} from {IP}",
                request.Method,
                request.Path,
                ip);

            try
            {
                await _next(context);
                sw.Stop();
                Log.Logger.Information("Outgoing Response:  responded {StatusCode} in {ElapsedMilliseconds}ms",
                    
                    context.Response.StatusCode,
                    sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                sw.Stop();
                Log.Logger.Error(ex, "Hata. Sure  {ElapsedMilliseconds}ms",sw.ElapsedMilliseconds);
                throw;
            }
        }
    }
}
