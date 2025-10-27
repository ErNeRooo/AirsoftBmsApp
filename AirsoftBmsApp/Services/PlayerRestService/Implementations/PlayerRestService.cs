using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using AirsoftBmsApp.Services.RestHelperService.Abstractions;
using System.Net.Http.Headers;

namespace AirsoftBmsApp.Services.PlayerRestService.Implementations;

public class PlayerRestService(HttpClient client, IJwtTokenService jwtTokenService, IJsonHelperService jsonHelper) : IPlayerRestService
{
    public async Task<(HttpResult result, PlayerDto? player)> GetMeAsync()
    {
        SetAuthorizationHeader();

        var response = await client.GetAsync("me");

        if (response.IsSuccessStatusCode)
        {
            PlayerDto player = jsonHelper.DeserializeFromResponseAsync<PlayerDto>(response).Result;

            return (new Success(), player);
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return (new Failure(errorContent), null);
        }
    }

    public async Task<(HttpResult result, PlayerDto? player)> GetByIdAsync(int playerId)
    {
        SetAuthorizationHeader();

        var response = await client.GetAsync($"id/{playerId}");

        if (response.IsSuccessStatusCode)
        {
            PlayerDto player = jsonHelper.DeserializeFromResponseAsync<PlayerDto>(response).Result;

            return (new Success(), player);
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return (new Failure(errorContent), null);
        }
    }

    public async Task<(HttpResult result, PlayerDto? player)> PutAsync(PutPlayerDto playerDto, int playerId)
    {
        SetAuthorizationHeader();

        StringContent stringContent = jsonHelper.GetStringContent(playerDto);

        var response = await client.PutAsync("", stringContent);

        if (response.IsSuccessStatusCode)
        {
            PlayerDto player = jsonHelper.DeserializeFromResponseAsync<PlayerDto>(response).Result;

            return (new Success(), player);
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return (new Failure(errorContent), null);
        }
    }

    public async Task<(HttpResult result, int? playerId)> RegisterAsync(PostPlayerDto playerDto)
    {
        StringContent stringContent = jsonHelper.GetStringContent(playerDto);

        var response = await client.PostAsync($"register", stringContent);

        if (response.IsSuccessStatusCode)
        {
            var jwt = await response.Content.ReadAsStringAsync();
            int id = GetLocationIdFromResponse(response);

            jwtTokenService.Token = jwt;

            return (new Success(), id);
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return (new Failure(errorContent), null);
        }
    }

    public async Task<(HttpResult result, PlayerDto? player)> KickFromRoomByIdAsync(int playerId)
    {
        SetAuthorizationHeader();

        var response = await client.PostAsync($"kick-from-room/playerId/{playerId}", null);

        if (response.IsSuccessStatusCode)
        {
            PlayerDto player = jsonHelper.DeserializeFromResponseAsync<PlayerDto>(response).Result;

            return (new Success(), player);
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return (new Failure(errorContent), null);
        }
    }

    public async Task<(HttpResult result, PlayerDto? player)> KickFromTeamByIdAsync(int playerId)
    {
        SetAuthorizationHeader();

        var response = await client.PostAsync($"kick-from-team/playerId/{playerId}", null);

        if (response.IsSuccessStatusCode)
        {
            PlayerDto player = jsonHelper.DeserializeFromResponseAsync<PlayerDto>(response).Result;

            return (new Success(), player);
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
