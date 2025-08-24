using AirsoftBmsApp.Networking.ApiFacade.Handlers.Account;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Battle;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Death;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Kill;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Location;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Player;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Room;

namespace AirsoftBmsApp.Networking.ApiFacade
{
    public class ApiFacade(
        IRoomHandler roomHandler,
        IPlayerHandler playerHandler,
        ITeamHandler teamHandler,
        IAccountHandler accountHandler,
        IBattleHandler battleHandler,
        IKillHandler killHandler,
        IDeathHandler deathHandler,
        ILocationHandler locationHandler
        ) : IApiFacade
    {
        public IRoomHandler Room { get; } = roomHandler;
        public IPlayerHandler Player { get; } = playerHandler;
        public ITeamHandler Team { get; } = teamHandler;
        public IAccountHandler Account { get; } = accountHandler;
        public IBattleHandler Battle { get; } = battleHandler;
        public IKillHandler Kill { get; } = killHandler;
        public IDeathHandler Death { get; } = deathHandler;
        public ILocationHandler Location { get; } = locationHandler;
    }
}
