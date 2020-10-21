using ProfileBook.Models;
using ProfileBook.Services.RepositoryService;
using System.Linq;

namespace ProfileBook.Services.AuthenticationServices
{
    // Check if there is a user with the entered username and password    
    public class UserAuthentication : IUserAuthentication
    {
        public User _userLogin;
        public User _userPassw;
        private readonly IRepository<User> _repository;

        public UserAuthentication(IRepository<User> repository)
        {
            _repository = repository;
        }

        public void GetUsersFromDB(string login, string password)
        {
            User log = _repository.GetAllItems().FirstOrDefault(user => user.Login == login);     //get user data by login
            User pas = _repository.GetAllItems().FirstOrDefault(user => user.Password == password);    //get user data by password
            _userLogin = log;
            _userPassw = pas;
        }

        public int GetUserId()
        {
            int id = _userLogin.Id;
            return id;
        }

        public bool IsPasswordConfirm()
        {
            bool result = false;
            if (_userLogin != null && _userPassw != null)       //if data is received
            {
                result = _userLogin.Id == _userPassw.Id;     //checking user ID. If the ID matches, 
            }                                                //then this is the same user and this password is correct
            return result;
        }
    }
}
