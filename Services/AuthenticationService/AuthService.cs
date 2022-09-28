using Portfolio_AppRepo_UI.Classes;
using Portfolio_AppRepo_UI.Models;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Portfolio_AppRepo_UI.Services.AuthenticationService
{
    public class AuthService : IAuthService
    {
        private ISessionStorageService _sessionStorage;
        public IHttpClientFactory _httpClient { get; }
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private LogWorker logWorker = new();
        public AuthService(IHttpClientFactory httpClient, ISessionStorageService sessionStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _sessionStorage = sessionStorage;
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> LoginAsync(LoginModel model)
        {
            try
            {
                var result = new UserModel();
                var url = Settings.BaseUrl + "Authentication/AuthorizeUser";
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpClient client = _httpClient.CreateClient();
                client.BaseAddress = new Uri(Settings.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<UserModel>(responseData);
                }

                if (result != null)
                {
                    await _sessionStorage.SetItemAsync("UserCredit", result);
                    await ((CustomAuthenticationProvider)_authenticationStateProvider).LoggedIn();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                logWorker.LogError(e);
                throw new Exception("Request Unseccussful.");
            }
        }

        public async Task<bool> LogoutAsync()
        {
            await ((CustomAuthenticationProvider)_authenticationStateProvider).LoggedoutAsync();
            return true;
        }
    }
}
