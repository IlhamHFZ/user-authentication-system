using Application.Features.UserFeatures;
using Application.Features.UserFeatures.CreateUser;
using Application.Features.UserFeatures.DeleteUser;
using Application.Features.UserFeatures.GetAllUser;
using Application.Features.UserFeatures.GetByIdUser;
using Application.Features.UserFeatures.UpdateUser;
using Application.Features.UserFeatures.UpdateUserProfile;
using Microsoft.AspNetCore.Mvc;
using Webapi.Models;
using Webapi.Models.ErrorResponse;

namespace Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UserController : ControllerBase
{
	private readonly IUserFacade _userFacade;
	private readonly ILogger<UserController> _logger;
	public UserController(IUserFacade userFacade, ILogger<UserController> logger)
	{
		_userFacade = userFacade;
		_logger = logger;
	}

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType<ApiResponse<object>>(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<ApiResponse<IEnumerable<GetAllUserResponse>>>> GetAllUser()
	{
		_logger.LogInformation("Starting to process GetAllUser request");
		var users = await _userFacade.GetAllUserAsync();

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
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType<ValidationErrorResponse>(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<ApiResponse<CreateUserResponse>>> PostUser([FromBody] CreateUserRequest request)
	{
		_logger.LogInformation($"Starting to process PostUser request for user email {request.Email}");
		var user = await _userFacade.CreateUserAsync(request);

		ApiResponse<CreateUserResponse> response = new ApiResponse<CreateUserResponse>()
		{
			Status = StatusCodes.Status200OK,
			Message = "success created new user",
			Data = user
		};

		_logger.LogInformation($"User with email {request.Email} successfully created");
		return Ok(response);
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
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType<ApiResponse<object>>(StatusCodes.Status404NotFound)]
	[ProducesResponseType<ValidationErrorResponse>(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult<ApiResponse<DeleteUserResponse>>> DeleteUser([FromRoute] Guid id)
	{
		_logger.LogInformation($"Starting to process DeleteUser request for user with id {id}");
		var user = await _userFacade.DeleteUserAsync(new DeleteUserRequest() { Id = id });
		
		ApiResponse<DeleteUserResponse> response = new ApiResponse<DeleteUserResponse>()
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
		response.Message = "success deleted user";
		
		_logger.LogInformation($"Successfully deleted user with id {id}");
		return Ok(user);
	}
}
