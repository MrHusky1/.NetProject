using CORE.APP.Domain;

namespace APP.Domain
{
    public class Group : Entity
    {
        public string Title { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}