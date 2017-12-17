﻿using Microsoft.AspNetCore.Http;
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
            IServiceCollection col=services;
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (services.FirstOrDefault(d => d.ServiceType == typeof(ITimezoneDetectorService)) == null)
                col = services.AddSingleton<ITimezoneDetectorService, TimezoneDetectorService>();
            if (services.FirstOrDefault(d => d.ServiceType == typeof(IHttpContextAccessor)) == null)
                col = services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return col;
        }
    }
}
