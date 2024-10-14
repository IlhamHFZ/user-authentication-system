using FluentValidation;

namespace Application.Features.UserFeatures.GetByIdUser;

public class GetByIdUserValidator : AbstractValidator<GetByIdUserRequest>
{
	public GetByIdUserValidator()
	{
		RuleFor(user => user.Id)
			.NotEmpty();
	}
}
