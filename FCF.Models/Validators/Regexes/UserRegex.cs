using System.Text.RegularExpressions;

namespace FCF.Models.Validators.Regexes
{
    public class UserRegex
    {
        public static bool CheckPassword(string password)
        {
            Regex passwordRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            return passwordRegex.IsMatch(password);
        }
    }
}
