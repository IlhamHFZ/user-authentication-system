using Application.Features.RoleFeatures.GetByIdRole;
using AutoMapper;
using Webapi.Models.Responses.Role;

namespace Webapi.MappingProfile;

public class RoleMapper : Profile
{
	public RoleMapper()
	{
		CreateMap<GetByIdRoleResponse, GetByIdRoleSuccessResponse>();
	}
}
