﻿using AirsoftBmsApp.Model.Observable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Utils
{
    public static class TeamsCollectionHelper
    {
        public static ObservablePlayer? FindPlayerWithId(this ObservableCollection<ObservableTeam> teams, int id)
        {
            foreach(var team in teams)
            {
                ObservablePlayer? player = team.Players.FirstOrDefault(player => player.Id == id);

                if (player is not null) return player;
            }

            return null;
        }

        public static void ReplacePlayerWithId(this ObservableCollection<ObservableTeam> teams, int id, ObservablePlayer newPlayer)
        {
            foreach (var team in teams)
            {
                ObservablePlayer? player = team.Players.FirstOrDefault(player => player.Id == id);

                if (player is not null) 
                {
                    team.Players.Remove(player);
                    team.Players.Add(newPlayer);
                }
            }
        }
    }
}
