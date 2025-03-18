namespace Application.Features.AuthFeatures.InternalRegister;

public record InternalRegisterRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    
}
