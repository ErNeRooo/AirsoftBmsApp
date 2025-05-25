using AirsoftBmsApp.Model;
using AirsoftBmsApp.Model.Dto.Account;
using AirsoftBmsApp.Model.Dto.Post;
using AirsoftBmsApp.Services.AccountRestService.Abstractions;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Networking
{
    public class AccountLogInHandler(IAccountRestService accountRestService, IPlayerDataService playerDataService) : AbstractHandler
    {
        public override async Task<HttpResult> Handle(object request)
        {
            dynamic dynamicRequest = request;

            LogInAccountDto logInAccountDto = new LogInAccountDto
            {
                Email = dynamicRequest.Email,
                Password = dynamicRequest.Password,
            };

            var result = await accountRestService.LogInAsync(request as LogInAccountDto);

            switch (result)
            {
                case Success<int> success:
                    playerDataService.Player.Account = new Account
                    {
                        Id = success.data,
                        Email = logInAccountDto.Email
                    };

                    var nextResult = await base.Handle(request);

                    return nextResult is null ? result : nextResult;
                case Failure failure:
                    return new Failure(failure.errorMessage);
                case Error error:
                    return new Error(error.errorMessage);
                default:
                    throw new InvalidOperationException("Unknown result type");
            }
        }
    }
}
