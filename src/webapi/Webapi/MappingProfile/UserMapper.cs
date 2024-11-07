using Application.Features.UserFeatures.CreateUser;
using AutoMapper;
using Webapi.Models.Responses.User;

namespace Webapi.MappingProfile;

public class UserMapper : Profile
{
	public UserMapper()
	{
		CreateMap<CreateUserResponse, CreateUserFailedResponse>();
		CreateMap<CreateUserResponse, CreateUserSuccessResponse>();
	}
}
