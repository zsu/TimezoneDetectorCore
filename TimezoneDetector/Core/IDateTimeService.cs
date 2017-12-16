using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimezoneDetector
{
    public interface IDateTimeService
    {
        string ToClientTime(DateTime dt);
        string ToClientTime(DateTime dt, string format);
        string ToClientTime(DateTime dt, bool withTimeZoneInfo, string format);
        string GetClientTimeZone();
    }
}
