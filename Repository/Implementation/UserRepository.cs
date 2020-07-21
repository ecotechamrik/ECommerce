using BAL.Entities;
using BAL.ViewModels.User;
using DAL;
using DAL.DBInitializer;
using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        DatabaseContext Context
        {
            get
            {
                return db as DatabaseContext;
            }
        }
        public UserRepository(DbContext _db) : base(_db)
        {

        }
        /// <summary>
        /// Get User Details with Roles
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User GetUsersWithRoles(string username, string password)
        {
            var user = Context.Users
                        .Include(ur => ur.UserRoles)
                        .ThenInclude(r => r.Role)
                        .Where(u => u.UserName == username && u.Password == password)
                        .FirstOrDefault();
            return user;
        }
    }
}