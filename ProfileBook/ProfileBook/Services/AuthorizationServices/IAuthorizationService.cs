using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.AuthorizationServices
{
    public interface IAuthorizationService
    {
        int AddUodateUserId(int id);
        void ToWriteLoginId();

    }
}
