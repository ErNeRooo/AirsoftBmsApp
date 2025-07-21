using AirsoftBmsApp.Model.Dto.Player;
using AirsoftBmsApp.Model.Dto.Room;
using AirsoftBmsApp.Resources.Styles.TeamTheme;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AirsoftBmsApp.Model.Observable;

public partial class ObservableConfirmationDialogState : ObservableObject
{
    [ObservableProperty]
    public string message;

    [ObservableProperty]
    public ICommand? command;
}
