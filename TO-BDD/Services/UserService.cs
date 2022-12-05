namespace TO_BDD.Services
{
    public interface IUserService
    {
        bool Login(string username, string password);
        bool Register(string username, string password);
    }
    public class UserService
    {
    }
}
