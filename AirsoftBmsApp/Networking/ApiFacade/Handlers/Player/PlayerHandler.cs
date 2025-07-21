using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;

namespace AirsoftBmsApp.Networking.ApiFacade.Handlers.Player
{
    public class PlayerHandler(
        IPlayerRestService playerRestService,
        IPlayerDataService playerDataService,
        IRoomDataService roomDataService
        ) : IPlayerHandler
    {
        public async Task<HttpResult> LogOut()
        {
            HttpResult result = await playerRestService.DeleteAsync();

            playerDataService.Player = new ObservablePlayer();

            if (result is Failure failure && failure.errorMessage == "") return new Failure("Unhandled error");

            return result;
        }

        public async Task<HttpResult> Register(PostPlayerDto postPlayerDto)
        {
            try
            {
                (HttpResult result, int? playerId) = await playerRestService.RegisterAsync(postPlayerDto);

                if(result is Success) playerDataService.Player = new ObservablePlayer
                {
                    Id = playerId ?? 0,
                    Name = postPlayerDto.Name
                };
                else
                {
                    if (result is Failure failure && failure.errorMessage == "") return new Failure("Unhandled error");

                    return result;
                }

                return result;
            }
            catch (Exception ex)
            {
                return new Error(ex.Message);
            }
        }

        public async Task<HttpResult> Update(PutPlayerDto putPlayerDto)
        {
            try
            {
                (HttpResult result, PlayerDto? player) = await playerRestService.PutAsync(putPlayerDto);

                if (result is Success)
                {
                    playerDataService.Player.Name = player?.Name;
                    playerDataService.Player.IsDead = (bool)player?.IsDead;

                    if (playerDataService.Player.TeamId != player.TeamId)
                    {
                        playerDataService.Player.IsOfficer = false;

                        ObservableTeam? previousTeam = roomDataService.Room.Teams
                            .FirstOrDefault(t => t.Id == (playerDataService.Player.TeamId ?? 0));

                        if(previousTeam.OfficerId == player.PlayerId) previousTeam.OfficerId = 0;

                        for (int i = 0; i < previousTeam.Players.Count; i++)
                        {
                            if (previousTeam.Players[i].Id == playerDataService.Player.Id)
                            {
                                previousTeam.Players.RemoveAt(i);
                                break;
                            }
                        }
                        
                        playerDataService.Player.TeamId = (int)player?.TeamId;

                        roomDataService.Room
                            .Teams.FirstOrDefault(t => t.Id == player?.TeamId)
                            ?.Players.Add(playerDataService.Player);
                    }
                }
                else
                {
                    if (result is Failure failure && failure.errorMessage == "") return new Failure("Unhandled error");

                    return result;
                }

                return result;
            }
            catch (Exception ex)
            {
                return new Error(ex.Message);
            }
        }
    }
}
