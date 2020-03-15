using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDataManager.Library.Internal.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class ProductData
    {
        public List<ProductModel> GetProducts()
        {
            SqlDataAccess dataAccess = new SqlDataAccess();
            return dataAccess.LoadData<ProductModel, dynamic>("spProduct_GetAll", new { }, "ToHaiRetailManager");
        }
        public ProductModel GetProductById(int productId)
        {
            SqlDataAccess dataAccess = new SqlDataAccess();
            return dataAccess.LoadData<ProductModel, dynamic>("spProduct_GetProductById", new {Id = productId}, "ToHaiRetailManager")
                .FirstOrDefault();
        }
    }
}
