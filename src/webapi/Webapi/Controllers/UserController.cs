using Application.Features.UserFeatures;
using Application.Features.UserFeatures.CreateUser;
using Application.Features.UserFeatures.DeleteUser;
using Application.Features.UserFeatures.GetAllUser;
using Application.Features.UserFeatures.GetByIdUser;
using Application.Features.UserFeatures.UpdateUser;
using Application.Features.UserFeatures.UpdateUserProfile;
using Microsoft.AspNetCore.Mvc;

namespace Webapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
	private readonly IUserFacade _userFacade;

	public UserController(IUserFacade userFacade)
	{
		_userFacade = userFacade;
	}
	
	[HttpGet]
	public async Task<ActionResult<IEnumerable<GetAllUserResponse>>> GetAllUser()
	{
		var users = await _userFacade.GetAllUserAsync();
		if(users is null)
		{
			return BadRequest();
		}
		return Ok(users);
	}
	
	[HttpGet("{id}")]
	public async Task<ActionResult<GetByIdUserResponse>> GetById([FromRoute]Guid id)
	{
		var user = await _userFacade.GetByIdUserAsync(new GetByIdUserRequest(){Id = id});
		if(user is null)
		{
			return BadRequest();
		}
		
		return Ok(user);
	}
	
	[HttpPost]
	public async Task<ActionResult<CreateUserResponse>> PostUser([FromBody] CreateUserRequest request)
	{
		var userRequest = await _userFacade.CreateUserAsync(request);
		
		return Ok();
	}
	
	[HttpPatch]
	public async Task<ActionResult<UpdateUserResponse>> PatchUser([FromBody] UpdateUserRequest request)
	{
		var user = await _userFacade.UpdateUserAsync(request);
		if(user is null)
		{
			return BadRequest();
		}
		
		return Ok();
	}
	
	[HttpPatch("profile")]
	public async Task<ActionResult<UpdateUserProfileResponse>> PatchUserProfile([FromBody] UpdateUserProfileRequest request)
	{
		var user = await _userFacade.UpdateUserProfileAsync(request);
		if(user is null)
		{
			return BadRequest();
		}
		
		return Ok();
	}
	
	[HttpDelete("{id}")]
	public async Task<ActionResult<DeleteUserResponse>> DeleteUser([FromRoute] Guid id)
	{
		var user = await _userFacade.DeleteUserAsync(new DeleteUserRequest(){Id = id});
		if(user is null)
		{
			return BadRequest();
		}
		
		return Ok();
	}
}
