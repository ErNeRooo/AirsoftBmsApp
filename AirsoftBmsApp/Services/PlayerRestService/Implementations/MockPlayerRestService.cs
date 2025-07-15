using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AirsoftBmsApp.Model;
using AirsoftBmsApp.Model.Dto.Account;
using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;

namespace AirsoftBmsApp.Services.PlayerRestService.Implementations
{
    public class MockPlayerRestService : IPlayerRestService
    {
        public async Task<(HttpResult result, PlayerDto? player)> GetMeAsync()
        {
            return (new Success(), new PlayerDto
            {
                PlayerId = 1,
                Name = "Mocked Player",
                IsDead = false,
                RoomId = 1,
                TeamId = 1
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
                    PlayerId = 1,
                    Name = "Mocked Player",
                    IsDead = false,
                    RoomId = 1,
                    TeamId = 1
                });
            }
        }

        public async Task<(HttpResult result, PlayerDto? player)> PutAsync(PutPlayerDto playerDto)
        {
            return (new Success(), new PlayerDto
            {
                PlayerId = 1,
                Name = playerDto.Name,
                IsDead = false,
                RoomId = 1,
                TeamId = 1
            });
        }
        public async Task<(HttpResult result, int? playerId)> RegisterAsync(PostPlayerDto playerDto)
        {
            if (playerDto.Name == "400")
            {
                return (new Failure("Mocked Bad Request"), null);
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
                    RoomId = 1,
                    TeamId = 1
                });
            }
        }

        public async Task<HttpResult> DeleteAsync()
        {
            return new Success();
        }
    }
}
