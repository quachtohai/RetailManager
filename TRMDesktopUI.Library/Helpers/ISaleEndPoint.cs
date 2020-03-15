using System.Threading.Tasks;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.Library.Helpers
{
    public interface ISaleEndPoint
    {
        Task PostSale(SaleModel sale);
    }
}