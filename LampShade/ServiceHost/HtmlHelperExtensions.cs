using DNTPersianUtils.Core;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace ServiceHost
{
    public static class HtmlHelperExtensions
    {
        // فقط فارسی کردن اعداد متن ساده (بدون HtmlAgilityPack)
        public static IHtmlContent FaNumbersPlainText(this IHtmlHelper htmlHelper, object input)
        {
            if (input == null)
                return HtmlString.Empty;

            var str = input.ToString();
            return new HtmlString(str.ToPersianNumbers());
        }

        // فارسی کردن اعداد داخل متن حاوی HTML (با HtmlAgilityPack)
        public static IHtmlContent FaNumbersHtml(this IHtmlHelper htmlHelper, object input)
        {
            if (input == null)
                return HtmlString.Empty;

            var str = input.ToString();

            if (str.Contains("<") && str.Contains(">"))
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(str);

                foreach (var node in doc.DocumentNode.DescendantsAndSelf())
                {
                    if (node.NodeType == HtmlNodeType.Text)
                        node.InnerHtml = node.InnerHtml.ToPersianNumbers();
                }

                return new HtmlString(doc.DocumentNode.InnerHtml);
            }

            // اگر رشته ساده بود، فقط تبدیل کن
            return new HtmlString(str.ToPersianNumbers());
        }

        // تبدیل تاریخ شمسی + فارسی کردن اعدادش
        public static IHtmlContent FaPersianDate(this IHtmlHelper htmlHelper, DateTime date)
        {
            var persianDate = date.ToShortPersianDateString().ToPersianNumbers();
            return new HtmlString(persianDate);
        }
    }
}