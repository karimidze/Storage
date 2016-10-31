using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Storage.Helpers
{
    public static class FormatStringHelper
    {
        public static IHtmlString FormatNewLines(this HtmlHelper helper, string input)
        {
            if (input.Length > 30)
            {
                return helper.Raw(helper.Encode(input).Substring(0, 30)+"(...)");
            }
            else

                return helper.Raw(helper.Encode(input));
        }
    }
}