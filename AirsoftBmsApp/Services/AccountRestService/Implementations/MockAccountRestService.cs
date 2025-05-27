using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AirsoftBmsApp.Model;
using AirsoftBmsApp.Model.Dto.Account;
using AirsoftBmsApp.Model.Dto.Account;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.AccountRestService.Abstractions;

namespace AirsoftBmsApp.Services.AccountRestService.Implementations
{
    public class MockAccountRestService : IAccountRestService
    {
        public async Task<HttpResult> TryRequest(AccountRequestIntent accountRequest)
        {
            try
            {
                await Task.Delay(1000);

                switch (accountRequest)
                {
                    case GetAccountByIdAsync getById:
                        return await GetAsync(getById.accountId);
                    case PutAccountAsync put:
                        return await PutAsync(put.accountDto, put.accountId);
                    case DeleteAccountAsync delete:
                        return await DeleteAsync(delete.accountId);
                    case SignUpAccountAsync signUp:
                        return await SignUpAsync(signUp.accountDto);
                    case LogInAccountAsync logIn:
                        return await LogInAsync(logIn.accountDto);
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
            if (accountId == 400)
            {
                return new Failure("Mocked Bad Request");
            }
            else if (accountId == 2137)
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return new Success<object>(null);
            }
        }

        public async Task<HttpResult> PutAsync(PutAccountDto accountDto, int accountId)
        {
            if (accountId == 400)
            {
                return new Failure("Mocked Bad Request");
            }
            else if (accountId == 2137)
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return new Success<object>(null);
            }
        }

        public async Task<HttpResult> DeleteAsync(int accountId)
        {
            if (accountId == 400)
            {
                return new Failure("Mocked Bad Request");
            }
            else if (accountId == 2137)
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return new Success<object>(null);
            }
        }

        public async Task<HttpResult> SignUpAsync(SignUpAccountDto accountDto)
        {
            if (accountDto.Email == "400@0.0")
            {
                return new Failure("Mocked Bad Request");
            }
            else if (accountDto.Email == "2137@0.0")
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return new Success<int>(1);
            }
        }

        public async Task<HttpResult> LogInAsync(LogInAccountDto accountDto)
        {
            if (accountDto.Email == "400@0.0")
            {
                return new Failure("Mocked Bad Request");
            }
            else if (accountDto.Email == "2137@0.0")
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return new Success<int>(1);
            }
        }
    }
}
