using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Discounts.CustomerDiscounts
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public CustomerDiscountSearchModel SearchModel;
        public List<CustomerDiscountViewModel> CustomerDiscounts;
        public SelectList Products;

        private readonly IProductApplication _productApplication;
        private readonly ICustomerDiscountApplication _customerDiscountApplication;

        public IndexModel(IProductApplication ProductApplication, ICustomerDiscountApplication customerDiscountApplication)
        {
            _productApplication = ProductApplication;
            _customerDiscountApplication = customerDiscountApplication;
        }

        [NeedsPermission(DiscountPermissions.ListCustomerDiscounts)]
        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            CustomerDiscounts = _customerDiscountApplication.Search(searchModel);
        }

        [NeedsPermission(DiscountPermissions.CreateCustomerDiscount)]
        public IActionResult OnGetCreate()
        {
            var RegisterModel = new DefineCustomerDiscount
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", RegisterModel);
        }

        [NeedsPermission(DiscountPermissions.CreateCustomerDiscount)]
        public JsonResult OnPostCreate(DefineCustomerDiscount RegisterModel)
        {
            var result = _customerDiscountApplication.Define(RegisterModel);
            return new JsonResult(result);
        }

        [NeedsPermission(DiscountPermissions.EditCustomerDiscount)]
        public IActionResult OnGetEdit(long id)
        {
            var customerDiscount = _customerDiscountApplication.GetDetails(id);

            customerDiscount.Products = _productApplication.GetProducts();
            return Partial("Edit", customerDiscount);
        }

        [NeedsPermission(DiscountPermissions.EditCustomerDiscount)]
        public JsonResult OnPostEdit(EditCustoemrDiscount RegisterModel)
        {
            var result = _customerDiscountApplication.Edit(RegisterModel);
            return new JsonResult(result);
        }
    }
}