using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.AuthenticationServices
{
    public interface IUserAuthentication
    {
        void GetUsersFromDB(string login, string password);

        bool IsPasswordConfirm();
    }
}
