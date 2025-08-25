using AirsoftBmsApp.Model.Dto.Death;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Services.DeathRestService;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Death;

public class DeathHandler(IDeathRestService DeathRestService, IPlayerDataService playerDataService) : IDeathHandler
{
    public async Task<HttpResult> Create(PostDeathDto postDeathDto)
    {
        try
        {
            (HttpResult result, DeathDto? Death) = await DeathRestService.PostAsync(postDeathDto);

            if (result is Success)
            {
                playerDataService.Player.Deaths.Add(new ObservableDeath(Death));
                playerDataService.Player.IsDead = true;
            }
            else if (result is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

            return result;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }
}
