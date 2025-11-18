using CORE.APP.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    // request properties are created according to the data that will be retrieved from APIs or UIs
    public class ProductRequest : Request
    {
        // copy all the non navigation properties from Product entity
        /*
        ErrorMessage parameter can be set in all data annotations to show custom validation error messages:  
        Example 1: [Required(ErrorMessage = "{0} is required!")] where {0} is the DisplayName if defined otherwise property name
        which is "Product Name".
        Example 2: [StringLength(150, 3, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")] where {0} is 
        the DisplayName if defined otherwise property name which is "Product Name", {1} is the first parameter which is 150 and {2} is 
        the second parameter which is 3.
        DisplayName attribute can be used to show user friendly names in both validation error messages and views using DisplayNameFor
        HTML Helper.
        // Product Name is required and can be minimum 3 maximum 150 characters.
        */
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        [DisplayName("Product Name")]
        public string Name { get; set; }



        [Range(0.01, double.MaxValue, ErrorMessage = "{0} must be a positive decimal number!")] // minimum value can be 0.01,
                                                                                                // maximum value is the largest possible
                                                                                                // value of double type
        [DisplayName("Unit Price")]
        [Required(ErrorMessage = "{0} is required!")]
        public decimal? UnitPrice { get; set; }



        [Range(0, 1000000, ErrorMessage = "{0} must be between {1} and {2}!")] // minimum value can be 0, maximum value can be 1 million
        [DisplayName("Stock Amount")]
        public int? StockAmount { get; set; }



        [DisplayName("Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = "{0} is required!")]
        public int? CategoryId { get; set; }

        [DisplayName("Stores")]
        public List<int> StoreIds { get; set; } = new List<int>();
    }
}
