using System;
using System.Collections.Generic;
using System.Linq;
using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        [TempData]
        public string CartMessage { get; set; }
        public List<CartItem> CartItems;
        public const string CookieName = "cart-items";
        private readonly IProductQuery _productQuery;

        public CartModel(IProductQuery productQuery)
        {
            CartItems = new List<CartItem>();
            _productQuery = productQuery;
        }

        public void OnGet()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];

            var cartItems = string.IsNullOrWhiteSpace(value)
                ? new List<CartItem>()
                : serializer.Deserialize<List<CartItem>>(value);

            cartItems = NormalizeCartItems(cartItems);

            if (!cartItems.Any())
                CartMessage = "سبد خرید شما خالی است.";
            else
                CartItems = _productQuery.CheckInventoryStatus(cartItems);
        }

        public IActionResult OnGetRemoveFromCart(long id)
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            Response.Cookies.Delete(CookieName);
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            var itemToRemove = cartItems.FirstOrDefault(x => x.Id == id);
            cartItems.Remove(itemToRemove);
            var options = new CookieOptions {IsEssential=true, Expires = DateTime.Now.AddDays(2) };
            Response.Cookies.Append(CookieName, serializer.Serialize(cartItems), options);
            return RedirectToPage("/Cart");
        }

        public IActionResult OnGetGoToCheckOut()
        {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];

            var cartItems = string.IsNullOrWhiteSpace(value)
                ? new List<CartItem>()
                : serializer.Deserialize<List<CartItem>>(value);

            cartItems = NormalizeCartItems(cartItems);

            if (!cartItems.Any())
                return RedirectToPage("/Cart");

            CartItems = _productQuery.CheckInventoryStatus(cartItems);

            return RedirectToPage(CartItems.Any(x => !x.IsInStock) ? "/Cart" : "/Checkout");
        }

        private List<CartItem> NormalizeCartItems(List<CartItem> items)
        {
            foreach (var item in items)
            {
                if (item.Count < 1)
                    item.Count = 1;

                item.CalculateTotalItemPrice();
            }
            return items;
        }
    }
}