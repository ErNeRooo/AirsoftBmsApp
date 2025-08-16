using Android.Gms.Maps.Model;
using Android.Gms.Maps;
using Microsoft.Maui.Maps.Handlers;
using Android.Graphics;
using AirsoftBmsApp.View.ContentViews.CustomMap;

namespace AirsoftBmsApp.Platforms.Android
{
    public class CustomMapHandler : MapHandler
    {
        private const int _iconSize = 40;

        private readonly Dictionary<string, BitmapDescriptor> _iconMap = [];

        public static new IPropertyMapper<CustomMap, CustomMapHandler> Mapper = new PropertyMapper<CustomMap, CustomMapHandler>(MapHandler.Mapper)
        {
            [nameof(CustomMap.CustomPins)] = MapPins
        };

        public Dictionary<string, (Marker Marker, CustomPin Pin)> MarkerMap { get; } = [];

        public CustomMapHandler()
            : base(Mapper)
        {
        }

        protected override void ConnectHandler(MapView platformView)
        {
            base.ConnectHandler(platformView);
            var mapReady = new MapCallbackHandler(this);

            PlatformView.GetMapAsync(mapReady);
        }

        private static new void MapPins(IMapHandler handler, Microsoft.Maui.Maps.IMap map)
        {
            if (handler.Map is null || handler.MauiContext is null)
            {
                return;
            }

            if (handler is CustomMapHandler mapHandler)
            {
                foreach (var marker in mapHandler.MarkerMap)
                {
                    marker.Value.Marker.Remove();
                }

                mapHandler.MarkerMap.Clear();

                mapHandler.AddPins();
            }
        }

        private BitmapDescriptor GetIcon(string icon)
        {
            if (_iconMap.TryGetValue(icon, out BitmapDescriptor? value))
            {
                return value;
            }

            var drawable = Context.Resources.GetIdentifier(icon, "drawable", Context.PackageName);
            var bitmap = BitmapFactory.DecodeResource(Context.Resources, drawable);
            var scaled = Bitmap.CreateScaledBitmap(bitmap, _iconSize, _iconSize, false);
            bitmap.Recycle();
            var descriptor = BitmapDescriptorFactory.FromBitmap(scaled);

            _iconMap[icon] = descriptor;

            return descriptor;
        }

        private void AddPins()
        {
            if (VirtualView is CustomMap CustomMap && CustomMap.CustomPins != null)
            {
                foreach (var pin in CustomMap.CustomPins)
                {
                    var markerOption = new MarkerOptions();
                    markerOption.SetTitle(pin.Label);
                    markerOption.SetIcon(GetIcon(pin.IconSource));
                    markerOption.SetPosition(new LatLng(pin.Location.Latitude, pin.Location.Longitude));
                    markerOption.Anchor(0.5f, 0.5f);
                    var marker = Map.AddMarker(markerOption);

                    MarkerMap.Add(marker.Id, (marker, pin));
                }
            }
        }



        public void MarkerClick(object sender, GoogleMap.MarkerClickEventArgs args)
        {
            if (MarkerMap.TryGetValue(args.Marker.Id, out (Marker Marker, CustomPin Pin) value))
            {
                value.Pin.ClickedCommand?.Execute(value.Pin);
            }

            args.Marker.ShowInfoWindow();
        }
    }

    public class MapCallbackHandler : Java.Lang.Object, IOnMapReadyCallback
    {
        private readonly CustomMapHandler mapHandler;

        public MapCallbackHandler(CustomMapHandler mapHandler)
        {
            this.mapHandler = mapHandler;
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            mapHandler.UpdateValue(nameof(CustomMap.CustomPins));
            googleMap.MarkerClick += mapHandler.MarkerClick;
        }
    }
}
