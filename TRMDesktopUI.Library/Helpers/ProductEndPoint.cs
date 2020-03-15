using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.Library.Helpers
{
    public class ProductEndPoint : IProductEndPoint
    {
        private IAPIHelper _apiHelper;
        public ProductEndPoint(IAPIHelper APIHelper)
        {
            _apiHelper = APIHelper;
        }
        public async Task<List<ProductModel>> GetAll()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Product"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var resultTmp = await response.Content.ReadAsStringAsync();
                    List<ProductModel> result = JsonConvert.DeserializeObject<List<ProductModel>>(resultTmp);
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
