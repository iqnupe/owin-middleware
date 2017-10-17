using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevUpMiddleware.Devup
{
    public static class InjectionHelpers
    {
        public static HtmlString Inject(this IHtmlHelper<dynamic> html)
        {
            var someGuid = Guid.NewGuid().ToString();
            html.ViewContext.HttpContext.Items.Add("injected", someGuid);
            return new HtmlString(someGuid);
        }
    }
}