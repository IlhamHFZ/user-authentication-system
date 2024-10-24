using FluentValidation;

namespace Application.Features.RoleFeatures.DeleteRole;

public class DeleteRoleValidator : AbstractValidator<DeleteRoleRequest>
{
	public DeleteRoleValidator()
	{
		RuleFor(role => role.Id)
			.NotEmpty();
	}
}
