using APP.Models;

namespace APP.Services
{
    /// <summary>
    /// Defines operations for managing a user's shopping cart.
    /// Provides methods to retrieve, add, remove, and clear cart items.
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Retrieves the list of items in the user's cart by user ID.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A list of <see cref="CartItem"/> objects representing the user's cart contents.</returns>
        public List<CartItem> GetCart(int userId); // public may not be written

        /// <summary>
        /// Retrieves a grouped summary of the user's cart items by book.
        /// Groups items by user ID, book ID, aggregates the book count and total price for each book.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>
        /// A list of <see cref="CartItemGroupedBy"/> objects, each representing a book in the user's cart
        /// with the total quantity and total price for that product.
        /// </returns>
        public List<CartItemGroupedBy> GetCartGroupedBy(int userId);

        /// <summary>
        /// Adds a book to the user's cart by user ID.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="bookId">The unique identifier of the book to add.</param>
        public void AddToCart(int userId, int bookId);

        /// <summary>
        /// Removes a book from the user's cart by user ID.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="bookId">The unique identifier of the book to remove.</param>
        public void RemoveFromCart(int userId, int bookId);

        /// <summary>
        /// Clears all items from the user's cart by user ID.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        public void ClearCart(int userId);
    }
}