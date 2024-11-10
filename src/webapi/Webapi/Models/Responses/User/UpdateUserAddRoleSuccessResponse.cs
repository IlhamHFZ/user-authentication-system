namespace Webapi.Models.Responses.User;

public record UpdateUserAddRoleSuccessResponse
{
	public Guid Id { get; set; }
	public string? UserName {get; set;}
	public IEnumerable<string>? RolesName {get; set;}
}
