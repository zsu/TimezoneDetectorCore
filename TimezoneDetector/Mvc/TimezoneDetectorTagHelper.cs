using System;
using System.Text;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Hosting;

namespace TimezoneDetector
{
    [HtmlTargetElement("timezonedetector")]
    public class TimezoneDetectorTagHelper : TagHelper
    {
        private IUrlHelperFactory _urlHelper;
        private IActionContextAccessor _actionContextAccessor;
        public TimezoneDetectorTagHelper(IUrlHelperFactory urlHelper, IActionContextAccessor actionContextAccessor)
        {
            _urlHelper = urlHelper;
            _actionContextAccessor = actionContextAccessor;
         }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }
            output.TagName = null;
            var builder = output.PostElement;
            builder.Clear();
            builder.AppendHtml(ToString());
        }
        private string RenderScript()
		{
			StringBuilder scripts = new StringBuilder();
            var scriptPath = _urlHelper.GetUrlHelper(_actionContextAccessor.ActionContext).Content("~");
            scripts.AppendFormat("<script type='text/javascript' src='{0}/timezonedetector/Mvc/Scripts/TimezoneDetector{1}.js'></script>", scriptPath, ".min");

            return scripts.ToString();
		}
		public override string ToString()
		{
			var content = new StringBuilder();
			content.AppendLine(RenderScript());
			return content.ToString();
		}
		public string ToHtmlString()
		{
			return ToString();
		}
    }
}