using Markdig;
using System.Web;
using System.Web.Mvc;

namespace Blog40.Utilities
{
    public static class HtmlExtensions
    {
        public static IHtmlString FromMarkdown(this HtmlHelper helper, string markdown)
        {
            string html = Markdown.ToHtml(markdown);
            return MvcHtmlString.Create(html);
        }
    }
}