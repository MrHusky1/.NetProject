using APP.Models;
using CORE.APP.Services;
using CORE.APP.Services.Session.MVC;

namespace APP.Services
{
    /// <summary>
    /// Provides operations for managing a user's shopping cart.
    /// Stores cart data in session and retrieves book details using the book service.
    /// </summary>
    public class CartService : ICartService
    {
        /// <summary>
        /// The private constant session key used to store and retrieve cart data.
        /// </summary>
        const string SESSIONKEY = "cart";

        /// <summary>
        /// Service for session management, used to store and retrieve cart items.
        /// </summary>
        private readonly SessionServiceBase _sessionService;

        /// <summary>
        /// Service for accessing book data.
        /// </summary>
        private readonly IService<BookRequest, BookResponse> _bookService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartService"/> class with the injection of session and book services.
        /// </summary>
        /// <param name="sessionService">Session service for managing cart data in session.</param>
        /// <param name="bookService">Book service for retrieving book details.</param>
        public CartService(SessionServiceBase sessionService, IService<BookRequest, BookResponse> bookService)
        {
            _bookService = bookService;
            _sessionService = sessionService;
        }

        /// <summary>
        /// Retrieves the list of items in the user's cart by user ID.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A list of <see cref="CartItem"/> objects representing the user's cart contents.</returns>
        public List<CartItem> GetCart(int userId)
        {
            var cart = _sessionService.GetSession<List<CartItem>>(SESSIONKEY);
            if (cart is not null)
                return cart.Where(c => c.UserId == userId).ToList();
            return new List<CartItem>();
        }

        /// <summary>
        /// Retrieves a grouped summary of the user's cart items by book.
        /// Groups items by user ID, book ID and book name, then aggregates the book count and total price for each book.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>
        /// A list of <see cref="CartItemGroupedBy"/> objects, each representing a book in the user's cart
        /// with the total quantity and total price for that book.
        /// </returns>
        public List<CartItemGroupedBy> GetCartGroupedBy(int userId)
        {
            // Get all cart items for the specified user.
            var cart = GetCart(userId);

            // Group cart items by user ID, book ID and book name, then project each group into a summary object.
            return cart
                .GroupBy(cartItem => new // group cart items with key user ID, book ID and book name
                {
                    cartItem.UserId,
                    cartItem.BookId,
                    cartItem.BookName
                })
                .Select(cartItemGroupedBy => new CartItemGroupedBy
                {
                    UserId = cartItemGroupedBy.Key.UserId, // grouped cart item key's user ID
                    BookId = cartItemGroupedBy.Key.BookId, // grouped cart item key's book ID
                    BookName = cartItemGroupedBy.Key.BookName, // grouped cart item key's book name
                    BookCount = cartItemGroupedBy.Count(), // Total quantity of this book according to the key
                    TotalPrice = cartItemGroupedBy.Sum(cartItem => cartItem.BookPrice), // Aggregate price according to the key
                    TotalPriceF = "$" + cartItemGroupedBy.Sum(cartItem => cartItem.BookPrice).ToString("N1")
                 // Formatted price for display
        }).ToList();
        }

        /// <summary>
        /// Adds a book to the user's cart by user ID.
        /// Retrieves book details and updates the session cart.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="bookId">The unique identifier of the book to add.</param>
        public void AddToCart(int userId, int bookId)
        {
            var book = _bookService.Item(bookId);
            if (book is not null)
            {
                var cart = _sessionService.GetSession<List<CartItem>>(SESSIONKEY) ?? new List<CartItem>();

                cart.Add(new CartItem
                {
                    UserId = userId,
                    BookId = book.Id,
                    BookName = book.Name,
                    BookPrice = book.Price,
                    BookPriceF = "$" + book.Price.ToString("N1")
                });

                _sessionService.SetSession(SESSIONKEY, cart);
            }
        }

        /// <summary>
        /// Removes a book from the user's cart by user ID.
        /// Updates the session cart after removal.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="bookId">The unique identifier of the book to remove.</param>
        public void RemoveFromCart(int userId, int bookId)
        {
            var cart = _sessionService.GetSession<List<CartItem>>(SESSIONKEY) ?? new List<CartItem>();
            var cartItem = cart.FirstOrDefault(c => c.UserId == userId && c.BookId == bookId);
            if (cartItem is not null)
            {
                cart.Remove(cartItem);
                _sessionService.SetSession(SESSIONKEY, cart);
            }
        }

        public void ClearCart(int userId)
        {
            var cart = _sessionService.GetSession<List<CartItem>>(SESSIONKEY) ?? new List<CartItem>();
            cart.RemoveAll(c => c.UserId == userId);
            _sessionService.SetSession(SESSIONKEY, cart);
        }
    }
}