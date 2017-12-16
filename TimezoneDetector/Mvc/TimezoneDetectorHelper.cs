using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimezoneDetector
{
	public static class TimezoneDetectorHelper
	{
		public static TimezoneDetectorControl TimezoneDetector(this IHtmlHelper helper)
		{
			return new TimezoneDetectorControl();
		}
	}
}
