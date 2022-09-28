using Portfolio_AppRepo_UI.Classes;
using Portfolio_AppRepo_UI.Models;
using Newtonsoft.Json;
using System.Text;

namespace Portfolio_AppRepo_UI.Services.UserService
{
    public class UserService : IUserService
    {
        public IHttpClientFactory _httpClient { get; }
        private LogWorker logWorker = new();
        public UserService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Create(UserModel model)
        {
            bool result = false;
            try
            {
                var url = Settings.BaseUrl + "User/Create";
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpClient client = _httpClient.CreateClient();
                client.BaseAddress = new Uri(Settings.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<bool>(responseData);
                }
                return result;
            }
            catch (Exception e)
            {
                logWorker.LogError(e);
                throw new Exception("Request Unseccussful.");
            }

        }

        public Task<bool> Delete(UserModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserModel>> GetAll()
        {
            try
            {
                var result = new List<UserModel>();

                HttpClient getUserRoleClient = _httpClient.CreateClient();
                getUserRoleClient.BaseAddress = new Uri(Settings.BaseUrl);
                getUserRoleClient.DefaultRequestHeaders.Add("Accept", "application/json");

                var integrationUrl = getUserRoleClient.BaseAddress + "User/GetAll";

                HttpResponseMessage response = await getUserRoleClient.GetAsync(integrationUrl);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<UserModel>>(responseData);
                }

                return result;
            }
            catch (Exception e)
            {
                logWorker.LogError(e);
                throw new Exception("Request Unseccussful.");
            }

        }

        public async Task<UserModel> GetById(int Id)
        {
            try
            {
                var result = new UserModel();

                HttpClient getUserRoleClient = _httpClient.CreateClient();
                getUserRoleClient.BaseAddress = new Uri(Settings.BaseUrl);
                getUserRoleClient.DefaultRequestHeaders.Add("Accept", "application/json");

                var integrationUrl = getUserRoleClient.BaseAddress + "User/GetById?Id=" + Id;

                HttpResponseMessage response = await getUserRoleClient.GetAsync(integrationUrl);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<UserModel>(responseData);
                }

                return result;
            }
            catch (Exception e)
            {
                logWorker.LogError(e);
                throw new Exception("Request Unseccussful.");
            }

        }

        public async Task<bool> Update(UserModel model)
        {
            bool result = false;
            try
            {
                var url = Settings.BaseUrl + "User/Update";
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpClient client = _httpClient.CreateClient();
                client.BaseAddress = new Uri(Settings.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<bool>(responseData);

                }
                return result;
            }
            catch (Exception e)
            {
                logWorker.LogError(e);
                throw new Exception("Request Unseccussful.");
            }
        }
    }
}
