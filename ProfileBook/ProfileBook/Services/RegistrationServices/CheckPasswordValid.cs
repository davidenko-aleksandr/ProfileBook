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
            bool isErrorExist;

            isErrorExist = !isCapitalLetter; //if the data contains a capital letter

            if (isErrorExist == false)    //If a capital letter is found
            {
                isErrorExist = !isSmallLatter;  //check if there is a small letter
            }

            if (isErrorExist == false)    //If a small letter is found
            {
                isErrorExist = !isNumber;    //check if there is a number
            }

            if (isErrorExist == false)    //if the data is still correct
            {
                isErrorExist = pasword.Length < 8 || pasword.Length > 16;  //check password length
            }
            return isErrorExist;
        }
    }
}
