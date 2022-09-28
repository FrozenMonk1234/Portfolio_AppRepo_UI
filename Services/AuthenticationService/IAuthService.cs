using Portfolio_AppRepo_UI.Models;
using System.Net;

namespace Portfolio_AppRepo_UI.Services.AuthenticationService
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginModel model);
        Task<bool> LogoutAsync();
    }
}
