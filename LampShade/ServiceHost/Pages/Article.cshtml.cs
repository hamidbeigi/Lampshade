using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using CommentManagement.Application.Contracts.Comment;
using CommnetManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        public ArticleQueryModel Article;
        public List<ArticleQueryModel> LatestArticles;
        public List<ArticleCategoryQueryModel> ArticleCategories;
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly ICommentApplication _commentApplication;

        public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery articleCategoryQuery, ICommentApplication commentApplication)
        {
            _articleQuery = articleQuery;
            _commentApplication = commentApplication;
            _articleCategoryQuery = articleCategoryQuery;
        }

        [BindProperty]
        public AddComment Comment { get; set; }

        public IActionResult OnGet(string id)
        {
            Article = _articleQuery.GetArticleDetails(id);

            if (Article == null)
            {
                // مقاله پیدا نشد یا منتشر نشده، مثلا ریدایرکت به صفحه 404
                return RedirectToPage("/NotFound", new { message = ".مقاله مورد نظر یافت نشد یا هنوز منتشر نشده است" });
            }

            LatestArticles = _articleQuery.LatestArticles();
            ArticleCategories = _articleCategoryQuery.GetArticleCategories();

            return Page();
        }

        public IActionResult OnPost(string articleSlug)
        {
            Comment.Type = CommentType.Article;


            if (!ModelState.IsValid)
            {
                Article = _articleQuery.GetArticleDetails(articleSlug);
                LatestArticles = _articleQuery.LatestArticles();
                ArticleCategories = _articleCategoryQuery.GetArticleCategories();

                return Page();
            }

            _commentApplication.Add(Comment);
            return RedirectToPage("/Article", new { Id = articleSlug });
        }
    }
}