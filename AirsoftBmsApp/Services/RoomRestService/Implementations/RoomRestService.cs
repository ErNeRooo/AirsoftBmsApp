using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.RestHelperService.Abstractions;
using System.Net.Http.Headers;

namespace AirsoftBmsApp.Services.PlayerRestService.Implementations
{
    public class RoomRestService(HttpClient client, IJsonHelperService jsonHelper, IJwtTokenService jwtTokenService) : IRoomRestService
    {
        public async Task<(HttpResult result, RoomIncludingRelatedEntitiesDto? room)> GetByIdAsync(int roomId)
        {
            SetAuthorizationHeader();

            var response = await client.GetAsync($"id/{roomId}");

            if (response.IsSuccessStatusCode)
            {
                var room = await jsonHelper.DeserializeFromResponseAsync<RoomIncludingRelatedEntitiesDto>(response);

                return (new Success(), room);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return (new Failure(errorContent), null);
            }
        }

        public async Task<(HttpResult result, RoomIncludingRelatedEntitiesDto? room)> GetByJoinCodeAsync(string joinCode)
        {
            SetAuthorizationHeader();

            var response = await client.GetAsync($"join-code/{joinCode}");

            if (response.IsSuccessStatusCode)
            {
                var room = jsonHelper.DeserializeFromResponseAsync<RoomIncludingRelatedEntitiesDto>(response).Result;

                return (new Success(), room);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return (new Failure(errorContent), null);
            }

        }

        public async Task<(HttpResult result, RoomDto? room)> PutAsync(PutRoomDto roomDto)
        {
            SetAuthorizationHeader();

            StringContent stringContent = jsonHelper.GetStringContent(roomDto);

            var response = await client.PutAsync("", stringContent);

            if (response.IsSuccessStatusCode)
            {
                var room = jsonHelper.DeserializeFromResponseAsync<RoomDto>(response).Result;

                return (new Success(), room);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return (new Failure(errorContent), null);
            }
        }

        public async Task<(HttpResult result, RoomDto? room)> PostAsync(PostRoomDto roomDto)
        {
            SetAuthorizationHeader();

            StringContent stringContent = jsonHelper.GetStringContent(roomDto);

            var response = await client.PostAsync("", stringContent);

            if (response.IsSuccessStatusCode)
            {
                var room = jsonHelper.DeserializeFromResponseAsync<RoomDto>(response).Result;

                return (new Success(), room);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                return (new Failure(errorContent), null);
            }
        }

        public async Task<HttpResult> DeleteAsync()
        {
            SetAuthorizationHeader();

            var response = await client.DeleteAsync("");

            if (response.IsSuccessStatusCode)
            {
                return new Success();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return new Failure(errorContent);
            }
        }

        public async Task<(HttpResult result, RoomIncludingRelatedEntitiesDto? room)> JoinAsync(JoinRoomDto roomDto)
        {
            SetAuthorizationHeader();

            var content = jsonHelper.GetStringContent(roomDto);

            var response = await client.PostAsync("join", content);

            if (response.IsSuccessStatusCode)
            {
                var room = jsonHelper.DeserializeFromResponseAsync<RoomIncludingRelatedEntitiesDto>(response).Result;

                return (new Success(), room);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return (new Failure(errorContent), null);
            }
        }

        public async Task<HttpResult> LeaveAsync()
        {
            SetAuthorizationHeader();

            var response = await client.PostAsync("leave", null);

            if (response.IsSuccessStatusCode)
            {
                return new Success();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return new Failure(errorContent);
            }
        }

        private void SetAuthorizationHeader()
        {
            if (!string.IsNullOrEmpty(jwtTokenService.Token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtTokenService.Token);
            }
            else
            {
                throw new Exception("No JWT token");
            }
        }
    }
}
