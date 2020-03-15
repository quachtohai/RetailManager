using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDataManager.Library.Models
{
    public class UserModel
    {
        public string Token { set; get; }
        public string Id { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string EmailAddress { set; get; }
        public DateTime CreatedDate { set; get; }
        
    }
}
