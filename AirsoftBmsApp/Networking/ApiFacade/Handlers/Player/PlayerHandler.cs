using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Observable;
using AirsoftBmsApp.Resources.Languages;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.RoomDataService.Abstractions;
using MethodTimer;
using System.Diagnostics;

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

            if (result is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

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
                    if (result is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

                    return result;
                }

                return result;
            }
            catch (Exception ex)
            {
                return new Error(ex.Message);
            }
        }

        [Time]
        public async Task<HttpResult> Update(PutPlayerDto putPlayerDto, int playerId)
        {
            try
            {
                (HttpResult result, PlayerDto? player) = await playerRestService.PutAsync(putPlayerDto, playerId);

                if (result is Success)
                {
                    ObservablePlayer? playerToUpdate = roomDataService.Room.Teams
                        .SelectMany(team => team.Players)
                        .FirstOrDefault(p => p.Id == playerId);

                    if (player is null || playerToUpdate is null) return new Failure("Player is null");

                    playerToUpdate.Name = player.Name;
                    playerToUpdate.IsDead = player.IsDead;

                    if (playerToUpdate.TeamId != player.TeamId)
                    {
                        ObservableTeam? previousTeam = roomDataService.Room.Teams
                            .FirstOrDefault(team => team.Id == (playerToUpdate.TeamId ?? 0));

                        if(previousTeam?.OfficerId == playerToUpdate.Id) {
                            playerToUpdate.IsOfficer = false;
                            previousTeam.OfficerId = 0; 
                        }

                        int index = previousTeam.Players.IndexOf(playerToUpdate);
                        if (index >= 0) previousTeam.Players.RemoveAt(index);

                        playerToUpdate.TeamId = player.TeamId;

                        roomDataService.Room
                            .Teams.FirstOrDefault(t => t.Id == player.TeamId)
                            ?.Players.Add(playerToUpdate);
                    }
                }
                else
                {
                    if (result is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

                    return result;
                }

                return result;
            }
            catch (Exception ex)
            {
                return new Error(ex.Message);
            }
        }

        [Time]
        public async Task<HttpResult> KickFromRoom(int playerId)
        {
            try
            {
                (HttpResult result, PlayerDto? player) = await playerRestService.KickFromRoomByIdAsync(playerId);

                if (result is Success)
                {
                    ObservablePlayer? playerToKick = roomDataService.Room.Teams
                        .SelectMany(team => team.Players)
                        .FirstOrDefault(p => p.Id == playerId);

                    ObservableTeam? teamOfPlayerToKick = roomDataService.Room.Teams.FirstOrDefault(team => team.Id == playerToKick.TeamId);

                    SetTeamOfficerToNotAssigned(playerToKick, teamOfPlayerToKick);

                    teamOfPlayerToKick.Players.Remove(playerToKick);
                }
                else
                {
                    if (result is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

                    return result;
                }

                return result;
            }
            catch (Exception ex)
            {
                return new Error(ex.Message);
            }
        }

        [Time]
        public async Task<HttpResult> KickFromTeam(int playerId)
        {
            try
            {
                (HttpResult result, PlayerDto? player) = await playerRestService.KickFromTeamByIdAsync(playerId);

                if (result is Success)
                {
                    ObservablePlayer? playerToKick = roomDataService.Room.Teams
                        .SelectMany(team => team.Players)
                        .FirstOrDefault(p => p.Id == playerId);

                    ObservableTeam? teamOfPlayerToKick = roomDataService.Room.Teams.FirstOrDefault(team => team.Id == playerToKick.TeamId);

                    SetTeamOfficerToNotAssigned(playerToKick, teamOfPlayerToKick);

                    teamOfPlayerToKick?.Players.Remove(playerToKick);

                    playerToKick.TeamId = 0;

                    roomDataService.Room.Teams[0].Players.Add(playerToKick);
                }
                else
                {
                    if (result is Failure failure && failure.errorMessage == "") return new Failure(AppResources.UnhandledErrorMessage);

                    return result;
                }

                return result;
            }
            catch (Exception ex)
            {
                return new Error(ex.Message);
            }
        }

        private ObservableTeam SetTeamOfficerToNotAssigned(ObservablePlayer player, ObservableTeam playersTeam)
        {
            if (player.TeamId != 0 && player.IsOfficer)
            {
                playersTeam.OfficerId = 0;
            }

            return playersTeam;
        }
    }
}
