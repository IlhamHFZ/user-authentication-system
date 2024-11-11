namespace Webapi.Models.Responses.Role;

public record DeleteRoleSuccessResponse
{
	public Guid Id {get; set;}
	public string RoleName {get; set;} = null!;
}
