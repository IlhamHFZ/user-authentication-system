using FluentValidation;

namespace Application.Features.RoleFeatures.GetByIdRole;

public class GetByIdRoleValidator : AbstractValidator<GetByIdRoleRequest>
{
	public GetByIdRoleValidator()
	{
		RuleFor(role => role.Id)
			.NotEmpty();
	}
}
