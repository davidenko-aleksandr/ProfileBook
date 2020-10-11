using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services
{
    public interface ICheckPasswordValid
    {
        bool IsPasswordValid(string pas);
    }
}
