using Microsoft.AspNetCore.Identity;

namespace Domain.Entites;

public class User : IdentityUser<Guid>
{
	public string DisplayName {get; set;} = null!;
	public string NormalizeDisplayName {get; set;} = null!;
}
