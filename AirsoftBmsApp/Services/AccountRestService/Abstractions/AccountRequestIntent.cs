using AirsoftBmsApp.Model.Dto.Account;

namespace AirsoftBmsApp.Services.AccountRestService.Abstractions
{
    public abstract record AccountRequestIntent;

    public record GetAccountByIdAsync(int accountId) : AccountRequestIntent;
    public record PutAccountAsync(PutAccountDto accountDto, int accountId) : AccountRequestIntent;
    public record SignUpAccountAsync(SignUpAccountDto accountDto) : AccountRequestIntent;
    public record LogInAccountAsync(LogInAccountDto accountDto) : AccountRequestIntent;
    public record DeleteAccountAsync(int accountId) : AccountRequestIntent;
}
