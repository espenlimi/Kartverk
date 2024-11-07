using Kartverk.Mvc.DataAccess.Entities;

namespace Kartverk.Mvc.Repositories
{
    public class UserRepository 
    {
        public async Task<User> GetUser(string email)
        {
            //Yes, this is proper stupid. But it's just a test.
            return await Task.FromResult(new User
            {
                Name = "Test",
                Email = email,
                Language = "en"
            });
        }

        public async Task AddUser(User user)
        {

            await Task.CompletedTask;
        }
    }
}
