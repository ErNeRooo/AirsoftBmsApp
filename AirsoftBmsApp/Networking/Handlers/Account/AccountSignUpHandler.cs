using AirsoftBmsApp.Model;
using AirsoftBmsApp.Model.Dto.Account;
using AirsoftBmsApp.Services.AccountRestService.Abstractions;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Networking.Handlers.Account
{
    public class AccountSignUpHandler(IAccountRestService accountRestService, IPlayerDataService playerDataService) : AbstractHandler
    {
        public override async Task<HttpResult> Handle(object request)
        {
            dynamic dynamicRequest = request;

            SignUpAccountDto signUpAccountDto = new SignUpAccountDto
            {
                Email = dynamicRequest.Email,
                Password = dynamicRequest.Password,
            };

            var result = await accountRestService.TryRequest(new SignUpAccountAsync(signUpAccountDto));

            switch (result)
            {
                case Success<int> success:
                    playerDataService.Player.Account = new Model.ObservableAccount
                    {
                        Id = success.data,
                        Email = signUpAccountDto.Email
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
