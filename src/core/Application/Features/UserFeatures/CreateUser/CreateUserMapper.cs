using AutoMapper;
using Domain.Entites;

namespace Application.Features.UserFeatures.CreateUser;

public class CreateUserMapper : Profile
{
	public CreateUserMapper()
	{
		CreateMap<CreateUserRequest, User>();
	}
}