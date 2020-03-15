using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDesktopUI.Library.Models
{
    public class LoggedInUserModel : ILoggedInUserModel
    {
        public string Id { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string EmailAddress { set; get; }
        public DateTime CreatedDate { set; get; }
        public string Token { set; get; }
    }
}
