using FluentValidation;
using FluentValidation.Results;

namespace FCF.Helpers
{
    public abstract class AbstractValidatorCustom<T> : AbstractValidator<T>
    {

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var validationResult = base.Validate(context);

            if (!validationResult.IsValid)
            {
                RaiseValidationException(context, validationResult);
            }

            return validationResult;
        }
    }

}
