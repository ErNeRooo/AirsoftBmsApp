using AirsoftBmsApp.Networking.ApiFacade.Handlers.Account;
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
    }
}
