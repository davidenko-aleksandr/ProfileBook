using System.Linq;
using System.Text.RegularExpressions;

namespace ProfileBook.Services
{
    class CheckLoginValid : ICheckLoginValid
    {
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
            else return false;
        }
        //heck the login for uniqueness
        public bool IsCheckLoginDB(string login)
        {
            var lg = App.DatabaseUser.GetItems().FirstOrDefault(user => user.Login == login); //if there is already a user in the database with 
            if (lg != null)                                                               //this login, then it is not unique
                return true;
            else return false;
        }

    }
}
