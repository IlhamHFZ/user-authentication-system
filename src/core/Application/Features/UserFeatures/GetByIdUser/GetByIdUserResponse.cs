namespace Application.Features.UserFeatures.GetByIdUser;

public record GetByIdUserResponse
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
