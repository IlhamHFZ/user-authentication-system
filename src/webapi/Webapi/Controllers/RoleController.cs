using Application.Features.RoleFeatures;
using Application.Features.RoleFeatures.CreateRole;
using Application.Features.RoleFeatures.DeleteRole;
using Application.Features.RoleFeatures.GetAllRole;
using Application.Features.RoleFeatures.GetByIdRole;
using Microsoft.AspNetCore.Mvc;
using Webapi.Models;
using Webapi.Models.ErrorResponse;

namespace Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class RoleController : ControllerBase
{
	private readonly IRoleFacade _roleFacade;
	private readonly ILogger<RoleController> _logger;

	public RoleController(IRoleFacade roleFacade, ILogger<RoleController> logger)
	{
		_roleFacade = roleFacade;
		_logger = logger;
	}

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType<ApiResponse<object>>(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<ApiResponse<IEnumerable<GetAllRoleResponse>>>> GetAllRole()
	{
		_logger.LogInformation("Starting to process GetAllRole request");
		var roles = await _roleFacade.GetAllRoleAsync();
		
		ApiResponse<IEnumerable<GetAllRoleResponse>> response = new ApiResponse<IEnumerable<GetAllRoleResponse>>()
		{
			Data = roles
		};
		
		if(roles is null)
		{
			response.Status = StatusCodes.Status404NotFound;
			response.Message = "roles not found";
			
			_logger.LogWarning("Roles not found");
			return NotFound(response);
		}
		
		response.Status = StatusCodes.Status200OK;
		response.Message = "sucess get roles";
		
		_logger.LogInformation($"Successfully retrieve {roles.Count()} roles");
		return Ok(response);
	}
	
	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType<ApiResponse<object>>(StatusCodes.Status404NotFound)]
	[ProducesResponseType<ValidationErrorResponse>(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<ApiResponse<GetByIdRoleResponse>>> GetByIdRole(Guid id)
	{
		_logger.LogInformation($"Starting to process GetByIdRole request for role with id {id}");
		var role = await _roleFacade.GetByIdRoleAsync(new GetByIdRoleRequest(){Id = id});
		
		ApiResponse<GetByIdRoleResponse> response = new ApiResponse<GetByIdRoleResponse>()
		{
			Data = role
		};
		
		if(role is null)
		{
			response.Status = StatusCodes.Status404NotFound;
			response.Message = "role not found";
			
			_logger.LogWarning($"Role with id {id} not found");
			return NotFound(response);
		}
		
		response.Status = StatusCodes.Status200OK;
		response.Message = "success get role";
		
		_logger.LogInformation($"Successfully retrieve user with id {id}");
		return Ok(response);
	}
	
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType<ValidationErrorResponse>(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<ApiResponse<CreateRoleResponse>>> CreateRole([FromBody] CreateRoleRequest request)
	{
		_logger.LogInformation($"Starting to process CreateRole request for role with name {request.Name}");
		var role = await _roleFacade.CreateRoleAsync(request);
		
		ApiResponse<CreateRoleResponse> response = new ApiResponse<CreateRoleResponse>()
		{
			Status = StatusCodes.Status200OK,
			Message = "success created new role",
			Data = role
		};
		
		_logger.LogInformation($"Role with name {request.Name} successfully created");
		return Ok(response);
	}
	
	[HttpDelete("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType<ApiResponse<object>>(StatusCodes.Status404NotFound)]
	[ProducesResponseType<ValidationErrorResponse>(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<ApiResponse<DeleteRoleResponse>>> DeleteRole(Guid id)
	{
		_logger.LogInformation($"Starting to process DeleteUser request for role with id {id}");
		var role = await _roleFacade.DeleteRoleAsync(new DeleteRoleRequest(){Id = id});
		
		ApiResponse<DeleteRoleResponse> response = new ApiResponse<DeleteRoleResponse>()
		{
			Data = role
		};
		
		if(role is null)
		{
			response.Status = StatusCodes.Status404NotFound;
			response.Message = "role not found";
			
			_logger.LogWarning($"Role with id {id} not found");
			return NotFound(response);
		}
		
		response.Status = StatusCodes.Status200OK;
		response.Message = "success deleted role";
		
		_logger.LogInformation($"Successfully deleted user with id {id}");
		return Ok(response);
	}
}
