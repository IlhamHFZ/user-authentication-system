using Application.Features.RoleFeatures;
using Application.Features.RoleFeatures.CreateRole;
using Application.Features.RoleFeatures.DeleteRole;
using Application.Features.RoleFeatures.GetAllRole;
using Application.Features.RoleFeatures.GetByIdRole;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Webapi.Models.Errors.ValidationException;
using Webapi.Models.Responses;
using Webapi.Models.Responses.Role;

namespace Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class RoleController : ControllerBase
{
	private readonly IRoleFacade _roleFacade;
	private readonly ILogger<RoleController> _logger;
	private readonly IMapper _mapper;

	public RoleController(
		IRoleFacade roleFacade, 
		ILogger<RoleController> logger,
		IMapper mapper)
	{
		_roleFacade = roleFacade;
		_logger = logger;
		_mapper = mapper;
	}

	[HttpGet]
	[ProducesResponseType<ApiResponse<IEnumerable<GetAllRoleSuccessResponse>>>(StatusCodes.Status200OK)]
	[ProducesResponseType<ApiResponse<object>>(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetAllRole()
	{
		_logger.LogInformation("Starting to process GetAllRole request");
		var roles = await _roleFacade.GetAllRoleAsync();
		if(!roles.Any())
		{
			var notFoundResponse = new ApiResponse<object>()
			{
				Status = StatusCodes.Status404NotFound,
				Message = "roles not found",
				Data = null
			};
			
			_logger.LogWarning("Roles not found");
			return NotFound(notFoundResponse);
		}
		
		var successResponse = new ApiResponse<IEnumerable<GetAllRoleSuccessResponse>>()
		{
			Status = StatusCodes.Status200OK,
			Message = "success",
			Data = _mapper.Map<IEnumerable<GetAllRoleSuccessResponse>>(roles)
		};
		
		_logger.LogInformation($"Successfully retrieve {roles.Count()} roles");
		return Ok(successResponse);
	}
	
	[HttpGet("{id}")]
	[ProducesResponseType<ApiResponse<GetByIdRoleSuccessResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType<ApiResponse<object>>(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetByIdRole(Guid id)
	{
		
		_logger.LogWarning($"Starting to preccess GetByIdRole request for role with di {id}");
		var role = await _roleFacade.GetByIdRoleAsync(new GetByIdRoleRequest(){Id = id});
		if(role is null)
		{
			var notFoundResponse = new ApiResponse<object>()
			{
				Status = StatusCodes.Status404NotFound,
				Message = "role not found",
				Data = null
			};
			
			_logger.LogWarning($"role with id {id} not found");
			return NotFound(notFoundResponse);
		}
		
		var successResponse = new ApiResponse<GetByIdRoleSuccessResponse>()
		{
			Status = StatusCodes.Status200OK,
			Message = "success",
			Data = _mapper.Map<GetByIdRoleSuccessResponse>(role)
		};
		
		_logger.LogInformation($"Successfully retrieved role with id {id}");
		return Ok(successResponse);
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
