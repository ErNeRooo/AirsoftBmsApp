using AirsoftBmsApp.Model.Dto.Location;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Services.LocationRestService;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Location;

public class LocationHandler(ILocationRestService LocationRestService, IPlayerDataService playerDataService) : ILocationHandler
{
    public async Task<HttpResult> Create(PostLocationDto postLocationDto)
    {
        try
        {
            (HttpResult result, LocationDto? Location) = await LocationRestService.PostAsync(postLocationDto);

            if (result is Success)
            {
                playerDataService.Player.Locations.Add(new ObservableLocation(Location));
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
