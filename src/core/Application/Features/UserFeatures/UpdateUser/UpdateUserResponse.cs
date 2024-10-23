using Domain.Entites;

namespace Application.Features.UserFeatures.UpdateUser;

public record class UpdateUserResponse
{
	public Guid Id { get; set; }
	public string UserName {get; set;} = null!;
	public string DisplayName {get; set;} = null!;
	public string RoleName {get; set;} = null!;
}
