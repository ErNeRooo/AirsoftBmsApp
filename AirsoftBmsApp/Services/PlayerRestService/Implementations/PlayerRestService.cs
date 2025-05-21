using AirsoftBmsApp.Model;
using AirsoftBmsApp.Model.Dto.Login;
using AirsoftBmsApp.Model.Dto.Post;
using AirsoftBmsApp.Model.Dto.Register;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.PlayerDataService.Abstractions;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Services.PlayerRestService.Implementations
{
    public class PlayerRestService : IPlayerRestService
    {
        HttpClient _client;
        JsonSerializerOptions _serializeOptions;
        static string _baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8080/Player" : "http://localhost:8080/Player";

        public PlayerRestService()
        {
            _client = new HttpClient();
            _serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }


        public async Task<HttpResult<Player>> RegisterPlayerAsync(PostPlayerDto playerDto)
        {
            var json = JsonSerializer.Serialize(playerDto, _serializeOptions);
            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{_baseUrl}/register", stringContent);

            if (response.IsSuccessStatusCode)
            {
                var jwt = await response.Content.ReadAsStringAsync();
                var path = response.Headers.Location?.ToString();
                var idString = path?.Split('/').LastOrDefault();

                int.TryParse(idString, out int id);

                return new Success<Player>(new Player { Jwt = jwt, Id = id});
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return new Failure<Player>(errorContent);
            }
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
