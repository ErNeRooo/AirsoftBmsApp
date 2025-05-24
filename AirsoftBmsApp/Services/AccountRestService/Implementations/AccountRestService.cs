using AirsoftBmsApp.Model;
using AirsoftBmsApp.Model.Dto.Account;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.PlayerRestService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Services.PlayerRestService.Implementations
{
    public class AccountRestService : IAccountRestService
    {
        HttpClient _client;
        JsonSerializerOptions _serializeOptions;
        IJwtTokenService _jwtTokenService;

        public AccountRestService(HttpClient httpClient, IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
            _client = httpClient;
            _serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };
        }

        public async Task<HttpResult> GetAsync(int accountId)
        {
            try
            {
                SetAuthorizationHeader();

                var response = await _client.GetAsync($"id/{accountId}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStreamAsync();
                    var account = await JsonSerializer.DeserializeAsync<AccountDto>(json, _serializeOptions);

                    return new Success<AccountDto>(new AccountDto { Email = account.Email, Id = account.Id });
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

        public async Task<HttpResult> PutAsync(PutAccountDto accountDto, int accoundId)
        {
            try
            {
                SetAuthorizationHeader();

                var json = JsonSerializer.Serialize(accountDto, _serializeOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PutAsync($"id/{accoundId}", content);

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

        public async Task<HttpResult> DeleteAsync(int accoundId)
        {
            try
            {
                SetAuthorizationHeader();

                var response = await _client.DeleteAsync($"id/{accoundId}");

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

        public async Task<HttpResult> SignUpAsync(SignupAccountDto accountDto)
        {
            try
            {
                SetAuthorizationHeader();

                var json = JsonSerializer.Serialize(accountDto, _serializeOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"signup", content);

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

        public async Task<HttpResult> LogInAsync(LogInAccountDto accountDto)
        {
            try
            {
                SetAuthorizationHeader();

                var json = JsonSerializer.Serialize(accountDto, _serializeOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync($"login", content);

                if (response.IsSuccessStatusCode)
                {
                    var path = response.Headers.Location?.ToString();
                    var idString = path?.Split('/').LastOrDefault();

                    int.TryParse(idString, out int id);

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
    }
}