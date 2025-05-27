using AirsoftBmsApp.Model.Dto.Account;
using AirsoftBmsApp.Networking;

namespace AirsoftBmsApp.Services.AccountRestService.Abstractions
{
    public interface IAccountRestService
    {
        Task<HttpResult> TryRequest(AccountRequestIntent accountRequest);
    }
}
