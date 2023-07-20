using Restful.WebApi.DAL.Entities;
using Restful.WebApi.FakeService.Abstract;
using Restful.WebApi.Models;

namespace Restful.WebApi.FakeService.Concrete
{
    public class UserManager : IUserService
    {
        private readonly List<User> _userList;

        public UserManager()
        {
            _userList = Users.UserList;
        }

        public List<User> GetUsers()
        {
            return _userList;
        }

        public void LoginUser(User user)
        {
            var foundUser = _userList.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            if (foundUser != null)
            {
                foundUser.IsLoggedIn = true;
            }
        }
    }
}
