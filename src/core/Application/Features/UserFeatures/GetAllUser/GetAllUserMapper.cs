using AutoMapper;
using Domain.Entites;

namespace Application.Features.UserFeatures.GetAllUser;

public class GetAllUserMapper : Profile
{
	public GetAllUserMapper()
    {
    	CreateMap<User, GetAllUserResponse>();
    }
}
