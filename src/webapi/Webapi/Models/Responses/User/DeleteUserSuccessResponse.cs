namespace Webapi.Models.Responses.User;

public class DeleteUserSuccessResponse
{
	public Guid Id { get; set; }
	public string Email {get; set;} = null!;
	public string UserName {get; set;} = null!;
	public string DisplayName {get; set;} = null!;
}
