using Domain.Entites;
using FluentValidation;

namespace Application.Features.UserFeatures.UpdateUser;

public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
	public UpdateUserValidator()
	{
		RuleFor(user => user.UserName)
			.NotEmpty()
			.MinimumLength(3)
			.MaximumLength(20)
			.Matches(@"^[a-zA-Z]+$");
		RuleFor(user => user.DisplayName)
			.NotEmpty()
			.MinimumLength(3)
			.MaximumLength(10);
	}
}
