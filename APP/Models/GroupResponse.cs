using CORE.APP.Models;

namespace APP.Models
{
    public class GroupResponse : Response
    {
        public string Title { get; set; }

        public int UserCount { get; set; }
        public List<string> UserNames { get; set; } = new List<string>();
    }
}