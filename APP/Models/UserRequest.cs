using APP.Domain;
using CORE.APP.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class UserRequest : Request
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(30, ErrorMessage = "{0} must be maximum {1} characters!")]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        public string Password { get; set; }

        [DisplayName("First Name")]
        public string? FirstName { get; set; }

        [DisplayName("Last Name")]
        public string? LastName { get; set; }
        public Genders Gender { get; set; }

        [DisplayName("Birth Date")]
        public DateTime? BirthDate { get; set; }
        public decimal Score { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }
        public string? Address { get; set; }
        //public int? CountryId { get; set; }
        //public int? CityId { get; set; }

        [DisplayName("Group")]
        public int? GroupId { get; set; }

        [Required(ErrorMessage = "At least one role is required!")]
        [DisplayName("Roles")]
        public List<int> RoleIds { get; set; }
    }
}