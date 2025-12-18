using CORE.APP.Models;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class GenreRequest : Request
    {
        [Required(ErrorMessage = "{0} is required!")]
        public string Name { get; set; }
    }
}