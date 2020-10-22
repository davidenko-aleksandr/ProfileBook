
namespace ProfileBook.Services.AuthenticationServices
{
    public interface IUserAuthentication
    {
        void GetUsersFromDB(string login, string password);
        int GetUserId();
        bool IsPasswordConfirm();
    }
}
