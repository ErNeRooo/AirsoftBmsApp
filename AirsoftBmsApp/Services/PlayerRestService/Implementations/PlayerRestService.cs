using AirsoftBmsApp.Model;
using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Dto.Post;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using System.Net.Http.Headers;
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


        public async Task<HttpResult> GetAsync(int playerId)
        {
            try
            {
                SetAuthorizationHeader();

                var response = await _client.GetAsync($"id/{playerId}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStreamAsync();
                    var player = await JsonSerializer.DeserializeAsync<PlayerDto>(json, _serializeOptions);

                    return new Success<PlayerDto>(player);
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

        public async Task<HttpResult> PutAsync(PostPlayerDto playerDto, int playerId)
        {
            try
            {
                SetAuthorizationHeader();

                StringContent stringContent = GetStringContet(playerDto);

                var response = await _client.PutAsync($"id/{playerId}", stringContent);

                if (response.IsSuccessStatusCode)
                {
                    return new Success<object>(null);
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

        public async Task<HttpResult> RegisterAsync(PostPlayerDto playerDto)
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

        public async Task<HttpResult> DeleteAsync(int playerId)
        {
            try
            {
                SetAuthorizationHeader();

                var response = await _client.DeleteAsync($"id/{playerId}");

                if (response.IsSuccessStatusCode)
                {
                    return new Success<object>(null);
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

        private void SetAuthorizationHeader()
        {
            if (!string.IsNullOrEmpty(_jwtTokenService.Token))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtTokenService.Token);
            }
            else
            {
                throw new Exception("No JWT token");
            }
        }

        private StringContent GetStringContet(object accountDto)
        {
            var json = JsonSerializer.Serialize(accountDto, _serializeOptions);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
