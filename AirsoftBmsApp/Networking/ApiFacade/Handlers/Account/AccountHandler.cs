using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Model.Dto.Account;
using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Services.AccountRestService.Abstractions;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Player;
using AirsoftBmsApp.Resources.Languages;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Account;

public class AccountHandler(
    IAccountRestService accountRestService,
    IPlayerDataService playerDataService,
    IPlayerHandler playerHandler
    ) : IAccountHandler
{
    public async Task<HttpResult> LogIn(LogInAccountDto logInAccountDto, string username)
    {
        try
        {
            PostPlayerDto postPlayerDto = new()
            {
                Name = username,
            };

            HttpResult registerResult = await playerHandler.Register(postPlayerDto);

            if(registerResult is not Success) return registerResult;

            (HttpResult logInResult, AccountDto account) = await accountRestService.LogInAsync(logInAccountDto);

            if (logInResult is Success logInSuccess)
            {
                playerDataService.Player.Account = new ObservableAccount
                {
                    AccountId = account.AccountId,
                    PlayerId = account.PlayerId,
                    Email = account.Email,
                };

                return new Success();
            }
            else
            {
                if (logInResult is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

                return logInResult;
            }
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }

    public async Task<HttpResult> SignUp(SignUpAccountDto signUpAccountDto, string username)
    {
        try
        {
            PostPlayerDto postPlayerDto = new()
            {
                Name = username,
            };

            HttpResult registerResult = await playerHandler.Register(postPlayerDto);

            if (registerResult is not Success) return registerResult;

            (HttpResult signUpResult, AccountDto account) = await accountRestService.SignUpAsync(signUpAccountDto);

            if (signUpResult is Success)
            {
                playerDataService.Player.Account = new ObservableAccount
                {
                    AccountId = account.AccountId,
                    PlayerId = account.PlayerId,
                    Email = account.Email,
                };

                return new Success();
            }
            else
            {
                if (signUpResult is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

                return signUpResult;
            }
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }
}

