using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class LogoutModel : PageModel
    {

        private readonly IAccountApplication _accountApplication;

        public LogoutModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }
        public IActionResult OnGet()
        {
            _accountApplication.Logout();
            return RedirectToPage("/Login");
        }
    }
}
