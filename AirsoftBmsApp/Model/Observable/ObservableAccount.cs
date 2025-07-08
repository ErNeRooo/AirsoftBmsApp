using CommunityToolkit.Mvvm.ComponentModel;


namespace AirsoftBmsApp.Model.Observable;

public partial class ObservableAccount : ObservableObject
{
    [ObservableProperty]
    private int accountId;

    [ObservableProperty]
    private int playerId;

    [ObservableProperty]
    private string email;
}

