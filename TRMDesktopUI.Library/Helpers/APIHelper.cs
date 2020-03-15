using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TRMDesktopUI.Library.Models;
using Newtonsoft.Json;

namespace TRMDesktopUI.Library.Helpers
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient APIClient;
        private ILoggedInUserModel _loggedInUser;
        public APIHelper(ILoggedInUserModel loggedInUser)
        {
            InitializeAPIHelper();
            _loggedInUser = loggedInUser;
        }
        public HttpClient ApiClient
        {
            get{ return APIClient; }
        }
        private void InitializeAPIHelper()
        {
            APIClient = new HttpClient();
            var baseUrl = ConfigurationManager.AppSettings["api"];
            APIClient.BaseAddress = new Uri(baseUrl);
            APIClient.DefaultRequestHeaders.Accept.Clear();
            APIClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<AuthenticationUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(
                new[] {
                new KeyValuePair<string,string>("grant_type","password"),
                new KeyValuePair<string,string>("username",username),
                new KeyValuePair<string,string>("password",password)
            });
            using (HttpResponseMessage response = await APIClient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticationUser>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task GetLoggedInUserInfo(string token)
        {
            APIClient.DefaultRequestHeaders.Clear();
            APIClient.DefaultRequestHeaders.Accept.Clear();
            APIClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            APIClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            using (HttpResponseMessage response = await APIClient.GetAsync("/api/User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result1 = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<LoggedInUserModel>>(result1);
                    //_loggedInUser.CreatedDate = result.CreatedDate;
                    //_loggedInUser.EmailAddress = result.EmailAddress;
                    //_loggedInUser.FirstName = result.FirstName;
                    //_loggedInUser.Id = result.Id;
                    //_loggedInUser.Token = result.Token;
                    //_loggedInUser.LastName = result.LastName;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }

}
