using Microsoft.AspNetCore.Identity;

namespace Domain.Entites;

public class User : IdentityUser<Guid>
{
	public string DisplayName {get; set;}
}
