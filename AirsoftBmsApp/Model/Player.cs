using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model
{
    public partial class Player : ObservableObject
    {
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private string _name;
        
        [ObservableProperty]
        private Account? _account;

        [ObservableProperty]
        private int _roomId;
    }
}
