using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDesktopUI.Library.Models
{
    public class CartItemModel
    {
        public ProductModel Product { set; get; }
        public int QuantityInCart { set; get; }
        public string DisplayText
        {
            get
            {
                return $"{Product.ProductName}({QuantityInCart})";
            }
        }
    }
}
