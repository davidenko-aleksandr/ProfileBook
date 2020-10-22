

namespace ProfileBook.Services.AuthorizationServices
{
    public class AuthorizationService : IAuthorizationService
    {       
        public static int Login { get; set; }

        public int AddUodateUserId(int id)
        {
            Login = id;
            return id;
        }

        public void ToWriteLoginId()
        {
            App.UserLogin = Login;
        }
    }
}
