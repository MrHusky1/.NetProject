using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Domain
{
    public class Genre : Entity
    {
        [Required]
        public string Name { get; set; }
    }
}