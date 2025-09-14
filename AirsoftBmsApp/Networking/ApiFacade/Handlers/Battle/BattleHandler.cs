using AirsoftBmsApp.Model.Dto.Battle;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Location;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Services.BattleRestService;
using AirsoftBmsApp.Services.GeolocationService;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Battle;

public class BattleHandler(
    IBattleRestService battleRestService,
    IRoomDataService roomDataService,
    IGeolocationService geolocationService
    ) : IBattleHandler
{
    public async Task<HttpResult> Create(PostBattleDto postBattleDto)
    {
        try
        {
            (HttpResult result, BattleDto? battle) = await battleRestService.PostAsync(postBattleDto);

            if (result is Success)
            {
                Action OnBattleActivated = async () => await geolocationService.Start();
                Action OnBattleDeactivated = geolocationService.Stop;
                var newBattle = new ObservableBattle(battle, OnBattleActivated, OnBattleDeactivated);

                roomDataService.Room.Battle = newBattle;
            }
            else if (result is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

            return result;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }

    public async Task<HttpResult> Update(PutBattleDto battleDto, int battleId)
    {
        try
        {
            (HttpResult result, BattleDto? battle) = await battleRestService.PutAsync(battleDto, battleId);

            if (result is Success)
            {
                if (roomDataService.Room.Battle is { } existingBattle)
                {
                    existingBattle.Name = battle.Name;
                    existingBattle.BattleId = battle.BattleId;
                    existingBattle.IsActive = battle.IsActive;
                    existingBattle.RoomId = battle.RoomId;
                }
                else
                {
                    Action OnBattleActivated = async () => await geolocationService.Start();
                    Action OnBattleDeactivated = geolocationService.Stop;
                    roomDataService.Room.Battle = new ObservableBattle(battle, OnBattleActivated, OnBattleDeactivated);
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

    public async Task<HttpResult> End(int battleId)
    {
        try
        {
            HttpResult result = await battleRestService.DeleteAsync(battleId);

            if (result is Success)
            {
                roomDataService.Room.Battle = null;
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
