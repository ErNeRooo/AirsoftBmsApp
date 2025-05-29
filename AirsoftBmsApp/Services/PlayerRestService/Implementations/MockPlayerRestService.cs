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
        public async Task<HttpResult> TryRequest(PlayerRequestIntent playerRequest)
        {
            try
            {
                await Task.Delay(100);

                switch (playerRequest)
                {
                    case GetPlayerByIdAsync getById:
                        return await GetAsync(getById.playerId);
                    case PutPlayerAsync put:
                        return await PutAsync(put.playerDto, put.playerId);
                    case RegisterPlayerAsync post:
                        return await RegisterAsync(post.playerDto);
                    case DeletePlayerAsync delete:
                        return await DeleteAsync(delete.playerId);
                    default:
                        return new Failure("Unknown request type");
                }
            }
            catch (Exception ex)
            {
                return new Error(ex.Message);
            }
        }

        public async Task<HttpResult> GetAsync(int playerId)
        {
            if (playerId == 400)
            {
                return new Failure("Mocked Bad Request");
            }
            else if (playerId == 2137)
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return new Success<PlayerDto>(new PlayerDto
                {
                    Id = playerId,
                    Name = "Mocked Player",
                    IsDead = false,
                    AccountId = 1,
                    RoomId = 1,
                    TeamId = 1
                });
            }
        }

        public async Task<HttpResult> PutAsync(PutPlayerDto playerDto, int playerId)
        {
            if (playerId == 400)
            {
                return new Failure("Mocked Bad Request");
            }
            else if (playerId == 2137)
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return new Success<object>(null);
            }
        }
        public async Task<HttpResult> RegisterAsync(PostPlayerDto playerDto)
        {
            if (playerDto.Name == "400")
            {
                return new Failure("Mocked Bad Request");
            }
            else if (playerDto.Name == "2137")
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return new Success<int>(1);
            }
        }

        public async Task<HttpResult> DeleteAsync(int playerId)
        {
            if (playerId == 400)
            {
                return new Failure("Mocked Bad Request");
            }
            else if (playerId == 2137)
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return new Success<object>(null);
            }
        }

        
    }
}
