using System;
using Microsoft.AspNet.Mvc.Rendering;

namespace DemoKCDC.Middleware
{
    public static class InjectionHelpers
    {
        public static HtmlString Inject(this IHtmlHelper html)
        {
            var someGuid = Guid.NewGuid().ToString();
            html.ViewContext.HttpContext.Items.Add("injected", someGuid);
            return new HtmlString(someGuid);
        }
    }
}