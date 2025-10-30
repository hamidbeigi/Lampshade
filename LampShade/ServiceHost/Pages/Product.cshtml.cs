using _01_LampshadeQuery.Contracts.Product;
using CommentManagement.Application.Contracts.Comment;
using CommnetManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        public ProductQueryModel Product;
        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;
        public ProductModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        [BindProperty]
        public AddComment Comment { get; set; }

        public void OnGet(string id)
        {
            
            Product = _productQuery.GetProductDetails(id);
        }

        public IActionResult OnPost(string productSlug)
        {
            //RegisterModel.Type = CommentType.Product;

            //    var result = _commentApplication.Add(RegisterModel);
            //    return RedirectToPage("/Product", new { Id = productSlug });

            Comment.Type = CommentType.Product;

            if (!ModelState.IsValid)
            {
                Product = _productQuery.GetProductDetails(productSlug);
                return Page(); // دوباره نمایش فرم با پیام خطا
            }

            _commentApplication.Add(Comment);
            return RedirectToPage("/Product", new { Id = productSlug });
        }
    }
}
