using CORE.APP.Domain;

namespace APP.Domain
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Genders Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public decimal Score { get; set; }
        public bool IsActive { get; set; }
        public string? Address { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? GroupId { get; set; }
        public Group Group { get; set; }

        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

    public enum Genders
    {
        Male = 1,
        Female = 2,
    }
}