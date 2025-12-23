using APP.Domain;
using APP.Models;
using CORE.APP.Models;
using CORE.APP.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace APP.Services
{
    public class BookService : Service<Book>, IService<BookRequest, BookResponse>
    {
        public BookService(DbContext db) : base(db)
        {

        }

        protected override IQueryable<Book> Query(bool isNoTracking = true)
        {
            // Eagerly load all relations needed for List() and Item()
            return base.Query(isNoTracking)
                .Include(b => b.BookGenres);
        }

        public List<BookResponse> List()
        {
            var query = Query().Select(b => new BookResponse
            {
                Id = b.Id,
                Name = b.Name,
                NumberOfPages = b.NumberOfPages,
                PublishDate = b.PublishDate,
                Price = b.Price,
                IsTopSeller = b.IsTopSeller,
                AuthorId = b.AuthorId,

                PublishDateF = b.PublishDate.ToString("MM/dd/yyyy"),

                PriceF = b.Price.ToString("N2"),

                IsTopSellerF = b.IsTopSeller ? "Top Seller" : "",

                Genres = b.BookGenres.Select(bg => bg.GenreId != null ? bg.Genre.Name : string.Empty).ToList(),
            });

            return query.ToList();
        }
        public BookResponse Item(int id)
        {
            var entity = Query().SingleOrDefault(b => b.Id == id);

            if (entity is null)
                return null;

            return new BookResponse()
            {
                Id = entity.Id,
                Name = entity.Name,
                NumberOfPages = entity.NumberOfPages,
                PublishDate = entity.PublishDate,
                Price = entity.Price,
                IsTopSeller = entity.IsTopSeller,
                AuthorId = entity.AuthorId,

                PublishDateF = entity.PublishDate.ToString("MM/dd/yyyy"),

                PriceF = entity.Price.ToString("N2"),

                IsTopSellerF = entity.IsTopSeller ? "Top Seller" : "",

                Genres = entity.BookGenres.Select(bg => bg.GenreId != null ? bg.Genre.Name : string.Empty).ToList(),
            };
        }

        public CommandResponse Create(BookRequest request)
        {
            if (Query().Any(b => b.Name == request.Name.Trim() && b.AuthorId == request.AuthorId))
                return Error("This book already exists!");
            var entity = new Book
            {
                Name = request.Name,
                NumberOfPages = request.NumberOfPages,
                PublishDate = request.PublishDate.Value,
                Price = request.Price.Value,
                IsTopSeller = request.IsTopSeller,
                AuthorId = request.AuthorId.Value,
                GenreIds = request.GenreIds,
            };

            Create(entity);
            return Success("Book created successfully.", entity.Id);
        }

        public CommandResponse Update(BookRequest request)
        {
            if (Query().Any(b => b.Id != request.Id && b.Name == request.Name.Trim() && b.AuthorId == request.AuthorId))
                return Error("This book already exists!");

            var entity = Query(false).SingleOrDefault(b => b.Id == request.Id);

            if (entity is null)
                return Error("Book not found!");

            Delete(entity.BookGenres);

            entity.Name = request.Name;
            entity.NumberOfPages = request.NumberOfPages;
            entity.PublishDate = request.PublishDate.Value;
            entity.Price = request.Price.Value;
            entity.IsTopSeller = request.IsTopSeller;
            entity.AuthorId = request.AuthorId.Value;

            entity.BookGenres = request.GenreIds?.Select(genreId => new BookGenre
            {
                BookId = entity.Id,
                GenreId = genreId
            }).ToList() ?? new List<BookGenre>();

            entity.GenreIds = request.GenreIds;

            Update(entity);
            return Success("Book updated successfully.", entity.Id);
        }

        public CommandResponse Delete(int id)
        {
            var entity = Query(false).SingleOrDefault(u => u.Id == id);

            if (entity is null)
                return Error("Book not found!");

            Delete(entity.BookGenres);

            Delete(entity);
            return Success("Book deleted successfully.", entity.Id);
        }

        public BookRequest Edit(int id)
        {
            var entity = Query(false).SingleOrDefault(u => u.Id == id);
            if (entity is null)
                return null;

            return new BookRequest()
            {
                Id = entity.Id,
                Name = entity.Name,
                NumberOfPages = entity.NumberOfPages,
                PublishDate = entity.PublishDate,
                Price = entity.Price,
                IsTopSeller = entity.IsTopSeller,
                AuthorId = entity.AuthorId,
                GenreIds = entity.GenreIds,
            };
        }
    }
}
