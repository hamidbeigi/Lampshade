using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader,
            IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = GenerateSlug.Slugify(command.Slug);

            //var categorySlug = _productCategoryRepository.GetSlugById(command.CategoryId);
            var path = $"products/{slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            // امن سازی توضیحات قبل از ذخیره (HtmlSanitizer)
            var sanitizedDescription = HtmlSanitizerUtils.SanitizeHtml(command.Description);

            var product = new Product(command.Name, command.Code,
                command.ShortDescription, sanitizedDescription, picturePath, command.
                PictureAlt, command.PictureTitle, command.KeyWords, command.MetaDescription, slug, command.CategoryId);

            _productRepository.Create(product);
            _productRepository.SaveChanges();

            return operation.Succeded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();

            var product = _productRepository.GetProductWithCategory(command.Id);
            if (product == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_productRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = GenerateSlug.Slugify(command.Slug);

            string picturePath;

            if (command.Picture == null)
            {
                // اگر عکس جدید انتخاب نشده، عکس قبلی رو نگه دار
                picturePath = product.Picture;
            }
            else
            {
                var path = $"products/{slug}";
                picturePath = _fileUploader.Upload(command.Picture, path);
            }

            // امن سازی توضیحات قبل از ذخیره (از HtmlSanitizer)
            var sanitizedDescription = HtmlSanitizerUtils.SanitizeHtml(command.Description);

            product.Edit(command.Name, command.Code,
                command.ShortDescription, sanitizedDescription, picturePath, command.
                PictureAlt, command.PictureTitle, command.KeyWords, command.MetaDescription, slug, command.CategoryId);

            _productRepository.SaveChanges();

            return operation.Succeded();
        }

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }
    }
}
