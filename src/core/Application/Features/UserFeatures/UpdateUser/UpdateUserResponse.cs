using Domain.Entites;

namespace Application.Features.UserFeatures.UpdateUser;

public record class UpdateUserResponse
{
	public string UserName {get; set;}
	public string DisplayName {get; set;}
	public string RoleName {get; set;}
}
