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

        public bool IsCheckLogin(string login)
        {
            bool isInvalidLOgin = false;
            string pattern = @"^\d\w*";

            if (login.Length < 4 ||
                login.Length > 16 ||
                Regex.IsMatch(login, pattern, RegexOptions.IgnoreCase))
            {
                isInvalidLOgin = true;
            }
            return isInvalidLOgin;
        }


        public bool IsCheckLoginDB(string login)
        {
            var lg = _repository.GetAllItems().FirstOrDefault(user => user.Login == login); 

            return lg != null;
        }
    }
}
