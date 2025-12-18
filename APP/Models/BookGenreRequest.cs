using CORE.APP.Models;

namespace APP.Models
{
    public class BookGenreRequest : Request
    {
        public int BookId { get; set; }
        public int GenreId { get; set; }
    }
}