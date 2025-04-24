using Application.Features.AuthFeatures.Interface;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Features.AuthFeatures.InternalRegister;

public class InternalRegisterHandler : IInternalRegisterHandler
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<IInternalRegisterHandler> _logger;
    private readonly IConfiguration _config;

    public InternalRegisterHandler(IConfiguration config, ILogger<IInternalRegisterHandler> logger, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _config = config;
        _userManager = userManager;
    }


    public Task<InternalRegisterResponse> HandleAsync(InternalRegisterRequest request)
    {
        // cek apakah email tidak terdaftar
        var isEmailExist = _userManager.Users.Where(x => x.Email == request.Email).Any();
        if (isEmailExist)
        {
            _logger.LogInformation($"Email {request.Email} has been registered");
            return Task.FromResult(new InternalRegisterResponse
            {
                IsSuccess = false,
                Message = "Email has registered"
            });
        }
        // jika iya, lanjut register
        
        
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
