using System.Collections.Generic;
using System.Threading.Tasks;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.Library.Helpers
{
    public interface IProductEndPoint
    {
        Task<List<ProductModel>> GetAll();
    }
}