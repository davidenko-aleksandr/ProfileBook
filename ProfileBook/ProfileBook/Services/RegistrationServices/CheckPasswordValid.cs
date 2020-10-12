using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ProfileBook.Services
{
    public class CheckPasswordValid : ICheckPasswordValid
    {
        //Checking the password for correctness
        public bool IsPasswordValid(string pasword)
        {
            bool isCapitalLetter = Regex.IsMatch(pasword, "[A-Z]{1}");
            bool isSmallLatter = Regex.IsMatch(pasword, "[a-z]{1}");
            bool isNumber = Regex.IsMatch(pasword, @"\d\w*");
            bool result;
            if (isCapitalLetter == true)    //if the data contains a capital letter
                result = false;
            else result = true;            
            if (result == false)    //If a capital letter is found
            {
                if (isSmallLatter)      //check if there is a small letter
                    result = false;
                else result = true;
            }
            if (result == false)    //If a small letter is found
            {
                if (isNumber)      //check if there is a number
                    result = false;
                else result = true;
            }
            if (result == false)    //if the data is still correct
            {
                if (pasword.Length < 8 || pasword.Length > 16)  //check password length
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
