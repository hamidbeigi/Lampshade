using Ganss.Xss;
using System;

namespace _0_Framework.Application
{
    public static class HtmlSanitizerUtils
    {
        public static string SanitizeHtml(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return string.Empty;

            var sanitizer = new HtmlSanitizer();

            sanitizer.AllowedTags.Clear();
            sanitizer.AllowedAttributes.Clear();
            sanitizer.AllowedCssProperties.Clear();
            sanitizer.AllowedSchemes.Clear();

            // تگ‌های مجاز - گسترده‌تر
            var allowedTags = new[]
            {
                "p", "br", "strong", "b", "i", "em", "u",
                "ul", "ol", "li", "a", "img",
                "h1", "h2", "h3", "h4", "h5", "h6",
                "blockquote", "div", "span", "hr", "pre", "code",
                "table", "thead", "tbody", "tfoot", "tr", "td", "th",
                "font", "del", "ins", "sup", "sub", "address", "cite", "small",
                "section", "article", "figure", "figcaption",
                "video", "source", "caption"
            };
            foreach (var tag in allowedTags)
                sanitizer.AllowedTags.Add(tag);

            // صفات مجاز
            var allowedAttrs = new[]
            {
                "href", "src", "alt", "title", "style", "class", "id", "name",
                "colspan", "rowspan", "width", "height", "target", "rel",
                "controls", "poster", "preload"
            };
            foreach (var attr in allowedAttrs)
                sanitizer.AllowedAttributes.Add(attr);

            // CSS properties مجاز بیشتر
            var cssProps = new[]
            {
                "color", "background-color", "text-align",
                "font-weight", "font-style", "text-decoration",
                "margin", "padding", "font-size", "line-height",
                "direction", "border", "border-collapse", "border-style",
                "border-width", "vertical-align", "font-family", "background",
                "max-width", "min-width", "max-height", "min-height",
                "display", "float", "position", "overflow", "clear",
                "vertical-align", "text-indent", "letter-spacing", "word-spacing",
                "white-space"
            };
            foreach (var css in cssProps)
                sanitizer.AllowedCssProperties.Add(css);

            // مجاز بودن اسکیماها
            sanitizer.AllowedSchemes.Add("http");
            sanitizer.AllowedSchemes.Add("https");
            sanitizer.AllowedSchemes.Add("mailto");

            // حذف تگ‌ها و صفات خطرناک
            var disallowedTags = new[] { "script", "iframe", "object", "embed", "style", "link", "meta" };
            foreach (var tag in disallowedTags)
                sanitizer.AllowedTags.Remove(tag);

            var disallowedAttrs = new[] { "onload", "onclick", "onerror", "onmouseover", "onfocus" };
            foreach (var attr in disallowedAttrs)
                sanitizer.AllowedAttributes.Remove(attr);

            // جلوگیری از اسکیماهای خطرناک در href و src
            sanitizer.FilterUrl += (sender, args) =>
            {
                var url = args.OriginalUrl?.Trim() ?? "";

                if (string.IsNullOrWhiteSpace(url) ||
                    url.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase) ||
                    url.StartsWith("data:", StringComparison.OrdinalIgnoreCase) ||
                    url.StartsWith("vbscript:", StringComparison.OrdinalIgnoreCase))
                {
                    args.SanitizedUrl = "";
                }
                else
                {
                    args.SanitizedUrl = url;
                }
            };


            return sanitizer.Sanitize(html);
        }
    }
}
