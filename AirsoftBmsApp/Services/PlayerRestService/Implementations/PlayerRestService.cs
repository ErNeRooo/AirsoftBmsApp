using AirsoftBmsApp.Model;
using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.RestHelperService.Abstractions;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AirsoftBmsApp.Services.PlayerRestService.Implementations
{
    public class PlayerRestService(HttpClient client, IJwtTokenService jwtTokenService, IJsonHelperService jsonHelper) : IPlayerRestService
    {
        public async Task<HttpResult> TryRequest(PlayerRequestIntent playerRequest)
        {
            try
            {
                switch (playerRequest)
                {
                    case GetPlayerByIdAsync getById:
                        SetAuthorizationHeader();
                        return await GetAsync(getById.playerId);
                    case PutPlayerAsync put:
                        SetAuthorizationHeader();
                        return await PutAsync(put.playerDto, put.playerId);
                    case RegisterPlayerAsync post:
                        return await RegisterAsync(post.playerDto);
                    case DeletePlayerAsync delete:
                        SetAuthorizationHeader();
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

        private async Task<HttpResult> GetAsync(int playerId)
        {
            var response = await client.GetAsync($"id/{playerId}");

            if (response.IsSuccessStatusCode)
            {
                var player = jsonHelper.DeserializeFromResponseAsync<PlayerDto>(response).Result;

                return new Success<PlayerDto>(player);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return new Failure(errorContent);
            }
        }

        private async Task<HttpResult> PutAsync(PutPlayerDto playerDto, int playerId)
        {
            StringContent stringContent = jsonHelper.GetStringContent(playerDto);

            var response = await client.PutAsync($"id/{playerId}", stringContent);

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

        private async Task<HttpResult> RegisterAsync(PostPlayerDto playerDto)
        {
            StringContent stringContent = jsonHelper.GetStringContent(playerDto);

            var response = await client.PostAsync($"register", stringContent);

            if (response.IsSuccessStatusCode)
            {
                var jwt = await response.Content.ReadAsStringAsync();
                int id = GetLocationIdFromResponse(response);

                jwtTokenService.Token = jwt;

                return new Success<int>(id);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return new Failure(errorContent);
            }
        }

        private async Task<HttpResult> DeleteAsync(int playerId)
        {
            var response = await client.DeleteAsync($"id/{playerId}");

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

        private int GetLocationIdFromResponse(HttpResponseMessage response)
        {
            var path = response.Headers.Location?.ToString();
            var idString = path?.Split('/').LastOrDefault();

            bool isParsingSuccessfull = int.TryParse(idString, out int id);

            if (isParsingSuccessfull)
            {
                return id;
            }
            throw new Exception("Failed to parse ID from response location header.");
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
