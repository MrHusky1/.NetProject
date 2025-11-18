using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Domain
{
    public class Category : Entity
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } // varchar(100)

        public string Description { get; set; } // varchar(max)

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
