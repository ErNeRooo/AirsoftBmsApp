using AirsoftBmsApp.Model.Dto.Account;
using AirsoftBmsApp.Networking;
using AirsoftBmsApp.Services.AccountRestService.Abstractions;

namespace AirsoftBmsApp.Services.AccountRestService.Implementations
{
    public class MockAccountRestService : IAccountRestService
    {
        public async Task<(HttpResult result, AccountDto? account)> GetAsync(int accountId)
        {
            if (accountId == 400)
            {
                return (new Failure("Mocked Bad Request"), null);
            }
            else if (accountId == 2137)
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return (new Success(), new AccountDto
                {
                    AccountId = accountId,
                    PlayerId = 1,
                    Email = "cokolwiek@gmail.com",
                });
            }
        }

        public async Task<(HttpResult result, AccountDto? account)> PutAsync(PutAccountDto accountDto, int accountId)
        {
            if (accountId == 400)
            {
                return (new Failure("Mocked Bad Request"), null);
            }
            else if (accountId == 2137)
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return (new Success(), new AccountDto
                {
                    AccountId = accountId,
                    PlayerId = 1,
                    Email = accountDto.Email,
                });
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
                return new Success();
            }
        }

        public async Task<(HttpResult result, AccountDto? account)> SignUpAsync(SignUpAccountDto accountDto)
        {
            if (accountDto.Email == "400@0.0")
            {
                return (new Failure("Mocked Bad Request"), null);
            }
            else if (accountDto.Email == "2137@0.0")
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return (new Success(), new AccountDto
                {
                    AccountId = 1,
                    PlayerId = 1,
                    Email = accountDto.Email,
                });
            }
        }

        public async Task<(HttpResult result, AccountDto? account)> LogInAsync(LogInAccountDto accountDto)
        {
            if (accountDto.Email == "400@0.0")
            {
                return (new Failure("Mocked Bad Request"), null);
            }
            else if (accountDto.Email == "2137@0.0")
            {
                throw new Exception("Mocked Exception for id 2137");
            }
            else
            {
                return (new Success(), new AccountDto
                {
                    AccountId = 1,
                    PlayerId = 1,
                    Email = accountDto.Email,
                });
            }
        }
    }
}
