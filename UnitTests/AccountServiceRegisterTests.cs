using MobileApp.Models;
using MobileApp.Services;
using Xunit;

namespace UnitTests

{
    public class AccountServiceRegisterTests
    {
        [Fact]
        public async void VerifyServiceForNewAccount()
        {
            var AccountService = new ApiAccountService(true);
            var account = new Account
            {
                User = "SomeAccount",
                Password = "ThisAccountShouldExist"
            };

            var response = await AccountService.Register(account);

            Assert.Equal("Account successfully created", response);
            //delete afterwards
            await AccountService.Delete("SomeAccount");
        }

        [Fact]
        public async void VerifyServiceForExistingAccount()
        {
            var AccountService = new ApiAccountService(true);
            var account = new Account
            {
                User = "a",
                Password = "a"
            };

            var response = await AccountService.Register(account);

            Assert.Equal("Username already used", response);
        }

        [Fact]
        public async void VerifyServiceForInvalidData()
        {
            var AccountService = new ApiAccountService(true);
            var account = new Account
            {
                User = null,
                Password = null
            };

            var response = await AccountService.Register(account);

            Assert.Equal("Incorrect data provided", response);
        }

    }
}
