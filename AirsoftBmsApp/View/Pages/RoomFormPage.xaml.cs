using AirsoftBmsApp.ViewModel.RoomFormPageViewModel;

namespace AirsoftBmsApp.View.Pages;

public partial class RoomFormPage : ContentPage
{
    public RoomFormPage(IRoomFormViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
    }
}

