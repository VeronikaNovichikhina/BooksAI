using BooksAI.Data;
using BooksAI.Models;
using BooksAI.Services.BasketService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;

namespace BooksAI.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int bookId, int quantity)
        {
            
            var basketId = GetBasketId();


            await _basketService.AddToCartAsync(basketId, bookId, quantity);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var basketId = GetBasketId();
            var items = await _basketService.GetItemsAsync(basketId);
            ViewBag.Total = await _basketService.GetTotalAsync(basketId);

            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int itemId, int quantity)
        {
            await _basketService.UpdateQuantityAsync(itemId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int itemId)
        {
            await _basketService.RemoveItemAsync(itemId);

            return RedirectToAction("Index");
        }

        private string GetBasketId()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                return User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

           
            if (Request.Cookies.ContainsKey("BasketId"))
            {
                return Request.Cookies["BasketId"];
            }

            var newBasketId = Guid.NewGuid().ToString();
            Response.Cookies.Append("BasketId", newBasketId, new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30)
            });

            return newBasketId;
        }

    }
}
