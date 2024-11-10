using Application.Features.UserFeatures;
using Application.Features.UserFeatures.CreateUser;
using Application.Features.UserFeatures.DeleteUser;
using Application.Features.UserFeatures.GetAllUser;
using Application.Features.UserFeatures.GetByIdUser;
using Application.Features.UserFeatures.UpdateUser;
using Application.Features.UserFeatures.UpdateUserAddRole;
using Application.Features.UserFeatures.UpdateUserProfile;
using Application.Features.UserFeatures.UpdateUserRemoveRole;
using Application.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;
using Webapi.Models.Errors.ValidationException;
using Webapi.Models.Responses;
using Webapi.Models.Responses.User;

namespace Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UserController : ControllerBase
{
	private readonly IUserFacade _userFacade;
	private readonly ILogger<UserController> _logger;
	private readonly IMapper _mapper;
	public UserController(IUserFacade userFacade, ILogger<UserController> logger, IMapper mapper)
	{
		_userFacade = userFacade;
		_logger = logger;
		_mapper = mapper;
	}

	[HttpGet]
	[ProducesResponseType<ApiResponse<GetAllUserSuccessResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType<ApiResponse<object>>(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetAllUser(
		[FromQuery] string? filterQuery,
		[FromQuery] string? filterOn = "displayname",
		[FromQuery] string? sortBy = "emailconfirmed",
		[FromQuery] bool isAscending = true,
		[FromQuery] int pageSize = 10,
		[FromQuery] int pageCurrent = 1
	)
	{
		_logger.LogInformation("Starting to process GetAllUser request");
		var request = new GetAllUserRequest()
		{
			QueryParameters = new QueryParameters()
			{
				Filtering = new Filtering()
				{
					FilterOn = filterOn,
					FilterQuery = filterQuery,			
				},
				Sorting = new Sorting()
				{
					SortBy = sortBy,
					IsAscending = isAscending,
				},
				Pagination = new Pagination()
				{
					PageSize = pageSize,
					PageCurrent = pageCurrent
				}
			}
		};
		var users = await _userFacade.GetAllUserAsync(request);

		ApiResponse<IEnumerable<GetAllUserSuccessResponse>> response = new ApiResponse<IEnumerable<GetAllUserSuccessResponse>>()
		{
			Data = _mapper.Map<IEnumerable<GetAllUserSuccessResponse>>(users),
			Filtering = request.QueryParameters.Filtering,
			Sorting = request.QueryParameters.Sorting,
			Pagination = request.QueryParameters.Pagination
		};

		if (!users.Any())
		{
			response.Status = StatusCodes.Status404NotFound;
			response.Message = "users not found";

			_logger.LogWarning("No users found");
			return NotFound(response);
		}

		response.Status = StatusCodes.Status200OK;
		response.Message = "success get users";

		_logger.LogInformation($"Successfully retrieved {users.Count()} users");
		return Ok(response);
	}

	[HttpGet("{id}")]
	[ProducesResponseType<ApiResponse<GetByIdUserSuccessResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType<ApiResponse<object>>(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetById([FromRoute] Guid id)
	{
		_logger.LogInformation($"Starting to process GetById request for user with id {id}");
		var user = await _userFacade.GetByIdUserAsync(new GetByIdUserRequest() { Id = id });

		ApiResponse<GetByIdUserSuccessResponse> response = new ApiResponse<GetByIdUserSuccessResponse>()
		{
			Data = _mapper.Map<GetByIdUserSuccessResponse>(user)
		};

		if (user is null)
		{
			response.Status = StatusCodes.Status404NotFound;
			response.Message = "user not found";

			_logger.LogWarning($"User with id {id} not found");
			return NotFound(response);
		}

		response.Status = StatusCodes.Status200OK;
		response.Message = "success get user";

		_logger.LogInformation($"Successfully retrieved user with id {id}");
		return Ok(response);
	}

	[HttpPost]
	[ProducesResponseType<ApiResponse<CreateUserSuccessResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType<ApiResponse<CreateUserFailedResponse>>(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> PostUser([FromBody] CreateUserRequest request)
	{
		_logger.LogInformation($"Starting to process PostUser request for user email {request.Email}");
		var user = await _userFacade.CreateUserAsync(request);

		if(!user.IsSuccess)
		{
			ApiResponse<CreateUserFailedResponse> badRequestResponse = new ApiResponse<CreateUserFailedResponse>()
			{
				Status = StatusCodes.Status400BadRequest,
				Message = "failed created new user",
				Data = _mapper.Map<CreateUserFailedResponse>(user)
			};

			_logger.LogWarning($"User with email {request.Email} failed created new user");
			return BadRequest(badRequestResponse);
		}
		
		ApiResponse<CreateUserSuccessResponse> successResponse = new ApiResponse<CreateUserSuccessResponse>()
		{
			Status = StatusCodes.Status200OK,
			Message = "success created new user",
			Data = _mapper.Map<CreateUserSuccessResponse>(user)
		};
		
		_logger.LogInformation($"User with email {request.Email} successfully created");
		return Ok(successResponse);
	}

	[HttpPatch("addrole")]
	[ProducesResponseType<ApiResponse<UpdateUserAddRoleSuccessResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType<ApiResponse<object>>(StatusCodes.Status404NotFound)]
	[ProducesResponseType<ApiResponse<UpdateUserAddRoleFailedResponse>>(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> PatchUserAddRole([FromBody] UpdateUserAddRoleRequest request)
	{
		_logger.LogInformation($"Starting to process PatchUserAddROle request for user with id {request.UserId}");
		var user = await _userFacade.UpdateUserAddRoleAsync(request);
		if(user is null)
		{
			var notFoundResponse = new ApiResponse<object>()
			{
				Status = StatusCodes.Status404NotFound,
				Message = "user or role not found",
				Data = null
			};
			
			_logger.LogWarning($"User with id {request.UserId} or role with id {request.RoleId} not found");
			return NotFound(notFoundResponse);
		}
		
		if(!user.IsSuccess)
		{
			var badRequestResponse = new ApiResponse<UpdateUserAddRoleFailedResponse>()
			{
				Status = StatusCodes.Status400BadRequest,
				Message = "failed add role to user",
				Data = _mapper.Map<UpdateUserAddRoleFailedResponse>(user)
			};
			
			_logger.LogWarning($"User withd id {request.UserId} failed add role to user");
			return BadRequest(badRequestResponse);
		}
		
		var successResponse = new ApiResponse<UpdateUserAddRoleSuccessResponse>()
		{
			Status = StatusCodes.Status200OK,
			Message = "success",
			Data = _mapper.Map<UpdateUserAddRoleSuccessResponse>(user)
		};
		
		_logger.LogInformation($"Successfully to add role to user with id {request.UserId}");
		return Ok(successResponse);
	}
	
	[HttpPatch("removerole")]
	[ProducesResponseType<ApiResponse<UpdateUserRemoveRoleSuccessResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType<ApiResponse<object>>(StatusCodes.Status404NotFound)]
	[ProducesResponseType<ApiResponse<UpdateUserRemoveRoleFailedResponse>>(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> PatchUserRemoveRole([FromBody] UpdateUserRemoveRoleRequest request)
	{
		_logger.LogInformation($"Starting to process PatchUserRemoveRole request for user with id {request.UserId}");
		var user = await _userFacade.UpdateUserRemoveRoleAsync(request);
		if(user is null)
		{
			var notFoundResponse = new ApiResponse<object>
			{
				Status = StatusCodes.Status404NotFound,
				Message = "user or role not found",
				Data = null
			};
			
			_logger.LogWarning($"User with id {request.UserId} or role with id {request.RoleId} not found");
			return NotFound(notFoundResponse);
		}
		
		if(!user.IsSuccess)
		{
			var badRequestResponse = new ApiResponse<UpdateUserRemoveRoleFailedResponse>()
			{
				Status = StatusCodes.Status400BadRequest,
				Message = "failed add role to user",
				Data = _mapper.Map<UpdateUserRemoveRoleFailedResponse>(user)
			};
			
			_logger.LogWarning($"User withd id {request.UserId} failed add role to user");
			return BadRequest(badRequestResponse);
		}
		
		var successResponse = new ApiResponse<UpdateUserRemoveRoleSuccessResponse>()
		{
			Status = StatusCodes.Status200OK,
			Message = "success",
			Data = _mapper.Map<UpdateUserRemoveRoleSuccessResponse>(user)
		};
		
		_logger.LogInformation($"Successfully to add role to user with id {request.UserId}");
		return Ok(successResponse);
	}
	
	[HttpPatch("profile")]
	[ProducesResponseType<ApiResponse<UpdateUserProfileSuccessResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType<ApiResponse<object>>(StatusCodes.Status404NotFound)]
	[ProducesResponseType<ApiResponse<UpdateUserProfileFailedResponse>>(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> PatchUserProfile([FromBody] UpdateUserProfileRequest request)
	{
		_logger.LogInformation($"Starting to process PatchUSerProfile request for user with id {request.Id}");
		var user = await _userFacade.UpdateUserProfileAsync(request);
		if (user is null)
		{
			var notFoundResponse = new ApiResponse<object>()
			{
				Status = StatusCodes.Status404NotFound,
				Message = "user not found",
				Data = null
			};
			
			_logger.LogWarning($"User with id {request.Id} not found");
			return NotFound(notFoundResponse);
		}
		
		if (!user.IsSuccess)
		{
			var badRequestResponse = new ApiResponse<UpdateUserProfileFailedResponse>()
			{
				Status = StatusCodes.Status400BadRequest,
				Message = "failed update user profile",
				Data = _mapper.Map<UpdateUserProfileFailedResponse>(user)
			};
			
			_logger.LogWarning($"User with id {request.Id} failed update user profile");
			return BadRequest(badRequestResponse);
		}
		
		var successResponse = new ApiResponse<UpdateUserProfileSuccessResponse>()
		{
			Status = StatusCodes.Status200OK,
			Message = "success",
			Data = _mapper.Map<UpdateUserProfileSuccessResponse>(user)
		};
		
		return Ok(successResponse);
	}

	[HttpDelete("{id}")]
	[ProducesResponseType<ApiResponse<DeleteUserSuccessResponse>>(StatusCodes.Status200OK)]
	[ProducesResponseType<ApiResponse<object>>(StatusCodes.Status404NotFound)]
	[ProducesResponseType<ApiResponse<DeleteUserFailedResponse>>(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
	{
		_logger.LogInformation($"Starting to process DeleteUser request for user with id {id}");
		DeleteUserRequest request = new DeleteUserRequest()
		{
			Id = id
		};
		var deleteResult = await _userFacade.DeleteUserAsync(request);
		
		if(deleteResult is null)
		{
			var notFoundResponse = new ApiResponse<object>()
			{
				Status = StatusCodes.Status404NotFound,
				Message = "user not found",
				Data = null
			};
			
			_logger.LogWarning($"User with id {request.Id} not found");
			return NotFound(notFoundResponse);
		}
		
		if(!deleteResult.IsSuccess)
		{
			var badRequestResponse = new ApiResponse<DeleteUserFailedResponse>()
			{
				Status = StatusCodes.Status400BadRequest,
				Message = "failed deleted user",
				Data = _mapper.Map<DeleteUserFailedResponse>(deleteResult)
			};
			
			_logger.LogWarning($"User with id {request.Id} failed deleted");
			return BadRequest(badRequestResponse);
		}
		
		var successResponse = new ApiResponse<DeleteUserSuccessResponse>()
		{
			Status = StatusCodes.Status200OK,
			Message = "success deleted user",
			Data = _mapper.Map<DeleteUserSuccessResponse>(deleteResult)
		};
		
		_logger.LogInformation($"Successfully to delete user with id {request.Id}");
		return Ok(successResponse);
	}
}
