using APP.Domain;
using CORE.APP.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class BookResponse : Response
    {
        public string Name { get; set; }
        public short? NumberOfPages { get; set; }
        public DateTime PublishDate { get; set; }
        public decimal Price { get; set; }
        public bool IsTopSeller { get; set; }
        public int AuthorId { get; set; }

        [DisplayName("Publish Date")]
        public string PublishDateF { get; set; }
        public string PriceF { get; set; }
        [DisplayName("Top Seller?")]
        public string IsTopSellerF { get; set; }

    }
}