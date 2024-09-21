using FluentValidation;

namespace Application.Features.UserFeatures.UpdateUser;

public class UpdateUserProfileValidator : AbstractValidator<UpdateUserRequest>
{
	public UpdateUserProfileValidator()
	{
		RuleFor(user => user.UserName)
			.MinimumLength(3)
			.MaximumLength(20)
			.Matches(@"^[a-zA-Z]+$");
		RuleFor(user => user.DisplayName)
			.MinimumLength(3)
			.MaximumLength(10);
	}
}
