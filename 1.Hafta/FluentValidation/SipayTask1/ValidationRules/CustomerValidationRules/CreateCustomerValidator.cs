using FluentValidation;
using SipayTask1.Entities;

namespace SipayTask1.ValidationRules.CustomerValidationRules
{
    public class CreateCustomerValidator : AbstractValidator<Customer>
    {
        public string NotEmptyMessage { get; } = "The {PropertyName} field cannot be empty.";
        public string MinimumLengthMessage { get; } = "The {PropertyName} field must be a minimum of {MinLength} characters.";
        public string MaximumLengthMessage { get; } = "The {PropertyName} field must be a maximum of {MaxLength} characters.";

        public CreateCustomerValidator()
        {
            RuleFor(x => x.CustomerID)
                    .NotEmpty().WithMessage(NotEmptyMessage)
                    .GreaterThanOrEqualTo(0).WithMessage("0 and value less than 0 cannot be entered.");

            RuleFor(x => x.FirstName)
                    .NotEmpty().WithMessage(NotEmptyMessage)
                    .MinimumLength(2).WithMessage(MinimumLengthMessage)
                    .MaximumLength(20).WithMessage(MaximumLengthMessage);

            RuleFor(x => x.LastName)
                    .NotEmpty().WithMessage(NotEmptyMessage)
                    .MinimumLength(2).WithMessage(MinimumLengthMessage)
                    .MaximumLength(20).WithMessage(MaximumLengthMessage);

            RuleFor(x => x.Email)
                    .NotEmpty().WithMessage(NotEmptyMessage)
                    .EmailAddress().WithMessage("Please make sure you have entered the email format correctly.");

            RuleFor(x => x.Password)
                    .NotEmpty().WithMessage(NotEmptyMessage)
                    .MinimumLength(6).WithMessage("The surname field must be a minimum of 6 characters.")
                    .Matches("[A-Z]").WithMessage("The password must contain at least one capital letter.")
                    .Matches("[a-z]").WithMessage("The password must contain at least one lowercase letter.")
                    .Matches("[0-9]").WithMessage("The password must contain at least one digit.")
                    .Matches("[^a-zA-Z0-9]").WithMessage("The password must contain at least one special character.");

            RuleFor(x => x.ConfirmPassword)
                    .NotEmpty().WithMessage(NotEmptyMessage)
                    .Equal(x => x.Password).WithMessage("Passwords do not match.");

            RuleFor(x => x.BirthDate)
                    .NotEmpty().WithMessage(NotEmptyMessage)
                    .Must(BeAtLeast18YearsOld).WithMessage("Date of birth must be over 18 years old.");

            RuleFor(x => x.Position)
                    .Must(x => x == "Junior" || x == "Mid" || x == "Senior")
                    .WithMessage("Please enter a valid position. Valid values: Junior, Mid, Senior");

            RuleFor(x => x.Salary)
                    .InclusiveBetween(15000, 20000)
                    .When(x => x.Position == "Junior")
                    .WithMessage("Salary for a junior level software developer should be in the range of 15.000 - 20.000.");

            RuleFor(x => x.Salary)
                    .InclusiveBetween(20001, 35000)
                    .When(x => x.Position == "Mid")
                    .WithMessage("Salary for a mid level software developer should be in the range of 20.001 - 35.000.");

            RuleFor(x => x.Salary)
                   .InclusiveBetween(35001, decimal.MaxValue)
                   .When(x => x.Position == "Senior")
                   .WithMessage("Salary for a senior level software developer should be in the range of 35.001 - ∞∞∞∞.");
        }

        private bool BeAtLeast18YearsOld(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate > today.AddYears(-age))
            {
                age--;
            }

            return age >= 18;
        }
    }
}