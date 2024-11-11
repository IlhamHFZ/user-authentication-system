using System.Text.RegularExpressions;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;

namespace Application.Commons;

public class CustomRoleValidator : IRoleValidator<Role>
{
	public Task<IdentityResult> ValidateAsync(RoleManager<Role> manager, Role role)
	{
		ICollection<IdentityError> errors = new List<IdentityError>();
		
		var pattern = @"^[a-zA-Z0-9 ]+$";
		Regex regex = new Regex(pattern);
		if(!regex.IsMatch(role.Name))
		{
			errors.Add(new IdentityError()
			{
				Code = "NameAndNumberOnlyContainAlphabet",
				Description = "Name can not contain unique symbol"
			});
		}
		
		if(errors.Any())
		{
			return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
		}
		
		return Task.FromResult(IdentityResult.Success);
	}
}
