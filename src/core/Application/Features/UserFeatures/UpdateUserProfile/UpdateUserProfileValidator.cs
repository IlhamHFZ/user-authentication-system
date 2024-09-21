using FluentValidation;

namespace Application.Features.UserFeatures.UpdateUserProfile;

public class UpdateUserProfileValidator : AbstractValidator<UpdateUserProfileRequest>
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
