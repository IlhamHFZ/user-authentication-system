using AutoMapper;
using Domain.Entites;

namespace Application.Features.UserFeatures.DeleteUser;

public class DeleteUserMapper : Profile
{
	public DeleteUserMapper()
	{
		CreateMap<User, DeleteUserResponse>();
	}
}
