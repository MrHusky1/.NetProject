using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Domain
{
    public class Group : Entity
    {
        [Required]
        public string Title { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}