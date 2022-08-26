using MobileApp.Models;
using MobileApp.Services;
using Xunit;

namespace UnitTests
{
    public class AccountServiceLoginTests
    {
        [Fact]
        public async void VerifyServiceForExistingAccount()
        {
            var AccountService = new ApiAccountService(true);
            var account = new Account
            {
                User = "a",
                Password = "a"
            };


            var response = await AccountService.Login(account);

            string CorrectResponse = "YS8vLy84MDA4NGJmMmZiYTAyNDc1NzI2ZmViMmNhYjJkODIxNWVhYjE0YmM2YmRkOGJmYjJjODE1MTI1NzAzMmVjZDhi";

            Assert.Equal(CorrectResponse, response);
        }

        [Fact]
        public async void VerifyServiceForNonExistingAccount()
        {
            var AccountService = new ApiAccountService(true);
            var account = new Account
            {
                User = "SomeAccount",
                Password = "ThisAccountDoesNotExist"
            };


            //"{\"token\": \"YS8vLy84MDA4NGJmMmZiYTAyNDc1NzI2ZmViMmNhYjJkODIxNWVhYjE0YmM2YmRkOGJmYjJjODE1MTI1NzAzMmVjZDhi\"}"
            //"{\"unauthorised\": \"incorrect credentials\"}"
            var response = await AccountService.Login(account);

            Assert.Equal("Incorrect credentials", response);
        }
        [Fact]
        public async void VerifyServiceForNonValidData()
        {
            var AccountService = new ApiAccountService(true);
            var account = new Account
            {
                User = null,
                Password = null
            };

            var response = await AccountService.Login(account);

            Assert.Equal("Something went wrong with the api call", response);
        }

    }
}