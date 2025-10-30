using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        [TempData] public string Message { get; set; }
        public InventorySearchModel SearchModel;
        public List<InventoryViewModel> Inventory;
        public SelectList Products;

        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;

        public IndexModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _productApplication = productApplication;
            _inventoryApplication = inventoryApplication;
        }

        [NeedsPermission(InventoryPermissions.ListInventories)]
        public void OnGet(InventorySearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Inventory = _inventoryApplication.Search(searchModel);
        }

        [NeedsPermission(InventoryPermissions.CreateInventory)]
        public IActionResult OnGetCreate()
        {
            var RegisterModel = new CreateInventory
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", RegisterModel);
        }

        [NeedsPermission(InventoryPermissions.CreateInventory)]
        public JsonResult OnPostCreate(CreateInventory RegisterModel)
        {
            var result = _inventoryApplication.Create(RegisterModel);
            return new JsonResult(result);
        }

        [NeedsPermission(InventoryPermissions.EditInventory)]
        public IActionResult OnGetEdit(long id)
        {
            var inventory = _inventoryApplication.GetDetails(id);
            inventory.Products = _productApplication.GetProducts();
            return Partial("Edit", inventory);
        }

        [NeedsPermission(InventoryPermissions.EditInventory)]
        public JsonResult OnPostEdit(EditInventory RegisterModel)
        {
            var result = _inventoryApplication.Edit(RegisterModel);
            return new JsonResult(result);
        }

        [NeedsPermission(InventoryPermissions.IncreaseInventory)]
        public IActionResult OnGetIncrease(long id)
        {
            var RegisterModel = new IncreaseInventory()
            {
                InventoryId = id
            };
            return Partial("Increase", RegisterModel);
        }

        [NeedsPermission(InventoryPermissions.IncreaseInventory)]
        public JsonResult OnPostIncrease(IncreaseInventory RegisterModel)
        {
            var result = _inventoryApplication.Increase(RegisterModel);
            return new JsonResult(result);
        }

        [NeedsPermission(InventoryPermissions.ReduceInventory)]
        public IActionResult OnGetReduce(long id)
        {
            var RegisterModel = new ReduceInventory()
            {
                InventoryId = id
            };
            return Partial("Reduce", RegisterModel);
        }

        [NeedsPermission(InventoryPermissions.ReduceInventory)]
        public JsonResult OnPostReduce(ReduceInventory RegisterModel)
        {
            var result = _inventoryApplication.Reduce(RegisterModel);
            return new JsonResult(result);
        }

        [NeedsPermission(InventoryPermissions.OperationLog)]
        public IActionResult OnGetLog(long id)
        {
            var log = _inventoryApplication.GetOperationLog(id);
            return Partial("OperationLog", log);
        }
    }
}