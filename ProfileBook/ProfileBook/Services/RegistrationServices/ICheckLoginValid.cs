
namespace ProfileBook.Services
{
    public interface ICheckLoginValid
    {
        bool IsCheckLogin(string log);
        bool IsCheckLoginDB(string login);
    }
}
