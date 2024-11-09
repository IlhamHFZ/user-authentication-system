namespace Webapi.Models.Responses.User;

public class UpdateUserProfileSuccessResponse
{
	public Guid Id { get; set; }
	public string UserName { get; set; } = null!;
	public string DisplayName { get; set; } = null!;
}
