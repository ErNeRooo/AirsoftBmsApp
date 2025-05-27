using AirsoftBmsApp.Model;
using AirsoftBmsApp.Model.Dto.Account;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.AccountRestService.Abstractions;
using AirsoftBmsApp.Services.JwtTokenService;
using AirsoftBmsApp.Services.RestHelperService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Services.AccountRestService.Implementations
{
    public class AccountRestService(HttpClient client, IJwtTokenService jwtTokenService, IJsonHelperService jsonHelper) : IAccountRestService
    {
        public async Task<HttpResult> TryRequest(AccountRequestIntent accountRequest)
        {
            try
            {
                SetAuthorizationHeader();

                switch (accountRequest)
                {
                    case GetAccountByIdAsync getById:
                        return await GetAsync(getById.accountId);
                    case PutAccountAsync put:
                        return await PutAsync(put.accountDto, put.accountId);
                    case SignUpAccountAsync signUp:
                        return await SignUpAsync(signUp.accountDto);
                    case LogInAccountAsync logIn:
                        return await LogInAsync(logIn.accountDto);
                    case DeleteAccountAsync delete:
                        return await DeleteAsync(delete.accountId);
                    default:
                        return new Failure("Unknown request type");
                }
            }
            catch (Exception ex)
            {
                return new Error(ex.Message);
            }
        }

        public async Task<HttpResult> GetAsync(int accountId)
        {
            var response = await client.GetAsync($"id/{accountId}");

            if (response.IsSuccessStatusCode)
            {
                var account = await jsonHelper.DeserializeFromResponseAsync<AccountDto>(response);

                return new Success<AccountDto>(account);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return new Failure(errorContent);
            }
        }

        public async Task<HttpResult> PutAsync(PutAccountDto accountDto, int accoundId)
        {
            var stringContent = jsonHelper.GetStringContent(accountDto);

            var response = await client.PutAsync($"id/{accoundId}", stringContent);

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

        public async Task<HttpResult> DeleteAsync(int accoundId)
        {
            var response = await client.DeleteAsync($"id/{accoundId}");

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

        public async Task<HttpResult> SignUpAsync(SignUpAccountDto accountDto)
        {
            var stringContent = jsonHelper.GetStringContent(accountDto);

            var response = await client.PostAsync($"signup", stringContent);

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

        public async Task<HttpResult> LogInAsync(LogInAccountDto accountDto)
        {
            var stringContent = jsonHelper.GetStringContent(accountDto);

            var response = await client.PostAsync($"login", stringContent);

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