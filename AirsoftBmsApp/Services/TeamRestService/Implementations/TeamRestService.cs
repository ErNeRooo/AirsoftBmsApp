using AirsoftBmsApp.Model.Dto.Team;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.RestHelperService.Abstractions;
using AirsoftBmsApp.Services.TeamRestService.Abstractions;
using System.Net.Http.Headers;

namespace AirsoftBmsApp.Services.TeamRestService.Implementations
{
    public class TeamRestService(HttpClient client, IJsonHelperService jsonHelper, IJwtTokenService jwtTokenService) : ITeamRestService
    {
        public async Task<(HttpResult result, TeamDto? team)> GetByIdAsync(int teamId)
        {
            SetAuthorizationHeader();

            var response = await client.GetAsync($"id/{teamId}");

            if (response.IsSuccessStatusCode)
            {
                var team = await jsonHelper.DeserializeFromResponseAsync<TeamDto>(response);
                return (new Success(), team);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return (new Failure(errorContent), null);
            }
        }

        public async Task<(HttpResult result, TeamDto? team)> PutAsync(PutTeamDto teamDto, int teamId)
        {
            SetAuthorizationHeader();

            var content = jsonHelper.GetStringContent(teamDto);

            var response = await client.PutAsync($"id/{teamId}", content);

            if (response.IsSuccessStatusCode)
            {
                var team = await jsonHelper.DeserializeFromResponseAsync<TeamDto>(response);
                return (new Success(), team);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return (new Failure(errorContent), null);
            }
        }

        public async Task<(HttpResult result, TeamDto? team)> PostAsync(PostTeamDto teamDto)
        {
            SetAuthorizationHeader();

            var content = jsonHelper.GetStringContent(teamDto);

            var response = await client.PostAsync("create", content);

            if (response.IsSuccessStatusCode)
            {
                var team = await jsonHelper.DeserializeFromResponseAsync<TeamDto>(response);
                return (new Success(), team);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return (new Failure(errorContent), null);
            }
        }

        public async Task<HttpResult> DeleteAsync(int teamId)
        {
            SetAuthorizationHeader();

            var response = await client.DeleteAsync($"id/{teamId}");

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
            if (jwtTokenService.Token is not null)
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
