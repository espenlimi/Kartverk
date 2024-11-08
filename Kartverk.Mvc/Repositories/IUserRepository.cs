using Kartverk.Mvc.DataAccess.Entities;

namespace Kartverk.Mvc.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User> GetUser(string email);
    }
}