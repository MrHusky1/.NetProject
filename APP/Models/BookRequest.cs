using APP.Domain;
using CORE.APP.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class BookRequest : Request
    {
        [DisplayName("Book Name")]
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(30, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }

        [DisplayName("Number of Pages")]
        public short? NumberOfPages { get; set; }

        [DisplayName("Publish Date")]
        public DateTime? PublishDate { get; set; }

        [DisplayName("Price")]
        [Required(ErrorMessage = "{0} is required!")]
        public decimal? Price { get; set; }

        [DisplayName("Is Top Seller?")]
        public bool IsTopSeller { get; set; }

        [DisplayName("Author")] // Simplified from Author's ID for better UI
        [Required(ErrorMessage = "{0} is required!")]
        public int? AuthorId { get; set; }

        [DisplayName("Genre")]
        [Required(ErrorMessage = "{0} is required!")]
        public int? GenreId { get; set; }
    }
}