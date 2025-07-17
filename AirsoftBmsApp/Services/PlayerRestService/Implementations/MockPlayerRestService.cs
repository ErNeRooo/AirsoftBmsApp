using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;

namespace AirsoftBmsApp.Services.PlayerRestService.Implementations
{
    public class MockPlayerRestService(
        IPlayerDataService playerDataService
        ) : IPlayerRestService
    {
        public async Task<(HttpResult result, PlayerDto? player)> GetMeAsync()
        {
            return (new Success(), new PlayerDto
            {
                PlayerId = 1,
                Name = playerDataService.Player.Name,
                IsDead = playerDataService.Player.IsDead,
                RoomId = playerDataService.Player.RoomId,
                TeamId = playerDataService.Player.RoomId
            });
        }

        public async Task<(HttpResult result, PlayerDto? player)> GetByIdAsync(int playerId)
        {
            if (playerId == 400)
            {
                return (new Failure("Mocked Bad Request"), null);
            }
            else if (playerId == 2137)
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return (new Success(), new PlayerDto
                {
                    PlayerId = playerId,
                    Name = "Mocked Player",
                    IsDead = true,
                    RoomId = null,
                    TeamId = null
                });
            }
        }

        public async Task<(HttpResult result, PlayerDto? player)> PutAsync(PutPlayerDto playerDto)
        {
            if(playerDto.TeamId == 400)
            {
                return (new Failure("Mocked switch team error"), null);
            }

            return (new Success(), new PlayerDto
            {
                PlayerId = 1,
                Name = playerDto.Name ?? playerDataService.Player.Name,
                IsDead = playerDto.IsDead ?? playerDataService.Player.IsDead,
                RoomId = 1,
                TeamId = playerDto.TeamId ?? playerDataService.Player.TeamId
            });
        }
        public async Task<(HttpResult result, int? playerId)> RegisterAsync(PostPlayerDto playerDto)
        {
            if (playerDto.Name == "400")
            {
                return (new Failure("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."), null);
            }
            else if (playerDto.Name == "2137")
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return (new Success(), 1);
            }
        }

        public async Task<(HttpResult result, PlayerDto? player)> KickByIdAsync(int playerId)
        {
            if (playerId == 400)
            {
                return (new Failure("Mocked Bad Request"), null);
            }
            else if (playerId == 2137)
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return (new Success(), new PlayerDto
                {
                    PlayerId = playerId,
                    Name = "Mocked Player",
                    IsDead = false,
                    RoomId = null,
                    TeamId = null
                });
            }
        }

        public async Task<HttpResult> DeleteAsync()
        {
            return new Success();
        }
    }
}
