using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfileBook.Services.AuthenticationServices
{
    /// <summary>
    /// Check if there is a user with the entered username and password
    /// </summary>
    public class UserAuthentication : IUserAuthentication
    {
        public User _userLogin;
        public User _userPassw;
        public void GetUsersFromDB(string login, string password)
        {
            User log = App.Database.GetItems().FirstOrDefault(user => user.Login == login);     //get user data by login
            User pas = App.Database.GetItems().FirstOrDefault(user => user.Password == password);    //get user data by password
            _userLogin = log;
            _userPassw = pas;
        }

        public bool IsPasswordConfirm()
        {
            bool result = false;
            if (_userLogin != null && _userPassw != null)       //if data is received
            {
                if (_userLogin.Id == _userPassw.Id)     //checking user ID. If the ID matches, 
                    result = true;                      //then this is the same user and this password is correct
            }
            
            else result = false;
            return result;
        }
    }
}
