using AirsoftBmsApp.Model.Dto.Account;
using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.AccountRestService.Abstractions
{
    public interface IAccountRestService
    {
        public Task<(HttpResult result, AccountDto? account)> GetAsync(int accountId);
        public Task<(HttpResult result, AccountDto? account)> PutAsync(PutAccountDto accountDto, int accoundId);
        public Task<HttpResult> DeleteAsync(int accoundId);
        public Task<(HttpResult result, AccountDto? account)> SignUpAsync(SignUpAccountDto accountDto);
        public Task<(HttpResult result, AccountDto? account)> LogInAsync(LogInAccountDto accountDto);
    }
}
