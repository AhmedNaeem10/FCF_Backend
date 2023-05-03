using FluentValidation;
using FCF.Models.Requests.UserDtos;
using FCF.Models.Validators.Regexes;

namespace FCF.Models.Validators.ModelValidators.User
{
    public class PasswordValidator : AbstractValidatorCustom<UpdatePasswordDto>
    {
        public PasswordValidator() {
            RuleFor(x => x.password).Must(CheckPassword).WithMessage("Password must be atleast 8 characters long with an uppercase, a lowercase and a special character!");
        }
        private bool CheckPassword(string password)
        {
            return UserRegex.CheckPassword(password);
        }   
    }
}
