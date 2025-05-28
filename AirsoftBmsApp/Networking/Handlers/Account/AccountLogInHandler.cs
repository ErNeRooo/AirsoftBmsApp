using AirsoftBmsApp.Model;
using AirsoftBmsApp.Model.Dto.Account;
using AirsoftBmsApp.Services.AccountRestService.Abstractions;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;


namespace AirsoftBmsApp.Networking.Handlers.Account
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

            var result = await accountRestService.TryRequest(new LogInAccountAsync(logInAccountDto));

            switch (result)
            {
                case Success<int> success:
                    playerDataService.Player.Account = new Model.ObservableAccount
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
