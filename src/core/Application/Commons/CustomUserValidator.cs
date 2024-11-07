using System.Text.RegularExpressions;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace Application.Commons;

public class CustomUserValidator : IUserValidator<User>
{
	public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
	{
		ICollection<IdentityError> errors = new List<IdentityError>();
		
		if(user.UserName.Length <= 3)
		{
			errors.Add(new IdentityError()
			{
				Code = "UsernameTooShort",
				Description = "Username have minimum length 3 character"
			});
		}
		
		if(user.UserName.Length >= 20)
		{
			errors.Add(new IdentityError()
			{
				Code = "UsernameTooLong",
				Description = "Username have maximum length 20 character"
			});
		}
		
		if(user.DisplayName.Length == 0)
		{
			errors.Add(new IdentityError()
			{
				Code = "DisplaynameRequire",
				Description = "Displayname must be filled"
			});
		}
		
		if(user.DisplayName.Length <= 3)
		{
			errors.Add(new IdentityError()
			{
				Code = "DisplaynameTooShort",
				Description = "Displayname have minimum length 3 character"
			});
		}
		
		if(user.DisplayName.Length >= 20)
		{
			errors.Add(new IdentityError()
			{
				Code = "DisplaynameTooLong",
				Description = "Displayname have maximum length 20 character"
			});
		}
		
		Regex regex = new Regex(@"^[a-zA-Z ]+$");
		if(regex.IsMatch(user.DisplayName))
		{
			errors.Add(new IdentityError()
			{
				Code = "DisplaynameOnlyContainAlphabet",
				Description = "Displayname can not contain number and unique symbol"
			});
		}
		
		if(errors.Any())
		{
			return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
		}
		
		return Task.FromResult(IdentityResult.Success);
	}
}
