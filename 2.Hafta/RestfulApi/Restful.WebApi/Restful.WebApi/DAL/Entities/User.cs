namespace Restful.WebApi.DAL.Entities
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
