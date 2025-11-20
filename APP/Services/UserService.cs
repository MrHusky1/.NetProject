using APP.Domain;
using APP.Models;
using CORE.APP.Models;
using CORE.APP.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace APP.Services
{
    public class UserService : Service<User>, IService<UserRequest, UserResponse>
    {
        public UserService(DbContext db) : base(db)
        {

        }

        protected override IQueryable<User> Query(bool isNoTracking = true)
        {
            // Eagerly load all relations needed for List() and Item()
            return base.Query(isNoTracking)
                .Include(u => u.Group)
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .OrderBy(u => u.UserName);
        }

        public List<UserResponse> List()
        {
            var query = Query().Select(u => new UserResponse
            {
                Id = u.Id,
                UserName = u.UserName,
                Password = u.Password,
                FirstName = u.FirstName,
                LastName = u.LastName,
                BirthDate = u.BirthDate,
                RegistrationDate = u.RegistrationDate,
                Score = u.Score,
                IsActive = u.IsActive,
                Address = u.Address,
                //CountryId = u.CountryId,
                //CityId = u.CityId,
                GroupId = u.GroupId,


                IsActiveF = u.IsActive ? "Active" : "Inactive",
                FullName = u.FirstName + " " + u.LastName,

                GenderF = u.Gender == Genders.Male ? "Male" : u.Gender == Genders.Female ? "Female" : "Unknown",

                BirthDateF = u.BirthDate.HasValue ? u.BirthDate.Value.ToString("MM/dd/yyyy") : string.Empty,
                RegistrationDateF = u.RegistrationDate.ToString("MM/dd/yyyy"),
                ScoreF = u.Score.ToString("N1"),

                Roles = u.UserRoles.Select(ur => ur.Role != null ? ur.Role.Name : string.Empty).ToList(),
                // RoleIds = u.UserRoles.Select(ur => ur.RoleId).ToList()

                GroupTitle = u.Group != null ? u.Group.Title : string.Empty,

            });

            return query.ToList();
        }
        public UserResponse Item(int id)
        {
            var entity = Query().SingleOrDefault(u => u.Id == id);

            if (entity is null)
                return null;

            return new UserResponse()
            {
                Id = entity.Id,
                UserName = entity.UserName,
                Password = entity.Password,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                BirthDate = entity.BirthDate,
                RegistrationDate = entity.RegistrationDate,
                Score = entity.Score,
                IsActive = entity.IsActive,
                Address = entity.Address,
                //CountryId = entity.CountryId,
                //CityId = entity.CityId,
                GroupId = entity.GroupId,

                IsActiveF = entity.IsActive ? "Active" : "Inactive",
                FullName = entity.FirstName + " " + entity.LastName,
                GenderF = entity.Gender.ToString(), 
                BirthDateF = entity.BirthDate.HasValue ? entity.BirthDate.Value.ToString("MM/dd/yyyy") : string.Empty,
                RegistrationDateF = entity.RegistrationDate.ToString("MM/dd/yyyy"),
                ScoreF = entity.Score.ToString("N1"),
                GroupTitle = entity.Group != null ? entity.Group.Title : string.Empty,

                Roles = entity.UserRoles?.Select(ur => ur.Role != null ? ur.Role.Name : string.Empty).ToList() ?? new List<string>(),
               // RoleIds = entity.UserRoles?.Select(ur => ur.RoleId).ToList() ?? new List<int>()

            };
        }

        public CommandResponse Create(UserRequest request)
        {
            if (Query().Any(u => u.UserName == request.UserName.Trim() && u.IsActive == request.IsActive))
                return Error("Active user with the same user name exists!");
            var entity = new User
            {
                UserName = request.UserName,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                BirthDate = request.BirthDate,
                RegistrationDate = DateTime.Now,
                Score = request.Score,
                IsActive = request.IsActive,
                Address = request.Address,
                //CountryId = request.CountryId,
                //CityId = request.CityId,
                GroupId = request.GroupId,
                RoleIds = request.RoleIds,
            };

            Create(entity);
            return Success("User created successfully.", entity.Id);
        }

        public CommandResponse Update(UserRequest request)
        {
            if (Query().Any(u => u.Id != request.Id && u.FirstName == request.FirstName.Trim() && u.LastName == request.LastName.Trim()))
                return Error("User with the same first and last name exists!");

            var entity = Query(false).SingleOrDefault(u => u.Id == request.Id);

            if (entity is null)
                return Error("User not found!");

            Delete(entity.UserRoles);

            entity.UserName = request.UserName;
            entity.Password = request.Password;
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.BirthDate = request.BirthDate;
            entity.Score = request.Score;
            entity.IsActive = request.IsActive;
            entity.Address = request.Address;
            entity.CountryId = request.CountryId;
            entity.CityId = request.CityId;
            entity.GroupId = request.GroupId;
            //entity.RoleIds = request.RoleIds;

            entity.UserRoles = request.RoleIds?.Select(roleId => new UserRole
            {
                UserId = entity.Id,
                RoleId = roleId
            }).ToList() ?? new List<UserRole>();

            entity.RoleIds = request.RoleIds;

            Update(entity);
            return Success("User updated successfully.", entity.Id);
        }

        public CommandResponse Delete(int id)
        {
            var entity = Query(false).SingleOrDefault(u => u.Id == id);

            if (entity is null)
                return Error("User not found!");

            Delete(entity.UserRoles);

            Delete(entity);
            return Success("User deleted successfully.", entity.Id);
        }

        public UserRequest Edit(int id)
        {
            var entity = Query(false).SingleOrDefault(u => u.Id == id);
            if (entity is null)
                return null;

            return new UserRequest()
            {
                Id = entity.Id,
                UserName = entity.UserName,
                Password = entity.Password,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Gender = entity.Gender,
                BirthDate = entity.BirthDate,
                Score = entity.Score,
                IsActive = entity.IsActive,
                Address = entity.Address,
                //CountryId = entity.CountryId,
                //CityId = entity.CityId,
                GroupId = entity.GroupId,
                RoleIds = entity.RoleIds
            };
        }
    }
}
