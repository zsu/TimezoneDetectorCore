using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TimezoneDetector
{
    public static class TimezoneDetectorServiceCollectionExtensions
    {

        public static IServiceCollection AddTimezoneDetector(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            var col = services.AddSingleton<ITimezoneDetectorService, TimezoneDetectorService>();
            //if(services.FirstOrDefault(d => d.ServiceType == typeof(IHttpContextAccessor))==null)
            //    col = services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return col;
        }
    }
}
