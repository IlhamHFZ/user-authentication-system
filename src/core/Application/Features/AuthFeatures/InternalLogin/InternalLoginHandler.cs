using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Features.AuthFeatures.Interface;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.AuthFeatures.InternalLogin;

public class InternalLoginHandler : IInternalLoginHandler
{
	private readonly UserManager<User> _userManager;
	private ILogger<InternalLoginHandler> _logger;
	private IConfiguration _config;

	public InternalLoginHandler(
		UserManager<User> userManager,
		ILogger<InternalLoginHandler> logger,
		IConfiguration config)
	{
		_userManager = userManager;
		_logger = logger;
		_config = config;
	}

	public async Task<InternalLoginResponse?> HandleAsync(InternalLoginRequest request)
	{
		var user = await _userManager.FindByEmailAsync(request.Email);
		var response = new InternalLoginResponse();
		if(user is null)
		{
			response.IsSuccess = false;
			return response;
		}
		
		if(!await _userManager.CheckPasswordAsync(user, request.Password))
		{
			response.IsSuccess = false;
			return response;
		}
		
		var token = await GenerateTokenJwt(user, _config);
		response.IsSuccess = true;
		response.Token = token;
		
		return response;
	}
	
	private async Task<string> GenerateTokenJwt(User user, IConfiguration config)
	{
		var claim = new List<Claim>()
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
			new Claim(JwtRegisteredClaimNames.Email, user.Email),
			new Claim(JwtRegisteredClaimNames.Name, user.UserName)
		};
		
		var roles = await _userManager.GetRolesAsync(user);
		claim.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
		
		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]));
		var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
		
		var token = new JwtSecurityToken(
			issuer: config["Jwt:Issuer"],
			audience: config["Jwt:Audience"],
			claims: claim,
			expires: DateTime.Now.AddHours(1),
			signingCredentials: credential
		);

		var serializeToken = new JwtSecurityTokenHandler().WriteToken(token);
		return serializeToken;
	}
}
