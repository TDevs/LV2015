using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Oas.Infrastructure.Domain;

namespace Oas.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> accountRepository;
        private readonly IRepository<Role> roleRepository;


        public UserService(IRepository<User> accountRepository, IRepository<Role> roleRepository)
        {
            this.accountRepository = accountRepository;
            this.roleRepository = roleRepository;
        }

        /// <summary>
        /// Get User List
        /// </summary>
        /// <returns></returns>
        public List<User> Get()
        {
            return accountRepository.Get
                .Include(t => t.BusinessComments)
                .Include(t => t.Businesses)
                .Include(t => t.Roles)
                .ToList();
        }


        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User Get(string id)
        {
            return accountRepository.Get
                .Include(t => t.BusinessComments)
                .Include(t => t.Businesses)
                .Include(t => t.Roles)
                .SingleOrDefault(s => s.Id == id);
        }


        /// <summary>
        /// Get User by Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetByEmail(string email)
        {
            return accountRepository.Get
                .Include(t => t.BusinessComments)
                .Include(t => t.Businesses)
                .Include(t => t.Roles)
                .SingleOrDefault(s => s.Email == email);
        }


        /// <summary>
        /// Get Roles
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Role> GetRoles(string userId)
        {
            var ur = accountRepository.Get.SelectMany(user => user.Roles, (user, role) => new { user, role })
                .Where(x => x.user.Id.Equals(userId))
                .Select(x => x.role
                ).ToList();

            var xRole = roleRepository.Get.ToList();
            var uRole = xRole.Where(r => ur.Exists(u => u.RoleId.Equals(r.Id))).ToList();
            return uRole;
        }


        /// <summary>
        /// Get Roles
        /// </summary>
        /// <returns></returns>
        public List<Role> GetRoles()
        {
            var roles = roleRepository.Get.ToList();
            return roles;
        }

        /// <summary>
        /// Find
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public User Find(object[] keyValues)
        {
            return accountRepository.Find(keyValues);
        }

        public List<User> GetUsersByRole(string userRole)
        {
            //return (accountRepository.Get.SelectMany(user => user.Roles, (user, role) => new { user, role })
            //        .Where(x => x.role.Name == userRole).Select(x => x.user)).ToList();
            return null;
        }


        /// <summary>
        /// Check role existed
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RoleExists(string name)
        {
            var rm = new RoleManager<Role>(
    new RoleStore<Role>(new DatabaseContext()));
            return rm.RoleExists(name);
        }


        /// <summary>
        /// Create role
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CreateRole(string name)
        {
            var rm = new RoleManager<Role>(
     new RoleStore<Role>(new DatabaseContext()));
            IdentityResult idResult = rm.Create(new Role() { Name = name });
            return idResult.Succeeded;
        }


        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CreateUser(User user, string password)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool CreateUser(User user)
        {
            return accountRepository.Add(user) != null;
        }

        /// <summary>
        /// Add user to role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public bool AddUserToRole(string userId, string roleName)
        {
            var um = new UserManager<User>(
                    new UserStore<User>(new DatabaseContext()));
            um.UserValidator = new UserValidator<User>(um) { AllowOnlyAlphanumericUserNames = false };
            IdentityResult idResult = um.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }

        /// <summary>
        /// Clear user roles
        /// </summary>
        /// <param name="userId"></param>
        public void ClearUserRoles(string userId)
        {
            var um = new UserManager<User>(new UserStore<User>(new DatabaseContext()));
            um.UserValidator = new UserValidator<User>(um) { AllowOnlyAlphanumericUserNames = false };
            User user = um.FindById(userId);
            List<IdentityUserRole> currentRoles = user.Roles.ToList();
            var roles = GetRoles(userId);
            currentRoles.ForEach(item =>
            {
                var roleName = roles.FirstOrDefault(t => t.Id == item.RoleId);
                um.RemoveFromRole(user.Id, roleName.Name);
            });
        }


        /// <summary>
        /// Update User Info
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUserInfo(User user)
        {
            if (user.ExpireDate == DateTime.MinValue)
                user.ExpireDate = DateTime.Now;
            if (user.StartDate == DateTime.MinValue)
                user.StartDate = DateTime.Now;

               
                using (var dbContext = new DatabaseContext())
                {
                    var _user = dbContext.Users.FirstOrDefault(t=>t.Id.Equals(user.Id));
                    if(_user != null)
                    {
                        _user.IsOnline = user.IsOnline;
                        dbContext.SaveChanges();
                    }
                }
            
        }

        

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool DeleteUser(User user)
        {

            using (var dbContext = new DatabaseContext())
            {

                var rDel = dbContext.Users.Include(t => t.Roles).FirstOrDefault(r => r.Id.Equals(user.Id));

                if (rDel != null)
                {
                    dbContext.Users.Remove(rDel);
                    dbContext.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public void CreateRole(Role role)
        {
            roleRepository.Add(role);
        }


        /// <summary>
        /// Update role
        /// </summary>
        /// <param name="role"></param>
        public void UpdateRole(Role role)
        {
            role.UserRoles = null;
            roleRepository.Update(role);
        }


        /// <summary>
        /// Update a role
        /// </summary>
        /// <param name="role"></param>
        public void DeleteRole(Role role)
        {

            using (var dbContext = new DatabaseContext())
            {

                var rDel = dbContext.Roles.FirstOrDefault(r => r.Id.Equals(role.Id));

                if (rDel != null)
                {
                    dbContext.Roles.Remove(rDel);
                    dbContext.SaveChanges();
                }
            }

        }

        /// <summary>
        /// Get user by user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User GetUserByUserName(string userName)
        {
            var user = accountRepository.Get.Where(t => t.UserName == userName).FirstOrDefault();
            return user;
        }
    }
}
