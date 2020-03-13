using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TRMDesktopUI.Models;

namespace TRMDesktopUI.Helpers
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient APIClient;
        public APIHelper()
        {
            InitializeAPIHelper();
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
    }

}
