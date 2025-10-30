using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;

        public LoginModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public Login Login { get; set; }
        public string ReturnUrl { get; set; }

        public IActionResult OnGet(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Index");
            }

            ReturnUrl = returnUrl ?? Url.Content("~/");
            return Page();
        }
        public IActionResult OnPost(string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return Page();

            var result = _accountApplication.Login(Login);
            if (!result.IsSucceded)
            {
                Message = result.Message;
                return Page();
            }

            returnUrl ??= Url.Content("~/");

            // امنیت: فقط ریدایرکت داخلی مجاز است
            if (!Url.IsLocalUrl(returnUrl))
                return RedirectToPage("/Index");

            return LocalRedirect(returnUrl);
        }
    }
}
