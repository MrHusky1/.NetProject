using CORE.APP.Services;
using APP.Domain;
using APP.Models;
using Microsoft.EntityFrameworkCore;
using CORE.APP.Models;

namespace APP.Services
{
    public class RoleService : Service<Role>, IService<RoleRequest, RoleResponse>
    {
        public RoleService(DbContext db) : base(db)
        {

        }

        public List<RoleResponse> List()
        {
            var query = Query().Select(r => new RoleResponse
            {
                Id = r.Id,
                Name = r.Name,
            });

            return query.ToList();
        }
        public RoleResponse Item(int id)
        {
            var entity = Query().SingleOrDefault(r => r.Id == id);

            if (entity is null)
                return null;

            return new RoleResponse()
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }

        public CommandResponse Create(RoleRequest request)
        {
            if (Query().Any(r => r.Name == request.Name.Trim()))
                return Error("Role with the same name exists!");

            var entity = new Role
            {
                Name = request.Name,
            };

            Create(entity);
            return Success("Role created successfully.", entity.Id);
        }

        public CommandResponse Update(RoleRequest request)
        {
            if (Query().Any(r => r.Id != request.Id && r.Name == request.Name.Trim()))
                return Error("Role with the same name exists!");

            var entity = Query(false).SingleOrDefault(r => r.Id == request.Id);

            if (entity is null)
                return Error("Role not found!");


            entity.Name = request.Name;


            Update(entity);
            return Success("Role updated successfully.", entity.Id);
        }

        public CommandResponse Delete(int id)
        {
            var entity = Query(false).SingleOrDefault(r => r.Id == id);

            if (entity is null)
                return Error("Role not found!");

            Delete(entity);
            return Success("Role deleted successfully.", entity.Id);
        }

        public RoleRequest Edit(int id)
        {
            var entity = Query(false).SingleOrDefault(r => r.Id == id);
            if (entity is null)
                return null;

            return new RoleRequest()
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}
