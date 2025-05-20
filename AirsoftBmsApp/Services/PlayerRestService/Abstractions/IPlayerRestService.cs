namespace AirsoftBmsApp.Services.PlayerRestService.Abstractions
{
    public interface IPlayerRestService
    {
        Task<bool> RegisterPlayerAsync(string name);
        Task<bool> LogInToAccountAsync(string email, string password);
        Task<bool> SignUpAccountAsync(string name, string email, string password);
    }
}
