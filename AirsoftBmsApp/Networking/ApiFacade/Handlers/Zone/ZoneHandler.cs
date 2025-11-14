using AirsoftBmsApp.Model.Dto.Zone;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Services.ZoneRestService;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using Microsoft.Maui.Controls.Maps;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Zone;

public class ZoneHandler(
    IZoneRestService ZoneRestService, 
    IRoomDataService roomDataService, 
    IPlayerDataService playerDataService
    ) : IZoneHandler
{
    public async Task<HttpResult> Create(PostZoneDto postZoneDto)
    {
        try
        {
            (HttpResult result, ZoneDto? zone) = await ZoneRestService.PostAsync(postZoneDto);

            if (result is Success)
            {
                roomDataService.Room.Battle?.Zones.Add(new ObservableZone(zone));
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
