using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public AccountSearchModel SearchModel;
        public List<AccountViewModel> Accounts;
        public SelectList Roles;

        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;


        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        [NeedsPermission(AccountPermissions.ListAccounts)]
        public void OnGet(AccountSearchModel searchModel)
        {
            Roles = new SelectList(_roleApplication.List(), "Id", "Name");
            Accounts = _accountApplication.Search(searchModel);
        }

        [NeedsPermission(AccountPermissions.CreateAccount)]
        public IActionResult OnGetCreate()
        {
            var RegisterModel = new RegisterAccount
            {
                Roles = _roleApplication.List()
            };
            return Partial("./Create", RegisterModel);
        }

        [NeedsPermission(AccountPermissions.CreateAccount)]
        public JsonResult OnPostCreate(RegisterAccount RegisterModel)
        {
            var result = _accountApplication.Register(RegisterModel);
            return new JsonResult(result);
        }

        [NeedsPermission(AccountPermissions.EditAccount)]
        public IActionResult OnGetEdit(long id)
        {
            var account = _accountApplication.GetDetails(id);
            account.Roles = _roleApplication.List();
            return Partial("Edit", account);
        }

        [NeedsPermission(AccountPermissions.EditAccount)]
        public JsonResult OnPostEdit(EditAccount RegisterModel)
        {
            var result = _accountApplication.Edit(RegisterModel);
            return new JsonResult(result);
        }

        [NeedsPermission(AccountPermissions.ChangePasswordAccount)]
        public IActionResult OnGetChangePassword(long id)
        {
            var RegisterModel = new ChangePassword { Id = id };
            return Partial("ChangePassword", RegisterModel);
        }
        [NeedsPermission(AccountPermissions.ChangePasswordAccount)]
        public JsonResult OnPostChangePassword(ChangePassword RegisterModel)
        {
            var result = _accountApplication.ChangePassword(RegisterModel);
            return new JsonResult(result);
        }
    }
}