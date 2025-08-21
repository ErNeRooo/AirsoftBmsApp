using AirsoftBmsApp.Model.Dto.Kills;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Services.KillRestService;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Kill;

public class KillHandler(IKillRestService killRestService, IPlayerDataService playerDataService) : IKillHandler
{
    public async Task<HttpResult> Create(PostKillDto postKillDto)
    {
        try
        {
            (HttpResult result, KillDto? kill) = await killRestService.PostAsync(postKillDto);

            if (result is Success)
            {
                playerDataService.Player.Kills.Add(new ObservableKill(kill));
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
