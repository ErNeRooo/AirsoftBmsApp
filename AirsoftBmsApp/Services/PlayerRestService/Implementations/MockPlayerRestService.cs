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
        public Task<HttpResult> TryRequest(PlayerRequestIntent playerRequest)
        {
            throw new NotImplementedException();
        }

        private async Task<HttpResult> GetMockedResults(string inputString) {

            if (inputString == "not found" || inputString == "404" || inputString == "4@0.4")
            {
                await Task.Delay(3000);
                return await Task.FromResult(new Failure("Not Found"));
            }
            else if (inputString == "bad request" || inputString == "400" || inputString == "4@0.0")
            {
                return await Task.FromResult(new Failure("Internal server error"));
            }
            else if (inputString == "internal server error" || inputString == "500" || inputString == "5@0.0")
            {
                return await Task.FromResult(new Failure("Internal server error"));
            }
            else
            {
                Player player = new Player
                {
                    Name = "Mockry player"
                };

                await Task.Delay(3000);
                return await Task.FromResult(new Success<Player>(player));
            }
        }


        public async Task<HttpResult> RegisterPlayerAsync(PostPlayerDto playerDto)
        {
            return await Task.FromResult(await GetMockedResults(playerDto.Name));
        }

        public Task<HttpResult> GetAsync(int playerId)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult> PutAsync(PutPlayerDto playerDto, int playerId)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult> RegisterAsync(PostPlayerDto playerDto)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResult> DeleteAsync(int playerId)
        {
            throw new NotImplementedException();
        }

        
    }
}
