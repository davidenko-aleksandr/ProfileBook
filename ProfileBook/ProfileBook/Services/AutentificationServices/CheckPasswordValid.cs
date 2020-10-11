using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ProfileBook.Services
{
    public class CheckPasswordValid : ICheckPasswordValid
    {
        //Checking the password for correctness
        public bool IsPasswordValid(string pas)
        {
            bool isCapitalLetter = Regex.IsMatch(pas, "[A-Z]{1}");
            bool isSmallLatter = Regex.IsMatch(pas, "[a-z]{1}");
            bool isNumber = Regex.IsMatch(pas, @"\d\w*");
            bool result;
            if (isCapitalLetter == true)
                result = false;
            else result = true;
            if (result == false)
            {
                if (isSmallLatter)
                    result = false;
                else result = true;
            }
            if (result == false)
            {
                if (isNumber)
                    result = false;
                else result = true;
            }
            return result;
        }
    }
}
