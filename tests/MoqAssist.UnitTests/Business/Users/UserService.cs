namespace MoqAssist.UnitTests.Business.Users
{
    public class UserService : IUserService
    {
        public UserService() { }
        public User GetById(int id)
        {
            if (id < 1) return null;
            else return new User() { Id = id, Name = "John", Surname = "Doe" };
        }
    }
}