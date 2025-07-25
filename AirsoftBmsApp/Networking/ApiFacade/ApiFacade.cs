﻿using AirsoftBmsApp.Networking.ApiFacade.Handlers.Account;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Player;
using AirsoftBmsApp.Networking.ApiFacade.Handlers.Room;

namespace AirsoftBmsApp.Networking.ApiFacade
{
    public class ApiFacade(
        IRoomHandler roomHandler,
        IPlayerHandler playerHandler,
        ITeamHandler teamHandler,
        IAccountHandler accountHandler
        ) : IApiFacade
    {
        public IRoomHandler Room { get; } = roomHandler;
        public IPlayerHandler Player { get; } = playerHandler;
        public ITeamHandler Team { get; } = teamHandler;
        public IAccountHandler Account { get; } = accountHandler;
    }
}
