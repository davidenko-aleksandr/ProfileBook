using ProfileBook.Models;
using ProfileBook.Services.RepositoryService;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProfileBook.Services
{
    class CheckLoginValid : ICheckLoginValid
    {
        private readonly IRepository<User> _repository;

        public CheckLoginValid(IRepository<User> repository)
        {
            _repository = repository;
        }

        //Checking the login for correctness
        public bool IsCheckLogin(string login)
        {
            string pattern = @"^\d\w*";
            if (login.Length < 4 ||
                login.Length > 16 ||
                Regex.IsMatch(login, pattern, RegexOptions.IgnoreCase) //if the first character is a number
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //heck the login for uniqueness
        public bool IsCheckLoginDB(string login)
        {
            var lg = _repository.GetAllItems().FirstOrDefault(user => user.Login == login); //if there is already a user in the database with 

            return lg != null;
        }
    }
}
