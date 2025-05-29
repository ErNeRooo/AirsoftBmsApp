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
        public async Task<HttpResult> TryRequest(TeamRequestIntent roomRequest)
        {
            SetAuthorizationHeader();

            switch (roomRequest)
            {
                case GetTeamByIdAsync getById:
                    return await GetByIdAsync(getById.teamId);
                case PutTeam put:
                    return await PutAsync(put.teamDto, put.teamId);
                case PostTeam post:
                    return await PostAsync(post.teamDto);
                case DeleteTeam delete:
                    return await DeleteAsync(delete.teamId);

            }

            throw new NotImplementedException();
        }

        private async Task<HttpResult> GetByIdAsync(int teamId)
        {
            var response = await client.GetAsync($"id/{teamId}");

            if (response.IsSuccessStatusCode)
            {
                var team = await jsonHelper.DeserializeFromResponseAsync<TeamDto>(response);
                return new Success<TeamDto>(team);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return new Failure(errorContent);
            }
        }

        private async Task<HttpResult> PutAsync(PutTeamDto teamDto, int teamId)
        {
            var content = jsonHelper.GetStringContent(teamDto);

            var response = await client.PutAsync($"id/{teamId}", content);

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

        private async Task<HttpResult> PostAsync(PostTeamDto teamDto)
        {
            var content = jsonHelper.GetStringContent(teamDto);

            var response = await client.PostAsync("", content);

            if (response.IsSuccessStatusCode)
            {
                int id = GetLocationIdFromResponse(response);

                return new Success<int>(id);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return new Failure(errorContent);
            }
        }

        private async Task<HttpResult> DeleteAsync(int teamId)
        {
            var response = await client.DeleteAsync($"id/{teamId}");

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
    }
}
