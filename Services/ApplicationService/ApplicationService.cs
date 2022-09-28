using Portfolio_AppRepo_UI.Classes;
using Portfolio_AppRepo_UI.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Text;

namespace Portfolio_AppRepo_UI.Services.ApplicationService
{
    public class ApplicationService : IApplicationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public IHttpClientFactory _httpClient { get; }
        private LogWorker logWorker = new();
        public ApplicationService(IHttpClientFactory httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
        }
        public async Task<int> Create(AddApplicationModel model)
        {
            try
            {
                int result = 0;
                var url = Settings.BaseUrl + "Application/CreateNewEntry";
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpClient client = _httpClient.CreateClient();
                client.BaseAddress = new Uri(Settings.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<int>(responseData);

                }
                return result;
            }
            catch (Exception e)
            {
                logWorker.LogError(e);
                throw new Exception("Request Unseccussful.");
            }
        }

        public async Task<bool> Delete(int Id, string User)
        {
            try
            {
                bool result = false;

                HttpClient getUserRoleClient = _httpClient.CreateClient();
                getUserRoleClient.BaseAddress = new Uri(Settings.BaseUrl);
                getUserRoleClient.DefaultRequestHeaders.Add("Accept", "application/json");
                var integrationUrl = getUserRoleClient.BaseAddress + "Application/DeleteEntryById?Id=" + Id + "&User=" + User;

                HttpResponseMessage response = await getUserRoleClient.GetAsync(integrationUrl);
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

        public async Task<AddApplicationModel> GetApplicationById(int Id)
        {
            try
            {
                var result = new AddApplicationModel();

                HttpClient getUserRoleClient = _httpClient.CreateClient();
                getUserRoleClient.BaseAddress = new Uri(Settings.BaseUrl);
                getUserRoleClient.DefaultRequestHeaders.Add("Accept", "application/json");

                var integrationUrl = getUserRoleClient.BaseAddress + "Application/GetEntryById?Id=" + Id;

                HttpResponseMessage response = await getUserRoleClient.GetAsync(integrationUrl);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<AddApplicationModel>(responseData);
                }

                return result;
            }
            catch (Exception e)
            {
                logWorker.LogError(e);
                throw new Exception("Request Unseccussful.");
            }
        }

        public async Task<bool> Update(AddApplicationModel model)
        {
            try
            {
                bool result = false;
                var url = Settings.BaseUrl + "Application/UpdateEntry";
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                HttpClient client = _httpClient.CreateClient();
                client.BaseAddress = new Uri(Settings.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                TimeSpan ts = new TimeSpan(600000000);
                client.Timeout.Add(ts);
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

        public async Task<List<AddApplicationModel>> GetAllApplications()
        {
            try
            {
                var result = new List<AddApplicationModel>();

                HttpClient getUserRoleClient = _httpClient.CreateClient();
                getUserRoleClient.BaseAddress = new Uri(Settings.BaseUrl);
                getUserRoleClient.DefaultRequestHeaders.Add("Accept", "application/json");

                var integrationUrl = getUserRoleClient.BaseAddress + "Application/GetAllEntries";

                HttpResponseMessage response = await getUserRoleClient.GetAsync(integrationUrl);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<AddApplicationModel>>(responseData);
                }
                return result;
            }
            catch (Exception e)
            {
                logWorker.LogError(e);
                throw new Exception("Request Unseccussful.");
            }
        }

        public async Task<List<ApplicationListModel>> GetApplicationExistanceCheck(string Name)
        {
            try
            {
                var result = new List<ApplicationListModel>();

                HttpClient getUserRoleClient = _httpClient.CreateClient();
                getUserRoleClient.BaseAddress = new Uri(Settings.BaseUrl);
                getUserRoleClient.DefaultRequestHeaders.Add("Accept", "application/json");

                var integrationUrl = getUserRoleClient.BaseAddress + "Application/GetApplicationExistanceCheck?Name=" + Name;

                HttpResponseMessage response = await getUserRoleClient.GetAsync(integrationUrl);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<ApplicationListModel>>(responseData);
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
