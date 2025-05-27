using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.RestHelperService.Abstractions;
using AirsoftBmsApp.Services.RoomRestService.Abstractions;
using System.Net.Http.Headers;

namespace AirsoftBmsApp.Services.PlayerRestService.Implementations
{
    public class RoomRestService(HttpClient client, IJsonHelperService jsonHelper, IJwtTokenService jwtTokenService) : IRoomRestService
    {
        public async Task<HttpResult> TryRequest(RoomRequestIntent roomRequest)
        {
            try
            {
                SetAuthorizationHeader();

                switch (roomRequest)
                {
                    case GetRoomByIdAsync getById:
                        return await GetByIdAsync(getById.roomId);
                    case GetRoomByJoinCodeAsync getByJoinCode:
                        return await GetByJoinCodeAsync(getByJoinCode.joinCode);
                    case PutRoomAsync put:
                        return await PutAsync(put.roomDto, put.roomId);
                    case PostRoomAsync post:
                        return await PostAsync(post.roomDto);
                    case DeleteRoomAsync delete:
                        return await DeleteAsync(delete.roomId);
                    case JoinRoomAsync join:
                        return await JoinAsync(join.roomDto);
                    case LeaveRoomAsync leave:
                        return await LeaveAsync();
                    default:
                        return new Failure("Unknown request type");
                }
            }
            catch (Exception ex)
            {
                return new Error(ex.Message);
            }
        }

        private async Task<HttpResult> GetByIdAsync(int roomId)
        {
            var response = await client.GetAsync($"id/{roomId}");

            if (response.IsSuccessStatusCode)
            {
                var room = await jsonHelper.DeserializeFromResponseAsync<RoomDto>(response);

                return new Success<RoomDto>(room);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return new Failure(errorContent);
            }
        }

        private async Task<HttpResult> GetByJoinCodeAsync(string joinCode)
        {
            var response = await client.GetAsync($"join-code/{joinCode}");

            if (response.IsSuccessStatusCode)
            {
                var room = jsonHelper.DeserializeFromResponseAsync<RoomDto>(response).Result;

                return new Success<RoomDto>(room);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return new Failure(errorContent);
            }
            
        }

        private async Task<HttpResult> PutAsync(PutRoomDto roomDto, int roomId)
        {
            StringContent stringContent = jsonHelper.GetStringContent(roomDto);

            var response = await client.PutAsync($"id/{roomId}", stringContent);

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

        private async Task<HttpResult> PostAsync(PostRoomDto roomDto)
        {
            StringContent stringContent = jsonHelper.GetStringContent(roomDto);

            var response = await client.PostAsync($"register", stringContent);

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

        private async Task<HttpResult> DeleteAsync(int roomId)
        {
            var response = await client.DeleteAsync($"id/{roomId}");

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

        private async Task<HttpResult> JoinAsync(JoinRoomDto roomDto)
        {
            var content = jsonHelper.GetStringContent(roomDto);

            var response = await client.PostAsync("join", content);

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

        private async Task<HttpResult> LeaveAsync()
        {
            var response = await client.PostAsync("leave", null);

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
