namespace Webapi.Models.Responses.Role;

public record CreateRoleSuccessResponse
{
	public Guid Id {get; set;}
	public string RoleName {get; set;} = null!;
}
