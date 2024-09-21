using AutoMapper;
using Domain.Entites;

namespace Application.Features.UserFeatures.UpdateUser;

public class UpdateUserMapper : Profile
{
	public UpdateUserMapper()
	{
		CreateMap<UpdateUserRequest, User>();
		CreateMap<User, UpdateUserResponse>();
	}
}
