using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services
{
    public interface ICheckLoginValid
    {
        bool IsCheckLogin(string log);
        bool IsCheckLoginDB(string login);
    }
}
