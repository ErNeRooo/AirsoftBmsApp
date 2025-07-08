using AirsoftBmsApp.Model.Dto.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Account
{
    public interface IAccountHandler
    {
        public Task<HttpResult> LogIn(LogInAccountDto logInAccountDto, string username);
        public Task<HttpResult> SignUp(SignUpAccountDto signUpAccountDto, string username);
    }
}
