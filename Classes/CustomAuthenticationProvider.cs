using Portfolio_AppRepo_UI.Models;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Portfolio_AppRepo_UI.Classes
{
    public class CustomAuthenticationProvider : AuthenticationStateProvider
    {
        private ISessionStorageService _sessionStorage;
        public CustomAuthenticationProvider(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public static string Username { get; set; }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userDetail = await _sessionStorage.GetItemAsync<UserModel>("UserCredit");
                if (userDetail == null)
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }
                else
                {

                    var claims = await ParseClaims(userDetail);
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "apiauth_type"));
                    return new AuthenticationState(user);

                }
            }
            catch (Exception)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

        }

        private Task<List<Claim>> ParseClaims(UserModel userModel)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userModel.Username));

            if (userModel.Role != null)
            {
                if (userModel.IsActive == true)
                {
                    var x = userModel.Role.Trim().ToLower();
                    claims.Add(new Claim(ClaimTypes.Role, x));
                }
            }
            Username = userModel.Username;
            return Task.FromResult(claims);
        }

        public async Task LoggedIn()
        {
            var userDetail = await _sessionStorage.GetItemAsync<UserModel>("UserCredit");

            var claims = await ParseClaims(userDetail);

            if (userDetail.IsActive == true || userDetail.Id > 0)
            {
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "apiauth_type"));
                var authState = Task.FromResult(new AuthenticationState(user));
                NotifyAuthenticationStateChanged(authState);
            }
            else
            {
                userDetail = new UserModel();
                var nobody = new ClaimsPrincipal(new ClaimsIdentity());
                var authState = Task.FromResult(new AuthenticationState(nobody));
                NotifyAuthenticationStateChanged(authState);

                throw new Exception("No Access, Contact your administrator");
            }
        }

        public async Task LoggedoutAsync()
        {
            await _sessionStorage.RemoveItemAsync("UserCredit");
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authState);

        }
    }
}
