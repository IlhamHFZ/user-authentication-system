namespace Application.Features.UserFeatures.GetAllUser;

public record GetAllUserResponse
{
	public Guid Id { get; set; }
	public string UserName { get; set; } = null!;
	public string DisplayName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public bool EmailConfirmed { get; set; }
	public string? PhoneNumber {get; set; }
	public bool PhoneConfirmed { get; set; }
	public bool TwoFactorEnable { get; set; }
}