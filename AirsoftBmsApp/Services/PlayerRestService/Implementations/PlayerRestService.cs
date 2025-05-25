using AirsoftBmsApp.Model;
using AirsoftBmsApp.Model.Dto.Post;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using System.Text;
using System.Text.Json;

namespace AirsoftBmsApp.Services.PlayerRestService.Implementations
{
    public class PlayerRestService : IPlayerRestService
    {
        HttpClient _client;
        JsonSerializerOptions _serializeOptions;
        IJwtTokenService _jwtTokenService;

        public PlayerRestService(HttpClient httpClient, IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
            _client = httpClient;
            _serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }


        public async Task<HttpResult> RegisterPlayerAsync(PostPlayerDto playerDto)
        {
            try
            {
                var json = JsonSerializer.Serialize(playerDto, _serializeOptions);
                StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"register", stringContent);

                if (response.IsSuccessStatusCode)
                {
                    var jwt = await response.Content.ReadAsStringAsync();
                    var path = response.Headers.Location?.ToString();
                    var idString = path?.Split('/').LastOrDefault();

                    int.TryParse(idString, out int id);

                    _jwtTokenService.Token = jwt;

                    return new Success<int>(id);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new Failure(errorContent);
                }
            }
            catch (Exception ex)
            {
                return new Error(ex.Message);
            }
        }
    }
}
