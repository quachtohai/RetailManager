using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Helpers;

namespace TRMDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName;
        private IAPIHelper _APIHelper;
        public LoginViewModel(IAPIHelper aPIHelper)
        {
            _APIHelper = aPIHelper;
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);

            }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }
        public bool CanLogIn(string userName, string password)
        {
            if (userName.Length > 0 && password.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task LogIn()
        {
            try
            {
                var result = await _APIHelper.Authenticate(UserName, Password);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}
