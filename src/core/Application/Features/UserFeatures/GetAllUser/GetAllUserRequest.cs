namespace Application.Features.UserFeatures.GetAllUser;

public record GetAllUserRequest
{
	public string? FilterOn {get; set;}
	public string? FilterQuery {get; set;}
	public string? SortBy {get; set;}
	public bool IsAscending {get; set;}
	public int PageSize {get; set;}
	public int PageCurrent {get; set;}
}
