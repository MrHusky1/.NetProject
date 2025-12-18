using CORE.APP.Models;

namespace APP.Models
{
    public class AuthorResponse : Response
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName { get; set; }
    }
}