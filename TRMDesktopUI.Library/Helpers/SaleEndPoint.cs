using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.Library.Helpers
{
    public class SaleEndPoint : ISaleEndPoint
    {
        private IAPIHelper _apiHelper;
        public SaleEndPoint(IAPIHelper APIHelper)
        {
            _apiHelper = APIHelper;
        }
        public async Task PostSale(SaleModel sale)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Sale", sale))
            {
                if (response.IsSuccessStatusCode)
                {
                    //var resultTmp = await response.Content.ReadAsStringAsync();
                    //List<ProductModel> result = JsonConvert.DeserializeObject<List<ProductModel>>(resultTmp);
                    //return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
