using System.ComponentModel;

namespace APP.Models
{
    /// <summary>
    /// Represents an item in a user's shopping cart.
    /// Contains product details and pricing information for cart operations.
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// The unique identifier of the user who owns the cart item.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The unique identifier of the book added to the cart.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// The name of the book.
        /// Used for display purposes in the views.
        /// </summary>
        [DisplayName("Book Name")]
        public string BookName { get; set; }

        /// <summary>
        /// The unit price of the book as a decimal value.
        /// Used for calculations and backend operations.
        /// </summary>
        public decimal BookPrice { get; set; }

        /// <summary>
        /// The formatted unit price of the book as a string.
        /// Used for display purposes in the views.
        /// </summary>
        [DisplayName("Book Price")]
        public string BookPriceF { get; set; }
    }
}