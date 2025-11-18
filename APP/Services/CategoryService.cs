using APP.Domain;
using APP.Models;
using CORE.APP.Models;
using CORE.APP.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Services
{
    public class CategoryService : Service<Category>, IService<CategoryRequest, CategoryResponse>
    {
        public CategoryService(DbContext db) : base(db)
        {
        }

        protected override IQueryable<Category> Query(bool isNoTracking = true)
        {
            return base.Query(isNoTracking)
                .Include(c => c.Products)
                .OrderBy(c => c.Title);
        }

        public List<CategoryResponse> List()
        {
            return Query().Select(c => new CategoryResponse
            {
                Description = c.Description,
                Guid = c.Guid,
                Id = c.Id,
                Title = c.Title,

                // Way 1:
                //ProductList = c.Products.Select(p => new ProductResponse
                //{
                //    Id = p.Id,
                //    Name = p.Name,
                //    ExpirationDate = p.ExpirationDate,
                //    Guid = p.Guid,
                //    StockAmount = p.StockAmount,
                //    UnitPrice = p.UnitPrice,
                //    //... 
                //}).ToList(),

                // Way 2:
                ProductCount = c.Products.Count,
                Products = string.Join(", ", c.Products.Select(p => p.Name))
                
            }).ToList();
        }

        public CategoryResponse Item(int id)
        {
            var entity = Query().SingleOrDefault(c => c.Id == id);
            if (entity is null)
                return null;
            return new CategoryResponse
            {
                Description = entity.Description,
                Guid = entity.Guid,
                Id = entity.Id,
                Title = entity.Title,

                // Way 1:
                //ProductList = entity.Products.Select(p => new ProductResponse
                //{
                //    Id = p.Id,
                //    Name = p.Name,
                //    ExpirationDate = p.ExpirationDate,
                //    Guid = p.Guid,
                //    StockAmount = p.StockAmount,
                //    UnitPrice = p.UnitPrice,
                //    //... 
                //}).ToList(),

                // Way 2:
                ProductCount = entity.Products.Count,
                Products = string.Join("<br>", entity.Products.Select(p => p.Name))
            };
        }

        public CategoryRequest Edit(int id)
        {
            var entity = Query().SingleOrDefault(c => c.Id == id);
            if (entity is null)
                return null;
            return new CategoryRequest
            {
                Description = entity.Description,
                Id = entity.Id,
                Title = entity.Title
            };
        }

        public CommandResponse Create(CategoryRequest request)
        {
            if (Query().Any(c => c.Title == request.Title.Trim()))
                return Error("Category with the same title already exists!");
            var entity = new Category
            {
                Description = request.Description?.Trim(),
                Title = request.Title?.Trim()
            };
            Create(entity);
            return Success("Category created successfully.", entity.Id);
        }

        public CommandResponse Update(CategoryRequest request)
        {
            if (Query().Any(c => c.Id != request.Id && c.Title == request.Title.Trim()))
                return Error("Category with the same title already exists!");
            var entity = Query().SingleOrDefault(c => c.Id == request.Id);
            if (entity is null)
                return Error("Category not found!");
            entity.Description = request.Description?.Trim();
            entity.Title = request.Title?.Trim();
            Update(entity);
            return Success("Category updated successfully.", entity.Id);
        }

        public CommandResponse Delete(int id)
        {
            var entity = Query().SingleOrDefault(c => c.Id == id);
            if (entity is null)
                return Error("Category not found!");


            // Way 1: not recommended
            //Delete(entity.Products);

            // Way 2: recommended
            if (entity.Products.Count > 0) // if (entity.Products.Any())
                return Error("Category can't be deleted because it has relational products!");


            Delete(entity);
            return Success("Category deleted successfully.", entity.Id);
        }
    }
}
