using AirsoftBmsApp.Networking.ApiFacade.Handlers.Account;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Battle;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Death;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Kill;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Location;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Player;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Networking.ApiFacade
{
    public interface IApiFacade
    {
        public IRoomHandler Room { get; }
        public IPlayerHandler Player { get; }
        public ITeamHandler Team { get; }
        public IAccountHandler Account { get; }
        public IBattleHandler Battle { get; }
        public IKillHandler Kill { get; }
        public IDeathHandler Death { get; }
        public ILocationHandler Location { get; }
    }
}
