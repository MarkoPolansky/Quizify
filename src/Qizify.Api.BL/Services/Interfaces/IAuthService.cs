using Microsoft.AspNetCore.Http;
using Quizify.Common.Models;

namespace Quizify.Api.BL.Services.Interfaces;

public interface IAuthService
{
    bool IsLoggedIn();
    
    UserDetailModel? GetUser();

}