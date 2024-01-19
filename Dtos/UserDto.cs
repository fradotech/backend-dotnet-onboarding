using FluentValidation;
using Iam.Models; 

namespace Iam.Validators;
public class UserDto : AbstractValidator<User>
{
    public UserDto()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
    }
}