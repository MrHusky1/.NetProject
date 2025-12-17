using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Domain
{
    public class Author : Entity
    {
        [Required, StringLength(30)]
        public string FirstName { get; set; }
        [Required, StringLength(30)]
        public string LastName { get; set; }
    }
}