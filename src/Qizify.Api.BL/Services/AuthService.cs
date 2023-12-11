using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Quizify.Api.BL.Facades.IFacades;
using Quizify.Api.BL.Services.Interfaces;
using Quizify.Common.Models;

namespace Quizify.Api.BL.Services;

public class AuthService : IAuthService
{
    private const string CookieKey = "generated_uuid";
    public UserDetailModel? User { get; set; }
    public string? Cookie { get; }
    
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IAuthFacace _authFacade;
    
    
    public AuthService(IHttpContextAccessor contextAccessor,IAuthFacace authFacace)
    {
        _authFacade = authFacace;
        _contextAccessor = contextAccessor;
        
        Cookie = contextAccessor.HttpContext.Request.Cookies[CookieKey];
        
        if (Cookie.IsNullOrEmpty())
            return;
        
        
        User = _authFacade.GetById(Guid.Parse(Cookie));
    }

    public bool IsLoggedIn()
    {
        return User != null;
    }

    public void SetCookieToResponse()
    {
        throw new NotImplementedException();
    }

    public void SetCookieToResponse(string cookie,CookieOptions? options)
    {
        CookieOptions option = options ?? new CookieOptions();
        option.Secure = true;
        option.HttpOnly = false;
        option.SameSite = SameSiteMode.None;
        
            
        option.Expires = DateTime.Now.AddHours(1);  
        _contextAccessor.HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials","true");
        _contextAccessor.HttpContext.Response.Cookies.Append(CookieKey, cookie, option);
    }

    public string GetCookie()
    {
        throw new NotImplementedException();
    }

    public UserDetailModel? GetUser()
    {
        return User;
    }
}