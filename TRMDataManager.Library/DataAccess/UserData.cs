using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDataManager.Library.Internal.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class UserData
    {
        public List<UserModel> GetUserById(string Id)
        {
            SqlDataAccess dataAccess = new SqlDataAccess();
            var p = new { Id = Id };
            return dataAccess.LoadData<UserModel, dynamic>("spUserLookUp", p, "ToHaiRetailManager");
        }
    }
}
