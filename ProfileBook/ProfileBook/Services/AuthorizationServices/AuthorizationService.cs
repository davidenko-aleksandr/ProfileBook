

namespace ProfileBook.Services.AuthorizationServices
{
    public class AuthorizationService : IAuthorizationService
    {       
        private static int logId;
        public static int Login
        {
            get { return logId; }
            set
            {
                if (value != logId)
                    _ = logId;
            }
        }

        public void ToWriteLoginId()
        {
            App.UserLogin = Login;
        }

        public int AddUodateUserId(int id)
        {
            logId = id;
            return id;            
        }
    }
}
