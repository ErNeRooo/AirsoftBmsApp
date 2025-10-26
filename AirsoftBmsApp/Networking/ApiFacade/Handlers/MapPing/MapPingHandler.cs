using AirsoftBmsApp.Model.Dto.MapPing;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Services.MapPingRestService;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.MapPing;

public class MapPingHandler(
    IMapPingRestService MapPingRestService, 
    IRoomDataService roomDataService, 
    IPlayerDataService playerDataService
    ) : IMapPingHandler
{
    public async Task<HttpResult> Create(PostMapPingDto postMapPingDto)
    {
        try
        {
            (HttpResult result, MapPingDto? mapPing) = await MapPingRestService.PostAsync(postMapPingDto);

            if (result is Success)
            {
                ObservableTeam? team = roomDataService.Room.Teams.FirstOrDefault(t => t.Id == playerDataService.Player.TeamId);

                if (team != null)
                {
                    team.MapPings.Add(new ObservableMapPing(mapPing));
                }
            }
            else if (result is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

            return result;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }

    public async Task<HttpResult> Delete(int id)
    {
        try
        {
            HttpResult result = await MapPingRestService.DeleteAsync(id);

            if (result is Success)
            {
                var team = roomDataService.Room.Teams
                    .FirstOrDefault(t => t.Id == playerDataService.Player.TeamId);

                var mapPingToRemove = team?.MapPings.FirstOrDefault(o => o.MapPingId == id);

                if(mapPingToRemove is not null && team is not null) team.MapPings.Remove(mapPingToRemove);
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
