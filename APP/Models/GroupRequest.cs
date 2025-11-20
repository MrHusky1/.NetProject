using CORE.APP.Models;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class GroupRequest : Request
    {
        [Required(ErrorMessage = "{0} is required!")]
        public string Title { get; set; }
    }
}