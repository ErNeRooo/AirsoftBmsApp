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
    public partial class ObservableAccount : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string name;
    }
}
