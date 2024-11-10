using Microsoft.AspNetCore.Identity;

namespace Application.Features.UserFeatures.UpdateUserRemoveRole;

public record UpdateUserRemoveRoleResponse
{
	public Guid Id { get; set; }
	public string? UserName {get; set;}
	public IEnumerable<string>? RolesName {get; set;}
	public bool IsSuccess {get; set;}
	public IEnumerable<IdentityError>? Errors {get; set;}
}
