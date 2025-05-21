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
        public async Task<HttpResult<Player>> RegisterPlayerAsync(PostPlayerDto playerDto)
        {
            Player player = new Player
            {
                Jwt = "mock-jwt-token",
            };

            return await Task.FromResult(new Success<Player>(player));
        }

        public async Task<HttpResult<Player>> LogInToAccountAsync(LoginAccountDto accountDto)
        {
            Player player = new Player
            {
                Jwt = "mock-jwt-token",
            };

            return await Task.FromResult(new Success<Player>(player));
        }

        public async Task<HttpResult<Player>> SignUpAccountAsync(RegisterAccountDto accountDto)
        {
            Player player = new Player
            {
                Jwt = "mock-jwt-token",
            };

            return await Task.FromResult(new Success<Player>(player));
        }
    }
}
