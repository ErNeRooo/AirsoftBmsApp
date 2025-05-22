using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AirsoftBmsApp.Model;
using AirsoftBmsApp.Model.Dto.Login;
using AirsoftBmsApp.Model.Dto.Post;
using AirsoftBmsApp.Model.Dto.Register;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;

namespace AirsoftBmsApp.Services.PlayerRestService.Implementations
{
    public class MockPlayerRestService : IPlayerRestService
    {
        private async Task<HttpResult<Player>> GetMockedResults(string inputString) {

            if (inputString == "not found" || inputString == "404")
            {
                await Task.Delay(3000);
                return await Task.FromResult(new Failure<Player>("Not Found"));
            }
            else if (inputString == "bad request" || inputString == "400")
            {
                return await Task.FromResult(new Failure<Player>("Internal server error"));
            }
            else if (inputString == "internal server error" || inputString == "500")
            {
                return await Task.FromResult(new Failure<Player>("Internal server error"));
            }
            else
            {
                Player player = new Player
                {
                    Jwt = "mock-jwt-token",
                };

                await Task.Delay(3000);
                return await Task.FromResult(new Success<Player>(player));
            }
        }


        public async Task<HttpResult<Player>> RegisterPlayerAsync(PostPlayerDto playerDto)
        {
            return await Task.FromResult(await GetMockedResults(playerDto.Name));
        }

        public async Task<HttpResult<Player>> LogInToAccountAsync(LoginAccountDto accountDto)
        {
            return await Task.FromResult(await GetMockedResults(accountDto.Password));
        }

        public async Task<HttpResult<Player>> SignUpAccountAsync(RegisterAccountDto accountDto)
        {
            return await Task.FromResult(await GetMockedResults(accountDto.Name));
        }
    }
}
