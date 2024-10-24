using FluentValidation;

namespace Application.Features.RoleFeatures.CreateRole;

public class CreateRoleValidator : AbstractValidator<CreateRoleRequest>
{
	public CreateRoleValidator()
	{
		RuleFor(role => role.Name)
			.NotEmpty()
			.MinimumLength(3)
			.MaximumLength(10)
			.Matches(@"^[a-zA-Z0-9 ]+$")
			.WithMessage("role name can not contain any symbola and number allowed");
	}
}
