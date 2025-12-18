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
                .Include(u => u.BookGenres);
        }

        public List<BookResponse> List()
        {
            var query = Query().Select(u => new BookResponse
            {
                Id = u.Id,
                Name = u.Name,
                NumberOfPages = u.NumberOfPages,
                PublishDate = u.PublishDate,
                Price = u.Price,
                IsTopSeller = u.IsTopSeller,
                AuthorId = u.AuthorId,

                PublishDateF = u.PublishDate.ToString("MM/dd/yyyy"),

                PriceF = u.Price.ToString("N2"),

                IsTopSellerF = u.IsTopSeller ? "Top Seller" : "",

                Genres = u.BookGenres.Select(ur => ur.GenreId != null ? ur.Genre.Name : string.Empty).ToList(),
            });

            return query.ToList();
        }
        public BookResponse Item(int id)
        {
            var entity = Query().SingleOrDefault(u => u.Id == id);

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

                Genres = entity.BookGenres.Select(ur => ur.GenreId != null ? ur.Genre.Name : string.Empty).ToList(),
            };
        }

        public CommandResponse Create(BookRequest request)
        {
            if (Query().Any(u => u.Name == request.Name.Trim() && u.AuthorId == request.AuthorId))
                return Error("This book already exists!");
            var entity = new Book
            {
                Name = request.Name,
                NumberOfPages = request.NumberOfPages,
                PublishDate = request.PublishDate,
                Price = request.Price,
                IsTopSeller = request.IsTopSeller,
                AuthorId = request.AuthorId,
                GenreIds = request.GenreIds,
            };

            Create(entity);
            return Success("Book created successfully.", entity.Id);
        }

        public CommandResponse Update(BookRequest request)
        {
            if (Query().Any(u => u.Id != request.Id && u.Name == request.Name.Trim() && u.AuthorId == request.AuthorId))
                return Error("This book already exists!");

            var entity = Query(false).SingleOrDefault(u => u.Id == request.Id);

            if (entity is null)
                return Error("Book not found!");

            Delete(entity.BookGenres);

            entity.Name = request.Name;
            entity.NumberOfPages = request.NumberOfPages;
            entity.PublishDate = request.PublishDate;
            entity.Price = request.Price;
            entity.IsTopSeller = request.IsTopSeller;
            entity.AuthorId = request.AuthorId;

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
