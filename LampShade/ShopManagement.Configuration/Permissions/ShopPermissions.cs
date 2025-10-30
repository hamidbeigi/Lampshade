namespace ShopManagement.Configuration.Permissions
{
    public static class ShopPermissions
    {
        //Product
        public const int ListProducts = 10;
        public const int CreateProduct = 11;
        public const int EditProduct = 12;


        //ProductCategory
        public const int ListProductCategories = 20;
        public const int CreateProductCategory = 21;
        public const int EditProductCategory = 22;

        //ProductPicture
        public const int ListProductPictures = 30;
        public const int CreateProductPicture = 31;
        public const int EditProductPicture = 32;
        public const int RemoveProductPicture = 33;
        public const int RestoreProductPicture = 34;

        //Slide
        public const int ListSlides = 40;
        public const int CreateSlide = 41;
        public const int EditSlide = 42;
        public const int RemoveSlide = 43;
        public const int RestoreSlide = 44;

        //Order
        public const int ListOrders = 50;
        public const int ConfirmOrder = 51;
        public const int CancelOrder = 52;
        public const int ListItemsOrders = 53;
    }
}