using Application.Features.AuthFeatures.Interface;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Features.AuthFeatures.InternalRegister;

public class InternalRegisterHandler : IInternalRegisterHandler
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<IInternalRegisterHandler> _logger;
    private readonly IPasswordValidator<User> _passwordValidator;
    private readonly IConfiguration _config;

    public InternalRegisterHandler(IConfiguration config, ILogger<IInternalRegisterHandler> logger, UserManager<User> userManager, IPasswordValidator<User> passwordValidator)
    {
        _logger = logger;
        _config = config;
        _passwordValidator = passwordValidator;
        _userManager = userManager;

    }


    public async Task<InternalRegisterResponse> HandleAsync(InternalRegisterRequest request)
    {
        // cek apakah email tidak terdaftar
        InternalRegisterResponse response = new InternalRegisterResponse();
        
        var isEmailExist = _userManager.Users.Where(x => x.Email == request.Email).Any();
        if (isEmailExist)
        {
            _logger.LogInformation($"Email {request.Email} has been registered");
            response.IsSuccess = false;
            response.Message = "Email has registered";
            return response;
        }
        // jika iya, lanjut register
        
        User newUser = new User()
        {
            UserName = request.UserName,
            NormalizedUserName = request.UserName.ToUpper(),
            Email = request.Email,
            NormalizedEmail = request.Email.ToUpper(),
            DisplayName = request.DisplayName,
            NormalizeDisplayName = request.DisplayName.ToUpper()
        };
        
        var isPasswordValid = await _passwordValidator.ValidateAsync(_userManager, newUser, request.Password);
        // cek kevalidan password
        // jika iya, lanjut register
        // jika tidak, return error password tidak valid
        
        // validasi apakah paswword sama dengan confirm password
        // jika iya, lanjut register
        // jika tidak, return error password tidak sama
        
        // cek username valid
        
        // cek display name valid
        
        
    }
}
