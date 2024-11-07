using Application.Features.UserFeatures.CreateUser;
using Application.Features.UserFeatures.DeleteUser;
using AutoMapper;
using Webapi.Models.Responses.User;

namespace Webapi.MappingProfile;

public class UserMapper : Profile
{
	public UserMapper()
	{
		CreateMap<CreateUserResponse, CreateUserFailedResponse>();
		CreateMap<CreateUserResponse, CreateUserSuccessResponse>();
		
		CreateMap<DeleteUserResponse, DeleteUserFailedResponse>();
		CreateMap<DeleteUserResponse, DeleteUserSuccessResponse>();
	}
}
