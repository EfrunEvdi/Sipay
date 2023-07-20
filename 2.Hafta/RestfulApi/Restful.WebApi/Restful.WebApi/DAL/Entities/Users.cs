namespace Restful.WebApi.DAL.Entities
{
    public class Users
    {
        public static List<User> UserList { get; set; }

        static Users()
        {
            UserList = new List<User>()
            {
              new User {Username = "Admin", Password = "123", IsLoggedIn = false},
              new User {Username = "Efrun", Password = "123", IsLoggedIn = false},
            };
        }
    }
}