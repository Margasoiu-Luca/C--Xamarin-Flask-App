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


    public class LoginPageViewModel : ObservableObject
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


        public ICommand LoginCommand { get; set; }

        public ICommand NavigateToRegisterCommand { get; set; }
        public LoginPageViewModel()
        {
            _accountService = new ApiAccountService(false);

            NavigateToRegisterCommand = new Command(NavigateToRegister);

            LoginCommand = new Command(async () => await AttemptLogin());
        }

        public void NavigateToRegister()
        {
            var registerPage = new RegisterPage();
            NavigationDispatcher.Instance.Navigation.PushAsync(registerPage);
        }

        public void NavigateToMainPage(string auth)
        {
            var mainPage = new MainPage(auth);
            NavigationDispatcher.Instance.Navigation.PushAsync(mainPage);
        }

        private async Task AttemptLogin()
        {
            var account = new Account
            {
                User = _username,
                Password = _password
            };
            var response = await _accountService.Login(account);
            if( response == "Error occurred" || response == "Something went wrong with the api call" || response == "Incorrect credentials")
            {
                Status = response;
            }
            else
            {
                NavigateToMainPage(response);
            }
        }
    }
}   
