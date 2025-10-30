using System.Collections.Generic;
using _0_Framework.Infrastructure;

namespace ShopManagement.Configuration.Permissions
{
    public class ShopPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Product", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListProducts, "ListProducts"),
                        new PermissionDto(ShopPermissions.CreateProduct, "CreateProduct"),
                        new PermissionDto(ShopPermissions.EditProduct, "EditProduct")
                    }
                },
                {
                    "ProductCategory", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListProductCategories, "ListProductCategories"),
                        new PermissionDto(ShopPermissions.CreateProductCategory, "CreateProductCategory"),
                        new PermissionDto(ShopPermissions.EditProductCategory, "EditProductCategory")
                    }
                },
                {
                    "ProductPicture", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListProductPictures, "ListProductPictures"),
                        new PermissionDto(ShopPermissions.CreateProductPicture, "CreateProductPicture"),
                        new PermissionDto(ShopPermissions.EditProductPicture, "EditProductPicture"),
                        new PermissionDto(ShopPermissions.RemoveProductPicture, "RemoveProductPicture"),
                        new PermissionDto(ShopPermissions.RestoreProductPicture, "RestoreProductPicture")
                    }
                },
                {
                     "Slide", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListSlides, "ListSlides"),
                        new PermissionDto(ShopPermissions.CreateSlide, "CreateSlide"),
                        new PermissionDto(ShopPermissions.EditSlide, "EditSlide"),
                        new PermissionDto(ShopPermissions.RemoveSlide, "RemoveSlide"),
                        new PermissionDto(ShopPermissions.RestoreSlide, "RestoreSlide")
                    }
                },
                {
                     "Order", new List<PermissionDto>
                     {
                        new PermissionDto(ShopPermissions.ListOrders, "ListOrders"),
                        new PermissionDto(ShopPermissions.ConfirmOrder, "ConfirmOrder"),
                        new PermissionDto(ShopPermissions.CancelOrder, "CancelOrder"),
                        new PermissionDto(ShopPermissions.ListItemsOrders, "ListItemsOrders")
                     }
                }
            };
        }
    }
}