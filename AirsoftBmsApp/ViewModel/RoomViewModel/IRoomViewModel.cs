﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirsoftBmsApp.Model;

namespace AirsoftBmsApp.ViewModel.RoomViewModel
{
    public interface IRoomViewModel
    {
        ObservableRoom Room { get; set; }
        public Task LeaveRoom();
    }
}
