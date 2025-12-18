using CORE.APP.Models;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class AuthorRequest : Request
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(30, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(30, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string LastName { get; set; }
    }
}