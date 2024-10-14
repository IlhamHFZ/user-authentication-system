using FluentValidation;

namespace Application.Features.UserFeatures.DeleteUser;

public class DeleteUserValidator : AbstractValidator<DeleteUserRequest>
{
	public DeleteUserValidator()
	{
		RuleFor(user => user.Id)
			.NotEmpty();
	}
}
