using MobileApp.Models;
using MobileApp.Services;
using Xunit;

namespace UnitTests
{
    public class AccountServiceDeleteTests
    {
        [Fact]
        public async void VerifyServiceForExistingAccount()
        {
            var AccountService = new ApiAccountService(true);
            var account = new Account
            {
                User = "SomeAccount",
                Password = "ThisAccountShouldExist"
            };

            await AccountService.Register(account);

            bool response = await AccountService.Delete("SomeAccount");

            Assert.True(response);
        }

        [Fact]
        public async void VerifyServiceForNonExistingAccount()
        {
            var AccountService = new ApiAccountService(true);



            bool response = await AccountService.Delete("ThisAccountDoesNotExist");

            Assert.False(response);
        }

        [Fact]
        public async void VerifyServiceForInvalidData()
        {
            var AccountService = new ApiAccountService(true);
            bool response = await AccountService.Delete("");

            Assert.False(response);
        }

    }
}
