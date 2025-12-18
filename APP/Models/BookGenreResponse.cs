using CORE.APP.Models;

namespace APP.Models
{
    public class BookGenreResponse : Response
    {
        public int BookId { get; set; }
        public int GenreId { get; set; }
    }
}