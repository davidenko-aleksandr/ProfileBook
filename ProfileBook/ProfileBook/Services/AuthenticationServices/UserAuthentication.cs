using ProfileBook.Models;
using ProfileBook.Services.RepositoryService;
using System.Linq;

namespace ProfileBook.Services.AuthenticationServices
{
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
            _userLogin = _repository.GetAllItems().FirstOrDefault(user => user.Login == login);
            _userPassw = _repository.GetAllItems().FirstOrDefault(user => user.Password == password);             
        }

        public int GetUserId()
        {
            return (int)(_userLogin?.Id);             
        }

        public bool IsPasswordConfirm()
        {
            bool result = false;
            if (_userLogin != null && _userPassw != null)       
            {
                result = _userLogin.Id == _userPassw.Id;   
            }                                            
            return result;
        }
    }
}
