using Application.Features.UserFeatures.GetAllUser;

namespace Application.Features.UserFeatures.Interface;

public interface IGetAllUserHandler
{
	Task<IEnumerable<GetAllUserResponse>?> HandleAsync();
}
