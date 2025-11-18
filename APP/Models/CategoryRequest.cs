using CORE.APP.Models;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class CategoryRequest : Request
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } // varchar(100)

        public string Description { get; set; } // varchar(max)
    }
}
