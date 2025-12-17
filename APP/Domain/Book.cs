using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Domain
{
    public class Book : Entity
    {
        [Required, StringLength(30)]
        public string Name { get; set; }
        public short? NumberOfPages { get; set; }
        public DateTime PublishDate { get; set; }
        [Required]
        public decimal Price { get; set; }
        public bool IsTopSeller { get; set; }
        public int AuthorId { get; set; }
    }
}