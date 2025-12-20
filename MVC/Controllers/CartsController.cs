using APP.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    /// <summary>
    /// Controller for managing shopping cart operations for authenticated users.
    /// Provides actions to view, add, remove, and clear cart items.
    /// </summary>
    [Authorize]
    public class CartsController : Controller
    {
        private readonly ICartService _cartService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartsController"/> class.
        /// </summary>
        /// <param name="cartService">Service for cart operations.</param>
        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Retrieves the current user's ID from claims.
        /// </summary>
        /// <returns>The unique identifier of the authenticated user.</returns>
        private int GetUserId() => Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == "Id")?.Value);

        /// <summary>
        /// Displays the grouped cart items for the current user.
        /// </summary>
        /// <returns>The cart view with grouped cart items.</returns>
        public IActionResult Index()
        {
            var cartGroupedBy = _cartService.GetCartGroupedBy(GetUserId());
            return View(cartGroupedBy);
        }

        /// <summary>
        /// Clears all items from the current user's cart.
        /// Sets a message and redirects to the cart index view.
        /// </summary>
        /// <returns>Redirects to the cart index view.</returns>
        public IActionResult Clear()
        {
            _cartService.ClearCart(GetUserId());
            TempData["Message"] = "Cart cleared.";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Removes a specific book from the current user's cart.
        /// Sets a message and redirects to the cart index view.
        /// </summary>
        /// <param name="bookId">The unique identifier of the book to remove.</param>
        /// <returns>Redirects to the cart index view.</returns>
        public IActionResult Remove(int bookId)
        {
            _cartService.RemoveFromCart(GetUserId(), bookId);
            TempData["Message"] = "Book removed from cart.";
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Adds a specific book to the current user's cart.
        /// Sets a message and redirects to the books index view.
        /// </summary>
        /// <param name="bookId">The unique identifier of the book to add.</param>
        /// <returns>Redirects to the books index view.</returns>
        public IActionResult Add(int bookId)
        {
            _cartService.AddToCart(GetUserId(), bookId);
            TempData["Message"] = "Book added to cart.";
            return RedirectToAction("Index", "Books");
        }
    }
}