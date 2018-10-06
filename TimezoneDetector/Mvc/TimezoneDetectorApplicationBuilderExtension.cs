using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TimezoneDetector
{
    public static class TimezoneDetectorApplicationBuilderExtension
    {
        public static IApplicationBuilder UseTimezoneDetector(this IApplicationBuilder app) => UseTimezoneDetector(app, null);

        public static IApplicationBuilder UseTimezoneDetector(this IApplicationBuilder app, Action<Options> setupOptions)
        {
            var options = new Options();
            setupOptions?.Invoke(options);
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = $"/{options.RoutePrefix}",
                FileProvider = new EmbeddedFileProvider(typeof(TimezoneDetectorApplicationBuilderExtension).GetTypeInfo().Assembly, "TimezoneDetector")
            });

            return app;
        }

        public class Options
        {
            internal Options() { }

            public string RoutePrefix { get; set; } = "timezonedetector";
        }
    }
}
