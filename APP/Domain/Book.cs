using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Domain
{
    public class Book : Entity
    {
        [Required, StringLength(30)]
        public string Name { get; set; }
        public short? NumberOfPages { get; set; }
        public DateTime PublishDate { get; set; }
        [Required]
        public decimal Price { get; set; }
        public bool IsTopSeller { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public List<BookGenre> BookGenres { get; set; } = new List<BookGenre>();

        [NotMapped]
        public List<int> GenreIds
        {
            get => BookGenres.Select(userRoleEntity => userRoleEntity.GenreId).ToList();
            set => BookGenres = value?.Select(genreId => new BookGenre() { GenreId = genreId }).ToList();
        }
    }
}