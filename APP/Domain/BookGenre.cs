using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Domain
{
    public class BookGenre : Entity
    {
        public int BookId { get; set; }
        public int GenreId { get; set; }
    }
}