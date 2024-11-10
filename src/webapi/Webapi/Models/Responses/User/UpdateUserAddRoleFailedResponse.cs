using Microsoft.AspNetCore.Identity;

namespace Webapi.Models.Responses.User;

public record UpdateUserAddRoleFailedResponse
{
	public Guid Id { get; set; }
	public string? UserName {get; set;}
	public IEnumerable<string>? RolesName {get; set;}
	public IEnumerable<IdentityError>? Errors {get; set;}
}
