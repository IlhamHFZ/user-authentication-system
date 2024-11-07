using Application.Features.UserFeatures;
using Application.Features.UserFeatures.CreateUser;
using Application.Features.UserFeatures.DeleteUser;
using Application.Features.UserFeatures.GetAllUser;
using Application.Features.UserFeatures.GetByIdUser;
using Application.Features.UserFeatures.UpdateUser;
using Application.Features.UserFeatures.UpdateUserProfile;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType<ApiResponse<object>>(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<ApiResponse<IEnumerable<GetAllUserResponse>>>> GetAllUser(
		[FromQuery] string? filterOn,
		[FromQuery] string? filterQuery,
		[FromQuery] string sortBy = "username",
		[FromQuery] bool isAscending = true,
		[FromQuery] int pageSize = 10,
		[FromQuery] int pageCurrent = 1
	)
	{
		
		_logger.LogInformation("Starting to process GetAllUser request");
		var request = new GetAllUserRequest()
		{
			FilterOn = filterOn,
			FilterQuery = filterQuery,
			SortBy = sortBy,
			IsAscending = isAscending,
			PageSize = pageSize,
			PageCurrent = pageCurrent
		};
		var users = await _userFacade.GetAllUserAsync(request);

		ApiResponse<IEnumerable<GetAllUserResponse>> response = new ApiResponse<IEnumerable<GetAllUserResponse>>()
		{
			Data = users
		};

		if (users is null)
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
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType<ApiResponse<object>>(StatusCodes.Status404NotFound)]
	[ProducesResponseType<ValidationErrorResponse>(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<ApiResponse<GetByIdUserResponse>>> GetById([FromRoute] Guid id)
	{
		_logger.LogInformation($"Starting to process GetById request for user with id {id}");
		var user = await _userFacade.GetByIdUserAsync(new GetByIdUserRequest() { Id = id });

		ApiResponse<GetByIdUserResponse> response = new ApiResponse<GetByIdUserResponse>()
		{
			Data = user
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

	[HttpPatch]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType<ApiResponse<object>>(StatusCodes.Status404NotFound)]
	[ProducesResponseType<ValidationErrorResponse>(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<ApiResponse<UpdateUserResponse>>> PatchUser([FromBody] UpdateUserRequest request)
	{
		_logger.LogInformation($"Starting to process PatchUser request for user with id {request.UserId}");
		var user = await _userFacade.UpdateUserAsync(request);

		ApiResponse<UpdateUserResponse> response = new ApiResponse<UpdateUserResponse>()
		{
			Data = user
		};

		if (user is null)
		{

			response.Status = StatusCodes.Status404NotFound;
			response.Message = "user or role not found";

			_logger.LogWarning($"User with id {request.UserId} not found");
			return NotFound(response);
		}

		response.Status = StatusCodes.Status200OK;
		response.Message = "success updated user";

		_logger.LogInformation($"Successfully updated user with id {request.UserId}");
		return Ok(response);
	}

	[HttpPatch("profile")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType<ApiResponse<object>>(StatusCodes.Status404NotFound)]
	[ProducesResponseType<ValidationErrorResponse>(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<ApiResponse<UpdateUserProfileResponse>>> PatchUserProfile([FromBody] UpdateUserProfileRequest request)
	{
		_logger.LogInformation($"Starting to process PatchUSerProfile request for user with id {request.GetType}");
		var user = await _userFacade.UpdateUserProfileAsync(request);

		ApiResponse<UpdateUserProfileResponse> response = new ApiResponse<UpdateUserProfileResponse>()
		{
			Data = user
		};

		if (user is null)
		{
			response.Status = StatusCodes.Status404NotFound;
			response.Message = "user not found";
			
			_logger.LogWarning($"User with id {request.Id} not found");
			return NotFound(response);
		}

		response.Status = StatusCodes.Status200OK;
		response.Message = "success updated profile user";
		
		_logger.LogInformation($"Successfully updated profile user with id {request.Id}");
		return Ok(response);
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
