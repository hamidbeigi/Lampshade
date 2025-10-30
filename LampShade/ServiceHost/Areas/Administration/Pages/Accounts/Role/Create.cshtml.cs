using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public CreateRole RegisterModel { get; set; }

        [TempData]
        public string Message { get; set; }

        private readonly IRoleApplication _roleApplication;

        public CreateModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        [NeedsPermission(AccountPermissions.CreateRole)]
        public void OnGet()
        {
        }

        [NeedsPermission(AccountPermissions.CreateRole)]

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = _roleApplication.Create(RegisterModel);
            if (result.IsSucceded)
                return RedirectToPage("Index");

            ModelState.AddModelError("RegisterModel.Name", result.Message);
            return Page();
        }
    }
}