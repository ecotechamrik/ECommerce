using BAL.Entities;
using BAL.ViewModels.User;
using System.Collections.Generic;

namespace Repository.Abstraction
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUsersWithRoles(string username, string password);
    }
}