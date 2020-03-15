using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.EventModels;
using TRMDesktopUI.Library.Helpers;

namespace TRMDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName ="test@gmail.com";
        private IAPIHelper _APIHelper;
        private IEventAggregator _evenAggregator;
        public LoginViewModel(IAPIHelper aPIHelper, IEventAggregator eventAggregator)
        {
            _APIHelper = aPIHelper;
            _evenAggregator = eventAggregator;
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
        private string _password ="Hh@123456";

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }
        public bool IsErrorVisible
        {
            get
            {
                bool output = false;
                if (ErrorMessage?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
        }
        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value;
                NotifyOfPropertyChange(() => IsErrorVisible);
                NotifyOfPropertyChange(() => ErrorMessage);
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
                ErrorMessage = string.Empty;
                var result = await _APIHelper.Authenticate(UserName, Password);
                await _APIHelper.GetLoggedInUserInfo(result.Access_Token);
                _evenAggregator.PublishOnUIThread(new LogOnEvent());
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

    }
}
