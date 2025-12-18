using CORE.APP.Models;
using System.ComponentModel;

namespace APP.Models
{
    public class AuthorResponse : Response
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Full Name")]
        public string FullName { get; set; }
    }
}