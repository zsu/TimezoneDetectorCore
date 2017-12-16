using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NodaTime.TimeZones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace TimezoneDetector
{
    public class TimezoneDetectorService : ITimezoneDetectorService
    {
        public const string COOKIE_KEY_TIME_ZONE_ID = "timezoneid";
        private static IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;
        public TimezoneDetectorService(IHttpContextAccessor httpContextAccessor, ILogger<TimezoneDetectorService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public string ToClientTime(DateTime dt)
        {
            return ToClientTime(dt, true, null);
        }
        public string ToClientTime(DateTime dt, string format)
        {
            return ToClientTime(dt, true, format);
        }
        public string ToClientTime(DateTime dt, bool withTimeZoneInfo, string format)
        {
            string timezoneId = null;
            var context = _httpContextAccessor.HttpContext;
            try
            {
                if (context != null && context.Request != null && context.Request.Cookies != null && context.Request.Cookies.ContainsKey(COOKIE_KEY_TIME_ZONE_ID))
                    timezoneId = WebUtility.UrlDecode(context.Request.Cookies[COOKIE_KEY_TIME_ZONE_ID]);
                string windowsTimezone = GetClientTimeZone();
                if (!string.IsNullOrEmpty(windowsTimezone))
                {
                    var localTime = TimeZoneInfo.ConvertTimeFromUtc(dt, TimeZoneInfo.FindSystemTimeZoneById(windowsTimezone));
                    if (withTimeZoneInfo)
                        return string.Format("{0} {1}", localTime.ToString(format), windowsTimezone);
                    else
                        return localTime.ToString(format);
                }
                //else
                //    _logger.LogError("Invalid timezone {timezoneId}", timezoneId);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Invalid timezone {timezoneId}", timezoneId);
            }

            // if there is no timezoneid in session return the datetime in server timezone
            if (withTimeZoneInfo)
                return string.Format("{0} UTC", dt.ToString(format));
            else
                return dt.ToString(format);
        }

        public string GetClientTimeZone()
        {
            var context = _httpContextAccessor.HttpContext;
            string timezoneId = null;
            try
            {
                if (context != null && context.Request != null && context.Request.Cookies != null && context.Request.Cookies.ContainsKey(COOKIE_KEY_TIME_ZONE_ID))
                    timezoneId = WebUtility.UrlDecode(context.Request.Cookies[COOKIE_KEY_TIME_ZONE_ID]);
                var windowsTimeZone = IanaToWindows(timezoneId);
                if (!string.IsNullOrEmpty(windowsTimeZone))
                {
                    return windowsTimeZone;
                }
            }
            catch (Exception exception)
            {
                //Logger.Log(LogLevel.Error, string.Format("Invalid timezone {0}", timezoneId), exception);
                throw;
            }
            //Logger.Log(LogLevel.Error, string.Format("Invalid timezone {0}", timezoneId));
            return null;
        }
        // This will return the Windows zone that matches the IANA zone, if one exists.
        private string IanaToWindows(string ianaZoneId)
        {
            var tzdbSource = NodaTime.TimeZones.TzdbDateTimeZoneSource.Default;
            var mappings = tzdbSource.WindowsMapping.MapZones;
            var item = mappings.FirstOrDefault(x => x.TzdbIds.Contains(ianaZoneId));
            if (item == null) return null;
            return item.WindowsId;
        }

        // This will return the "primary" IANA zone that matches the given windows zone.
        private static string WindowsToIana(string windowsZoneId)
        {
            // Avoid UTC being mapped to Etc/GMT, which is the mapping in CLDR
            if (windowsZoneId == "UTC")
            {
                return "Etc/UTC";
            }
            var source = TzdbDateTimeZoneSource.Default;
            string result;
            // If there's no such mapping, result will be null.
            source.WindowsMapping.PrimaryMapping.TryGetValue(windowsZoneId, out result);
            // Canonicalize
            if (result != null)
            {
                result = source.CanonicalIdMap[result];
            }
            return result;
        }
    }
}
