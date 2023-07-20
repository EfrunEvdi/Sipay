using Restful.WebApi.DAL.Entities;

namespace Restful.WebApi.FakeService.Abstract
{
    public interface IUserService
    {
        List<User> GetUsers();
        void LoginUser(User user);
    }
}
