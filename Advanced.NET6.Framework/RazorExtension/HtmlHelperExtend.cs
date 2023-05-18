using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.NET6.Framework.RazorExtension
{
    public static class HtmlHelperExtend
    {
        /// <summary>
        /// 换行
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static IHtmlContent Br(this IHtmlHelper helper)
        {
            return new HtmlString("</Br>");  //Html字符串
        }

        /// <summary>
        /// submit提交表单
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="value"></param>
        /// <param name="defaultClass"></param>
        /// <returns></returns>
        public static IHtmlContent Submit(this IHtmlHelper helper, string value, string defaultClass = "btn btn-default")
        {
            return new HtmlString($"<input type='submit' value='{value}' class='{defaultClass}'/>");
        }

        /// <summary>
        /// 图片展示扩展
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="href"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static HtmlString Image(this IHtmlHelper helper, string src, string text = null)
        {
            return new($"<img src='{src}' title='{text}'/> ");
        }
    }

    //可以生成一个 Hello全新的控件
    [HtmlTargetElement("Hello")] //特性：标识控件的名称；
    public class RichardTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "p";
            output.Attributes.Add("id", context.UniqueId);
            output.PreContent.SetContent("Hello  欢迎来到.Net高级班的VIP课程；");
            output.PostContent.SetContent($"time is now: { DateTime.Now.ToString("HH:mm")}");
        }
    }

    [HtmlTargetElement("divInfo", Attributes = "theme")]
    public class ButtonGroupThemeTagHelper : TagHelper
    {
        public string Theme { get; set; }

        //public string Theme1 { get; set; }
        //public string Theme2 { get; set; }
        //public string Theme3 { get; set; }

        public override void Process(TagHelperContext context,
            TagHelperOutput output)
        {
            output.Attributes.SetAttribute("style", "background-color:red;height:60px;width:100px");
            context.Items["theme"] = Theme;
        }
    }

    [HtmlTargetElement("button", ParentTag = "divInfo")]
    [HtmlTargetElement("a", ParentTag = "divInfo")]
    public class ButtonThemeTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context.Items.ContainsKey("theme"))
                output.Attributes.SetAttribute("class", $"btn btn-{context.Items["theme"]}");
        }
    }
}
