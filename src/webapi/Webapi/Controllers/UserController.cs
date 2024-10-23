using System.Net;
using Application.Features.UserFeatures;
using Application.Features.UserFeatures.CreateUser;
using Application.Features.UserFeatures.DeleteUser;
using Application.Features.UserFeatures.GetAllUser;
using Application.Features.UserFeatures.GetByIdUser;
using Application.Features.UserFeatures.UpdateUser;
using Application.Features.UserFeatures.UpdateUserProfile;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
	public async Task<ActionResult<IEnumerable<GetAllUserResponse>>> GetAllUser()
	{
		_logger.LogInformation("Starting to process GetAllUser request");
		var users = await _userFacade.GetAllUserAsync();
		if(users is null)
		{
			_logger.LogWarning("No users found");
			return NotFound("list of user not found");
		}
		
		_logger.LogInformation($"Successfully retrieved {users.Count()} users");
		return Ok(users);
	}
	
	[HttpGet("{id}")]
	public async Task<ActionResult<GetByIdUserResponse>> GetById([FromRoute]Guid id)
	{
		_logger.LogInformation($"Starting to process GetById request for user with id {id}");
		var user = await _userFacade.GetByIdUserAsync(new GetByIdUserRequest(){Id = id});
		if(user is null)
		{
			_logger.LogWarning($"User with id {id} not found");
			return NotFound("user not found");
		}
		
		_logger.LogInformation($"Successfully retrieved user with id {id}");
		return Ok(user);
	}
	
	[HttpPost]
	public async Task<ActionResult<CreateUserResponse>> PostUser([FromBody] CreateUserRequest request)
	{
		_logger.LogInformation($"Starting to process PostUser request for user email {request.Email}");
		var user = await _userFacade.CreateUserAsync(request);
		
		_logger.LogInformation($"User with email {request.Email} successfully created");
		return Ok(user);
	}
	
	[HttpPatch]
	public async Task<ActionResult<UpdateUserResponse>> PatchUser([FromBody] UpdateUserRequest request)
	{
		_logger.LogInformation($"Starting to process PatchUser request for user with id {request.UserId}");
		var user = await _userFacade.UpdateUserAsync(request);
		if(user is null)
		{
			_logger.LogWarning($"User with id {request.UserId} not found");
			return NotFound("user or role not found");
		}
		
		_logger.LogInformation($"Successfully updated user with id {request.UserId}");
		return Ok(user);
	}
	
	[HttpPatch("profile")]
	public async Task<ActionResult<UpdateUserProfileResponse>> PatchUserProfile([FromBody] UpdateUserProfileRequest request)
	{
		_logger.LogInformation($"Starting to process PatchUSerProfile request for user with id {request.GetType}");
		var user = await _userFacade.UpdateUserProfileAsync(request);
		if(user is null)
		{
			_logger.LogWarning($"User with id {request.Id} not found");
			return NotFound("user not found");
		}
		
		_logger.LogInformation($"Successfully updated profile user with id {request.Id}");
		return Ok(user);
	}
	
	[HttpDelete("{id}")]
	public async Task<ActionResult<DeleteUserResponse>> DeleteUser([FromRoute] Guid id)
	{
		_logger.LogInformation($"Starting to process DeleteUser request for user with id {id}");
		var user = await _userFacade.DeleteUserAsync(new DeleteUserRequest(){Id = id});
		if(user is null)
		{
			_logger.LogWarning($"User with id {id} not found");
			return NotFound("user not found");
		}
		
		_logger.LogInformation($"Successfully deleted user with id {id}");
		return Ok(user);
	}
}
