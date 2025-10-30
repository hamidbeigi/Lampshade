using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Configuration.Permissions;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public ProductPictureSearchModel SearchModel;
        public List<ProductPictureViewModel> ProductPictures;
        public SelectList Products;

        private readonly IProductApplication _productApplication;
        private readonly IProductPictureApplication _productPictureApplication;
        public IndexModel(IProductApplication ProductApplication, IProductPictureApplication productPictureApplication)
        {
            _productApplication = ProductApplication;
            _productPictureApplication = productPictureApplication;
        }

        [NeedsPermission(ShopPermissions.ListProductPictures)]
        public void OnGet(ProductPictureSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            ProductPictures = _productPictureApplication.Search(searchModel);
        }

        [NeedsPermission(ShopPermissions.CreateProductPicture)]
        public IActionResult OnGetCreate()
        {
            var RegisterModel = new CreateProductPicture
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", RegisterModel);
        }

        [NeedsPermission(ShopPermissions.CreateProductPicture)]
        public JsonResult OnPostCreate(CreateProductPicture RegisterModel)
        {
            var result = _productPictureApplication.Create(RegisterModel);
            return new JsonResult(result);
        }

        [NeedsPermission(ShopPermissions.EditProductPicture)]
        public IActionResult OnGetEdit(long id)
        {
            var productPicture = _productPictureApplication.GetDetails(id);
            productPicture.Products = _productApplication.GetProducts();
            return Partial("Edit", productPicture);
        }

        [NeedsPermission(ShopPermissions.EditProductPicture)]
        public JsonResult OnPostEdit(EditProductPicture RegisterModel)
        {
            var result = _productPictureApplication.Edit(RegisterModel);
            return new JsonResult(result);
        }

        [NeedsPermission(ShopPermissions.RemoveProductPicture)]
        public IActionResult OnGetRemove(long id)
        {
            var result = _productPictureApplication.Remove(id);
            if (result.IsSucceded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        [NeedsPermission(ShopPermissions.RestoreProductPicture)]
        public IActionResult OnGetRestore(long id)
        {
            var result = _productPictureApplication.Restore(id);
            if (result.IsSucceded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}