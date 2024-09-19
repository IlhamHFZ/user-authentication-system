using AutoMapper;
using Domain.Entites;

namespace Application.Features.UserFeatures.GetByIdUser;

public class GetByIdUserMapper : Profile
{
	public GetByIdUserMapper()
	{
		CreateMap<User, GetByIdUserResponse>();
	}
}
