using System.Text.RegularExpressions;
using ApiProtection.Models;
using FluentValidation;

namespace ApiProtection.Validation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.FirstName).NotNull();
        RuleFor(x => x.LastName).NotNull();

        RuleFor(x => x.Id).GreaterThan(1).LessThan(int.MaxValue);
        RuleFor(x => x.FirstName).MinimumLength(5).MaximumLength(10);
        RuleFor(x => x.LastName).MaximumLength(5).MaximumLength(10);

        RuleFor(x => x.PhoneNumber)
            .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
            .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
            .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("PhoneNumber not valid");
        
        RuleFor(x => x.EmailAddress).EmailAddress();

        RuleFor(x => x.NumberOfVehicles).GreaterThan(0).LessThan(5);
    }
}