using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Domain
{
    public class User : Entity
    {
        [Required, StringLength(30)]
        public string UserName { get; set; }

        [Required, StringLength(30)]
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Genders Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public decimal Score { get; set; }
        public bool IsActive { get; set; }
        public string? Address { get; set; }
        //public int? CountryId { get; set; }
        //public int? CityId { get; set; }
        public int? GroupId { get; set; }
        public Group Group { get; set; }

        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();

        [NotMapped]
        public List<int> RoleIds
        {
            get => UserRoles.Select(userRoleEntity => userRoleEntity.RoleId).ToList();
            set => UserRoles = value?.Select(roleId => new UserRole() { RoleId = roleId }).ToList();
        }
    }

    public enum Genders
    {
        Male = 1,
        Female = 2,
    }
}