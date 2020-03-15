using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDataManager.Library.Internal.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class SaleData
    {
        public void SaveSale(SaleModel sale, string userId)
        {

            List<SaleDBDetailModel> details = new List<SaleDBDetailModel>();
            ProductData productData = new ProductData();
            foreach (var item in sale.SaleDetails)
            {
                var detail = new SaleDBDetailModel()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                var productInfo = productData.GetProductById(item.ProductId);
                detail.PurchasePrice = productInfo.RetailPrice * item.Quantity;
                detail.Tax = detail.PurchasePrice * (decimal)0.1;
                details.Add(detail);
            }
            SaleDBModel saleDBModel = new SaleDBModel()
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = userId
            };
            saleDBModel.Total = saleDBModel.SubTotal + saleDBModel.Tax;
            SqlDataAccess sql = new SqlDataAccess();
            sql.SaveData("dbo.spSale_Insert", saleDBModel, "ToHaiRetailManager");

            var saleInfo = sql.LoadData<SaleDBModel, dynamic>("spLookUp_SaleId", new
            {
                CashierId = saleDBModel.CashierId
            }, "ToHaiRetailManager").FirstOrDefault();
            foreach (var item in details)
            {
                item.SaleId = saleInfo.Id;
                sql.SaveData("dbo.spSaleDetail_Insert", item, "ToHaiRetailManager");
            }

        }
    }
}

