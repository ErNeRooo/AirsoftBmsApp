using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Model.Observable;

public partial class ObservablePlayer : ObservableObject
{
    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private string name;
        
    [ObservableProperty]
    private ObservableAccount? account;

    [ObservableProperty]
    private int roomId;
}

