using APP.Domain;
using APP.Models;
using CORE.APP.Models;
using CORE.APP.Services;

namespace APP.Services
{
    public class CategoryObsoleteService : ServiceBase
    {
        private readonly Db _db;

        public CategoryObsoleteService(Db db)
        {
            _db = db;
        }

        public IQueryable<CategoryResponse> Query()
        {
            var query = _db.Categories.Select(categoryEntity => new CategoryResponse
            {
                Id = categoryEntity.Id,
                Guid = categoryEntity.Guid,
                Title = categoryEntity.Title,
                Description = categoryEntity.Description
            });
            return query;
        }

        public CommandResponse Create(CategoryRequest request)
        {
            //var existingEntity = _db.Categories.SingleOrDefault(categoryEntity => categoryEntity.Title == request.Title.Trim());
            //// select * from categories where title = request.Title
            //if (existingEntity is null)
            //    //return new CommandResponse(false, "Category with same title exists!");
            //    return Error("Category with same title exists!");
            if (_db.Categories.Any(categoryEntity => categoryEntity.Title == request.Title.Trim()))
                return Error("Category with same title exists!");
            var entity = new Category
            {
                Title = request.Title.Trim(),
                // Way 1:
                //Description = !string.IsNullOrWhiteSpace(request.Description) ? request.Description.Trim() : null,
                // Way 2:
                Description = request.Description?.Trim(),
                Guid = Guid.NewGuid().ToString()
            };
            _db.Categories.Add(entity);
            _db.SaveChanges();
            return Success("Category created successfully", entity.Id);
        }

        public CategoryRequest Edit(int id)
        {
            var entity = _db.Categories.SingleOrDefault(c => c.Id == id);
            if (entity is null)
                return null;
            return new CategoryRequest
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description
            };
        }

        public CommandResponse Update(CategoryRequest request)
        {
            if (_db.Categories.Any(c => c.Id != request.Id && c.Title == request.Title.Trim()))
                return Error("Category with same title exists!");
            var entity = _db.Categories.SingleOrDefault(c => c.Id == request.Id);
            if (entity is null)
                return Error("Category not found!");
            entity.Title = request.Title?.Trim();
            entity.Description = request.Description?.Trim();
            _db.Categories.Update(entity);
            _db.SaveChanges();
            return Success("Category updated successfully.", entity.Id);
        }

        public CommandResponse Delete(int id)
        {
            var entity = _db.Categories.SingleOrDefault(c => c.Id == id);
            if (entity is null)
                return Error("Category not found!");
            _db.Categories.Remove(entity);
            _db.SaveChanges();
            return Success("Category deleted successfully.", entity.Id);
        }
    }
}
