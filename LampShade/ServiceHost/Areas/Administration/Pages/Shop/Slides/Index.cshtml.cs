using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Configuration.Permissions;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public List<SlideViewModel> Slides;

        private readonly ISlideApplication _slideApplication;

        public IndexModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        [NeedsPermission(ShopPermissions.ListSlides)]
        public void OnGet()
        {
            Slides = _slideApplication.GetList();
        }

        [NeedsPermission(ShopPermissions.CreateSlide)]
        public IActionResult OnGetCreate()
        {
            var RegisterModel = new CreateSlide();
            return Partial("./Create", RegisterModel);
        }

        [NeedsPermission(ShopPermissions.CreateSlide)]
        public JsonResult OnPostCreate(CreateSlide RegisterModel)
        {
            var result = _slideApplication.Create(RegisterModel);
            return new JsonResult(result);
        }

        [NeedsPermission(ShopPermissions.EditSlide)]
        public IActionResult OnGetEdit(long id)
        {
            var slide = _slideApplication.GetDetails(id);
            return Partial("Edit", slide);
        }

        [NeedsPermission(ShopPermissions.EditSlide)]
        public JsonResult OnPostEdit(EditSlide RegisterModel)
        {
            var result = _slideApplication.Edit(RegisterModel);
            return new JsonResult(result);
        }

        [NeedsPermission(ShopPermissions.RemoveSlide)]
        public IActionResult OnGetRemove(long id)
        {
            var result = _slideApplication.Remove(id);
            if (result.IsSucceded)
            {
                Message = "محصول مورد نظر حذف شد.";
                return RedirectToPage("./Index");
            }
            else
            {
                Message = "مشکلی در سیستم رخ داده است.";
                return RedirectToPage("./Index");
            }
        }

        [NeedsPermission(ShopPermissions.RestoreSlide)]
        public IActionResult OnGetRestore(long id)
        {
            var result = _slideApplication.Restore(id);
            if (result.IsSucceded)
            {
                Message = "محصول مورد نظر بازگردانی شد.";
                return RedirectToPage("./Index");
            }
            else
            {
                Message = "مشکلی در سیستم رخ داده است.";
                return RedirectToPage("./Index");
            }
        }
    }
}