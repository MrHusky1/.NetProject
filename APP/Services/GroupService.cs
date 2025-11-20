using CORE.APP.Services;
using APP.Domain;
using APP.Models;
using Microsoft.EntityFrameworkCore;
using CORE.APP.Models;

namespace APP.Services
{
    public class GroupService : Service<Group>, IService<GroupRequest, GroupResponse>
    {
        public GroupService(DbContext db) : base(db)
        {

        }

        protected override IQueryable<Group> Query(bool isNoTracking = true)
        {
            return base.Query(isNoTracking).Include(g => g.Users);
        }

        public List<GroupResponse> List()
        {
            var query = Query().Select(g => new GroupResponse
            {
                Id = g.Id,
                Title = g.Title,
                UserCount = g.Users.Count,
                UserNames = g.Users.Select(u => u.UserName).ToList()
            });

            return query.ToList();
        }
        public GroupResponse Item(int id)
        {
            var entity = Query().SingleOrDefault(g => g.Id == id);

            if (entity is null)
                return null;

            return new GroupResponse()
            {
                Id = entity.Id,
                Title = entity.Title,
                UserCount = entity.Users.Count,
                UserNames = entity.Users.Select(u => u.UserName).ToList()
            };
        }

        public CommandResponse Create(GroupRequest request)
        {
            if (Query().Any(g => g.Title == request.Title.Trim()))
                return Error("Group with the same title exists!");

            var entity = new Group
            {
                Title = request.Title,
            };

            Create(entity);
            return Success("Group created successfully.", entity.Id);
        }

        public CommandResponse Update(GroupRequest request)
        {
            if (Query().Any(g => g.Id != request.Id && g.Title == request.Title.Trim()))
                return Error("Group with the same title exists!");

            var entity = Query(false).SingleOrDefault(g => g.Id == request.Id);

            if (entity is null)
                return Error("Student not found!");


            entity.Title = request.Title;


            Update(entity);
            return Success("Group updated successfully.", entity.Id);
        }

        public CommandResponse Delete(int id)
        {
            var entity = Query(false).SingleOrDefault(g => g.Id == id);

            if (entity is null)
                return Error("Group not found!");

            Delete(entity);
            return Success("Group deleted successfully.", entity.Id);
        }

        public GroupRequest Edit(int id)
        {
            var entity = Query(false).SingleOrDefault(g => g.Id == id);
            if (entity is null)
                return null;

            return new GroupRequest()
            {
                Id = entity.Id,
                Title = entity.Title,
            };
        }
    }
}
