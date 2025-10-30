using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace ServiceHost.Pages
{
    public class NotFoundModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet(string message = null)
        {
            Message = message; // فقط همینه، نه مقدار پیش‌فرض نه چیز دیگه
        }
    }
}
