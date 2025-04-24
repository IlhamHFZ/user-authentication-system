namespace Application.Features.AuthFeatures.InternalRegister;

public record InternalRegisterResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
}
