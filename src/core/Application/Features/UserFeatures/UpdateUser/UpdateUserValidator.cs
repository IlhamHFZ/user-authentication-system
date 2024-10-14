using FluentValidation;

namespace Application.Features.UserFeatures.UpdateUser;

public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
	public UpdateUserValidator()
	{
		RuleFor(user => user.UserId)
			.NotEmpty();
		RuleFor(user => user.RoleId)
			.NotEmpty();
	}
}
