using CORE.APP.Services;
using APP.Domain;
using APP.Models;
using Microsoft.EntityFrameworkCore;
using CORE.APP.Models;

namespace APP.Services
{
    public class GenreService : Service<Genre>, IService<GenreRequest, GenreResponse>
    {
        public GenreService(DbContext db) : base(db)
        {

        }

        protected override IQueryable<Genre> Query(bool isNoTracking = true)
        {
            return base.Query(isNoTracking);
        }

        public List<GenreResponse> List()
        {
            var query = Query().Select(g => new GenreResponse
            {
                Id = g.Id,
                Name = g.Name,
            });

            return query.ToList();
        }
        public GenreResponse Item(int id)
        {
            var entity = Query().SingleOrDefault(g => g.Id == id);

            if (entity is null)
                return null;

            return new GenreResponse()
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }

        public CommandResponse Create(GenreRequest request)
        {
            if (Query().Any(g => g.Name == request.Name.Trim()))
                return Error("Genre with the same name exists!");

            var entity = new Genre
            {
                Name = request.Name,
            };

            Create(entity);
            return Success("Genre created successfully.", entity.Id);
        }

        public CommandResponse Update(GenreRequest request)
        {
            if (Query().Any(g => g.Id != request.Id && g.Name == request.Name.Trim()))
                return Error("Genre with the same name exists!");

            var entity = Query(false).SingleOrDefault(g => g.Id == request.Id);

            if (entity is null)
                return Error("Genre not found!");


            entity.Name = request.Name;


            Update(entity);
            return Success("Genre updated successfully.", entity.Id);
        }

        public CommandResponse Delete(int id)
        {
            var entity = Query(false).SingleOrDefault(g => g.Id == id);

            if (entity is null)
                return Error("Genre not found!");

            Delete(entity);
            return Success("Genre deleted successfully.", entity.Id);
        }

        public GenreRequest Edit(int id)
        {
            var entity = Query(false).SingleOrDefault(g => g.Id == id);
            if (entity is null)
                return null;

            return new GenreRequest()
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}
