using Application.Features.RoleFeatures.CreateRole;
using Application.Features.RoleFeatures.DeleteRole;
using Application.Features.RoleFeatures.GetAllRole;
using Application.Features.RoleFeatures.GetByIdRole;
using AutoMapper;
using Webapi.Models.Responses.Role;

namespace Webapi.MappingProfile;

public class RoleMapper : Profile
{
	public RoleMapper()
	{
		CreateMap<GetByIdRoleResponse, GetByIdRoleSuccessResponse>();
		
		CreateMap<GetAllRoleResponse, GetAllRoleSuccessResponse>();
		
		CreateMap<DeleteRoleResponse, DeleteRoleSuccessResponse>();
		CreateMap<DeleteRoleResponse, DeleteRoleFailedResponse>();
		
		CreateMap<CreateRoleResponse, CreateRoleSuccessResponse>();
		CreateMap<CreateRoleResponse, CreateRoleFailedResponse>();
	}
}
