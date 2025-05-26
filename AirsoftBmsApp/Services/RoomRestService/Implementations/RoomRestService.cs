using AirsoftBmsApp.Model;
using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Dto.Post;
using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AirsoftBmsApp.Services.PlayerRestService.Implementations
{
    public class RoomRestService : IRoomRestService
    {
        HttpClient _client;
        JsonSerializerOptions _serializeOptions;
        IJwtTokenService _jwtTokenService;

        public RoomRestService(HttpClient httpClient, IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
            _client = httpClient;
            _serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }

        public async Task<HttpResult> GetByIdAsync(int roomId)
        {
            try
            {
                SetAuthorizationHeader();

                var response = await _client.GetAsync($"id/{roomId}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStreamAsync();
                    var room = await JsonSerializer.DeserializeAsync<RoomDto>(json, _serializeOptions);

                    return new Success<RoomDto>(room);
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

        public async Task<HttpResult> GetByJoinCodeAsync(string joinCode)
        {
            try
            {
                SetAuthorizationHeader();

                var response = await _client.GetAsync($"join-code/{joinCode}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStreamAsync();
                    var room = await JsonSerializer.DeserializeAsync<RoomDto>(json, _serializeOptions);

                    return new Success<RoomDto>(room);
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

        public async Task<HttpResult> PutAsync(PutRoomDto roomDto, int roomId)
        {
            try
            {
                SetAuthorizationHeader();

                StringContent stringContent = GetStringContent(roomDto);

                var response = await _client.PutAsync($"id/{roomId}", stringContent);

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

        public async Task<HttpResult> PostAsync(PostRoomDto roomDto)
        {
            try
            {
                StringContent stringContent = GetStringContent(roomDto);

                var response = await _client.PostAsync($"register", stringContent);

                if (response.IsSuccessStatusCode)
                {
                    var path = response.Headers.Location?.ToString();
                    var idString = path?.Split('/').LastOrDefault();

                    int.TryParse(idString, out int id);

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

        public async Task<HttpResult> DeleteAsync(int roomId)
        {
            try
            {
                SetAuthorizationHeader();

                var response = await _client.DeleteAsync($"id/{roomId}");

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

        public async Task<HttpResult> JoinAsync(LogInRoomDto roomDto)
        {
            try
            {
                SetAuthorizationHeader();

                var content = GetStringContent(roomDto);

                var response = await _client.PostAsync("join", content);

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

        public async Task<HttpResult> LeaveAsync()
        {
            try
            {
                SetAuthorizationHeader();

                var response = await _client.PostAsync("leave", null);

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

        private StringContent GetStringContent(object accountDto)
        {
            var json = JsonSerializer.Serialize(accountDto, _serializeOptions);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
