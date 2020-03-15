using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Library.Helpers;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
        private IConfigHelper _configHelper;
        private IProductEndPoint _productEndPoint;
        private ISaleEndPoint _saleEndPoint;
        public SalesViewModel(IProductEndPoint productEndPoint, ISaleEndPoint saleEndPoint, IConfigHelper configHelper)
        {
            _productEndPoint = productEndPoint;
            _saleEndPoint = saleEndPoint;
            _configHelper = configHelper;
        }
        protected override async void OnViewLoaded(object view)
        {
            await LoadData();
        }
        private async Task LoadData()
        {
            var productList = await _productEndPoint.GetAll();
            Products = new BindingList<ProductModel>(productList);
        }
        private BindingList<ProductModel> _products;
        public BindingList<ProductModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }
        private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();

        public BindingList<CartItemModel> Cart
        {
            get { return _cart; }
            set
            {
                _cart = value;
                NotifyOfPropertyChange(() => Cart);
            }
        }
        private int _itemQuantity;

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                NotifyOfPropertyChange(() => ItemQuantity);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }
        private ProductModel _selectedProduct;

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanAddToCart);
            }
        }
        public string SubTotal
        {
            get
            {
                return CalCulateSubToTal().ToString();
            }
        }
        private decimal CalCulateSubToTal()
        {
            decimal subTotal = 0;
            foreach (var item in Cart)
            {
                subTotal += item.QuantityInCart * item.Product.RetailPrice;
            }
            return subTotal;
        }
        private decimal CalCulateTax()
        {
            decimal tax = 0;
            decimal taxRate = _configHelper.GetTaxRate() / 100;
            foreach (var item in Cart)
            {
                tax += item.QuantityInCart * item.Product.RetailPrice * taxRate;
            }
            return tax;
        }
        public string Tax
        {
            get
            {
                return CalCulateTax().ToString();
            }
        }
        public string Total
        {
            get
            {
                return
                  (CalCulateSubToTal() + CalCulateTax()).ToString();
            }
        }
        public bool CanAddToCart
        {
            get
            {
                bool output = false;
                if (ItemQuantity > 0 && SelectedProduct?.QuantityInStock > ItemQuantity)
                {
                    output = true;
                }
                return output;
            }
        }
        public void AddToCart()
        {
            CartItemModel existingItem = Cart.FirstOrDefault(x => x.Product.ProductName == SelectedProduct.ProductName);
            if (existingItem != null)
            {
                existingItem.QuantityInCart += ItemQuantity;
                Cart.Remove(existingItem);
                Cart.Add(existingItem);
            }
            else
            {
                CartItemModel cartItemModel = new CartItemModel()
                {
                    Product = SelectedProduct,
                    QuantityInCart = ItemQuantity
                };
                Cart.Add(cartItemModel);
            }
            SelectedProduct.QuantityInStock -= ItemQuantity;
            ItemQuantity = 1;
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
        }
        public bool CanCheckOut
        {
            get
            {
                bool output = false;
                if (Cart.Count > 0)
                {
                    output = true;
                }
                return output;
            }
        }
        public async Task CheckOut()
        {
            SaleModel sale = new SaleModel();
            sale.SaleDetails = new List<SaleDetailModel>();
            foreach (var item in Cart)
            {
                sale.SaleDetails.Add(new SaleDetailModel
                {
                    ProductId = item.Product.Id,
                    Quantity = item.QuantityInCart
                });
            }
            await _saleEndPoint.PostSale(sale);
        }
        public bool CanRemoveFromCart
        {
            get
            {
                bool output = false;
                return output;
            }
        }
        public void RemoveFromCart()
        {
            NotifyOfPropertyChange(() => SubTotal);
            NotifyOfPropertyChange(() => Tax);
            NotifyOfPropertyChange(() => Total);
            NotifyOfPropertyChange(() => CanCheckOut);
        }

    }
}
