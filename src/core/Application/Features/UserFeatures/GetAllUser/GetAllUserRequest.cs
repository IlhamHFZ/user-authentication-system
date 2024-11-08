using Application.Shared;

namespace Application.Features.UserFeatures.GetAllUser;

public record GetAllUserRequest
{
	public QueryParameters QueryParameters {get; set;} = new QueryParameters();
}
