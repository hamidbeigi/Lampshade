using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountApplication _accountApplication;

        public RegisterModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public RegisterAccount Register { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPostRegister()
        {
            //if (string.IsNullOrWhiteSpace(Register.Password))
            //{
            //    ModelState.AddModelError("", "رمز عبور الزامی است");
            //    return Page();
            //}

            if (!ModelState.IsValid)
                return Page();

            var result = _accountApplication.Register(Register);
            if (result.IsSucceded)
                return RedirectToPage("/Login");

            Message = result.Message;
            return Page();
        }
    }
}