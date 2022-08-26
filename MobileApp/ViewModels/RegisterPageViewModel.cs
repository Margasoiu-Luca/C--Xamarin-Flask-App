using MobileApp.Models;
using MobileApp.Navigation;
using MobileApp.Services;
using MobileApp.Views;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class RegisterPageViewModel : ObservableObject
    {
        private readonly ApiAccountService _accountService;
        private string _username;
        private string _password;
        private string _status;


        public string Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));

                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));

                }
            }
        }


        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));

                }
            }
        }




        public RegisterPageViewModel()
        {
            _accountService = new ApiAccountService(false);

            NavigateToLoginCommand = new Command(NavigateToLogin);

            RegisterCommand = new Command(async () => await AttemptRegister()); 
        }

        public ICommand RegisterCommand { get; set; }
        public ICommand NavigateToLoginCommand { get; set; }

        private async Task AttemptRegister()
        {
            var account = new Account
            {
                User = _username,
                Password = _password
            };
            var succeeded = await _accountService.Register(account);
            Status = succeeded;
        }

        public void NavigateToLogin()
        {
            var loginPage = new LoginPage();
            NavigationDispatcher.Instance.Navigation.PopAsync();

        }
    }
}
