using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Models;

namespace MobileApp.Services
{
    internal interface IAccountService
    {
        Task<string> Login(Account accountToLogin);

        Task<string> Register(Account accountToRegister);

        Task<bool> Delete(string accountName);
    }
}
