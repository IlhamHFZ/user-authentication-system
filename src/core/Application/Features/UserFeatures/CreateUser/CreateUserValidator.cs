using Domain.Entites;
using FluentValidation;

namespace Application.Features.UserFeatures.CreateUser;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
	public CreateUserValidator()
	{
		RuleFor(user => user.Email)
			.NotEmpty()
			.EmailAddress();
		RuleFor(user => user.UserName)
			.NotEmpty()
			.MinimumLength(3)
			.MaximumLength(20)
			.Matches(@"^[a-zA-Z ]+$")
			.WithMessage("username can not contain number and special symbol");
		RuleFor(user => user.DisplayName)
			.NotEmpty()
			.MinimumLength(3)
			.MaximumLength(20);
		RuleFor(user => user.Password)
			.NotEmpty()
			.MinimumLength(8)
			.Matches(@"^(?=.*[A-Z])(?=.*[0-9])(?=.*[^\w\s\\/])(?=.{8,})(?!.*([aA][dD][mM][iI][nN]|[pP][aA][sS][sS][wW][oO][rR][dD])).*$")
			.WithMessage("password must contain minimal a number, a letter uppercase, special character, and can not contain 'admin' and 'password' any combination");
	}
}
