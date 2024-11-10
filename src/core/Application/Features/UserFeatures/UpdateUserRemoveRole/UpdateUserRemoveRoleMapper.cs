using AutoMapper;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.UserFeatures.UpdateUserRemoveRole;

public class UpdateUserRemoveRoleMapper : Profile
{
	public UpdateUserRemoveRoleMapper()
	{
		CreateMap<User, UpdateUserRemoveRoleResponse>();
		CreateMap<IdentityResult, UpdateUserRemoveRoleResponse>()
			.ForMember(dest => dest.IsSuccess, opt => opt.MapFrom(src => src.Succeeded));
	}
}
