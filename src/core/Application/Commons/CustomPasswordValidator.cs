using System.Text.RegularExpressions;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace Application.Commons;

public class CustomPasswordValidator : IPasswordValidator<User>
{
	public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string? password)
	{
		ICollection<IdentityError> errors = new List<IdentityError>();
		
		if(password.Contains(user.UserName, StringComparison.OrdinalIgnoreCase))
		{
			errors.Add(new IdentityError()
			{
				Code = "PasswordContainUserName",
				Description = "Password can not contain username"
			});
		}
		
		string pattern = @"^(?!.*(?i)((a|4|@)(|d|&)m(i|!|1)n|p(a|@|4)(s|$|5)(s|$|5)w(o|0)r(d|&)|w(o|0)r(d|&))).*$";
		Regex regex = new Regex(pattern);
		if(!regex.IsMatch(password))
		{
			errors.Add(new IdentityError()
			{
				Code = "PasswordContainAdminAndPasswordWord",
				Description = "Passwrod can not contain admin and password word with any combination by number and symbol"
			});
		}
		
		if(errors.Any())
		{
			return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
		}
		
		return Task.FromResult(IdentityResult.Success);
	}
}
