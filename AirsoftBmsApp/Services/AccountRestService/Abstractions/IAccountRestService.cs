using AirsoftBmsApp.Model.Dto.Account;
using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.AccountRestService.Abstractions
{
    public interface IAccountRestService
    {
        public Task<HttpResult> GetAsync(int accountId);
        public Task<HttpResult> PutAsync(PutAccountDto accountDto, int accountId);
        public Task<HttpResult> DeleteAsync(int accoundId);
        public Task<HttpResult> SignUpAsync(SignUpAccountDto accountDto);
        public Task<HttpResult> LogInAsync(LogInAccountDto accountDto);
    }
}
