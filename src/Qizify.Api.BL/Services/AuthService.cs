using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Quizify.Api.BL.Facades.IFacades;
using Quizify.Api.BL.Services.Interfaces;
using Quizify.Common.Models;

namespace Quizify.Api.BL.Services;

public class AuthService : IAuthService
{
    public UserDetailModel? User { get; set; }
    public string? Token { get; }
    
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IAuthFacace _authFacade;
    
    
    public AuthService(IHttpContextAccessor contextAccessor,IAuthFacace authFacace)
    {
        _authFacade = authFacace;
        _contextAccessor = contextAccessor;

        Token = contextAccessor.HttpContext.Request.Headers["Authorization"];
        if (Token.IsNullOrEmpty())
            return;
        
        try
        {
            User = _authFacade.GetById(Guid.Parse(Token));
        }
        catch (Exception e)
        {
            return;
        }
    }

    public bool IsLoggedIn()
    {
        return User != null;
    }
    


    public UserDetailModel? GetUser()
    {
        return User;
    }
}