using Android.App;
using Android.Content.Res;
using Android.Runtime;
using Microsoft.Maui.Handlers;

namespace AirsoftBmsApp
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
            EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
                handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            });

            PickerHandler.Mapper.AppendToMapping(nameof(Picker), (handler, view) =>
            {
                handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            });
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
