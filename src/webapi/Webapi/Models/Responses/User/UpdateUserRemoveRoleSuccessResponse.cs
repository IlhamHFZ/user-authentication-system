namespace Webapi.Models.Responses.User;

public record UpdateUserRemoveRoleSuccessResponse
{
	public Guid Id { get; set; }
	public string? UserName {get; set;}
	public IEnumerable<string>? RolesName {get; set;}
}
