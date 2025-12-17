using APP.Domain;
using CORE.APP.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class BookRequest : Request
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(30, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }
        public short? NumberOfPages { get; set; }
        public DateTime PublishDate { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        public decimal Price { get; set; }
        [DisplayName("Top Seller?")]
        public bool IsTopSeller { get; set; }
        [DisplayName("Author's ID")]
        public int AuthorId { get; set; }
    }
}