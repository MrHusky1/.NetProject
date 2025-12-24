using APP.Domain;
using CORE.APP.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class BookResponse : Response
    {
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Number of Pages")]
        public short? NumberOfPages { get; set; }
        [DisplayName("Publish Date")]
        public DateTime PublishDate { get; set; }
        [DisplayName("Price")]
        public decimal Price { get; set; }
        [DisplayName("Top Seller?")]
        public bool IsTopSeller { get; set; }
        public int AuthorId { get; set; }
        [DisplayName("Author")]
        public string AuthorName { get; set; }

        [DisplayName("Publish Date")]
        public string PublishDateF { get; set; }
        [DisplayName("Price")]
        public string PriceF { get; set; }
        [DisplayName("Top Seller?")]
        public string IsTopSellerF { get; set; }
        [DisplayName("Genre")]
        public string GenreName { get; set; }
    }
}