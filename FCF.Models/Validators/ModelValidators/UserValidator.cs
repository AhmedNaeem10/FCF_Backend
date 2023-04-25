using FluentValidation;
using FCF.Models.Validators.Regexes;
using FCF.Models.Requests.UserDtos;

namespace FCF.Models.Validators.ModelValidators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Must(CheckName).WithMessage("Name cannot be empty nor contain numbers!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email format!");
            RuleFor(x => x.Password).Must(CheckPassword).WithMessage("Password must be atleast 8 characters long with an uppercase, a lowercase and a special character!");
        }

        private bool CheckName(string name)
        {
            return !name.Any(c => char.IsDigit(c));
        }

        private bool CheckPassword(string password)
        {
            return UserRegex.CheckPassword(password);
        }
    }
}
