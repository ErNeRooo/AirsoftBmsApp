using Android.Gms.Maps.Model;
using Android.Gms.Maps;
using Microsoft.Maui.Maps.Handlers;
using Android.Graphics;
using AirsoftBmsApp.View.ContentViews.CustomMap;
using Polygon = Microsoft.Maui.Controls.Maps.Polygon;
using APolygon = Android.Gms.Maps.Model.Polygon;
using Polyline = Microsoft.Maui.Controls.Maps.Polyline;
using APolyline = Android.Gms.Maps.Model.Polyline;
using Circle = Microsoft.Maui.Controls.Maps.Circle;
using ACircle = Android.Gms.Maps.Model.Circle;
using Microsoft.Maui.Maps;

namespace AirsoftBmsApp.Platforms.Android
{
    public class CustomMapHandler : MapHandler
    {
        private readonly Dictionary<string, BitmapDescriptor> _iconMap = [];

        public static new IPropertyMapper<CustomMap, CustomMapHandler> Mapper = new PropertyMapper<CustomMap, CustomMapHandler>(MapHandler.Mapper)
        {
            [nameof(CustomMap.CustomPins)] = MapPins,
            [nameof(CustomMap.BindableMapElements)] = MapElements
        };

        public Dictionary<string, (Marker Marker, CustomPin Pin)> MarkerMap { get; } = [];
        public Dictionary<string, (ACircle AndroidCircle, Circle MauiCircle)> CircleMap { get; } = [];
        public Dictionary<string, (APolygon AndroidPolygon, Polygon MauiPolygon)> PolygonMap { get; } = [];
        public Dictionary<string, (APolyline AndroidPolyline, Polyline MauiPolyline)> PolylineMap { get; } = [];

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

        private static new void MapElements(IMapHandler handler, Microsoft.Maui.Maps.IMap map)
        {
            if (handler.Map is null || handler.MauiContext is null) return;

            if (handler is CustomMapHandler mapHandler)
            {
                mapHandler.ClearMapElements(mapHandler);

                mapHandler.AddMapElements();
            }
        }

        private void AddMapElements()
        {
            if (VirtualView is CustomMap CustomMap)
            {
                foreach(var element in CustomMap.BindableMapElements)
                {
                    switch (element)
                    {
                        case Circle circle:
                            var aCircle = AddCircle(circle);
                            CircleMap.Add(aCircle.Id, (aCircle, circle));
                            break;
                        case Polygon polygon:
                            if (polygon.Geopath.Count < 1) break;
                            var aPolygon = AddPolygon(polygon);
                            PolygonMap.Add(aPolygon.Id, (aPolygon, polygon));
                            break;
                        case Polyline polyline:
                            if (polyline.Geopath.Count < 1) break;
                            var aPolyline = AddPolyline(polyline);
                            PolylineMap.Add(aPolyline.Id, (aPolyline, polyline));
                            break;
                    }
                }
            }
        }
        private ACircle AddCircle(Circle circle)
        {
            var circleOptions = new CircleOptions();
            circleOptions.InvokeCenter(new LatLng(circle.Center.Latitude, circle.Center.Longitude));
            circleOptions.InvokeRadius(circle.Radius.Meters);
            circleOptions.InvokeFillColor(circle.FillColor.ToInt());
            circleOptions.InvokeStrokeColor(circle.StrokeColor.ToInt());
            circleOptions.InvokeStrokeWidth(circle.StrokeWidth);
            var aCircle = Map.AddCircle(circleOptions);
            return aCircle;
        }

        private APolygon AddPolygon(Polygon polygon)
        {
            var polygonOptions = new PolygonOptions();
            foreach (var position in polygon.Geopath)
            {
                polygonOptions.Add(new LatLng(position.Latitude, position.Longitude));
            }
            polygonOptions.InvokeFillColor(polygon.FillColor.ToInt());
            polygonOptions.InvokeStrokeColor(polygon.StrokeColor.ToInt());
            polygonOptions.InvokeStrokeWidth(polygon.StrokeWidth);
            var aPolygon = Map.AddPolygon(polygonOptions);
            return aPolygon;
        }

        private APolyline AddPolyline(Polyline polyline)
        {
            var polylineOptions = new PolylineOptions();
            foreach (var position in polyline.Geopath)
            {
                polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));
            }
            polylineOptions.InvokeColor(polyline.StrokeColor.ToInt());
            polylineOptions.InvokeWidth(polyline.StrokeWidth);
            APolyline aPolyline = Map.AddPolyline(polylineOptions);
            return aPolyline;
        }

        private void ClearMapElements(CustomMapHandler mapHandler)
        {
            foreach (var circle in mapHandler.CircleMap)
            {
                circle.Value.AndroidCircle.Remove();
            }

            mapHandler.CircleMap.Clear();

            foreach (var polygon in mapHandler.PolygonMap)
            {
                polygon.Value.AndroidPolygon.Remove();
            }

            mapHandler.PolygonMap.Clear();

            foreach (var polyline in mapHandler.PolylineMap)
            {
                polyline.Value.AndroidPolyline.Remove();
            }

            mapHandler.PolylineMap.Clear();
        }

        private BitmapDescriptor GetIcon(string icon, int iconSizeInPixels)
        {
            if (_iconMap.TryGetValue(icon, out BitmapDescriptor? value))
            {
                return value;
            }

            var drawable = Context.Resources.GetIdentifier(icon, "drawable", Context.PackageName);
            var bitmap = BitmapFactory.DecodeResource(Context.Resources, drawable);
            var scaled = Bitmap.CreateScaledBitmap(bitmap, iconSizeInPixels, iconSizeInPixels, false);
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
                    markerOption.SetIcon(GetIcon(pin.IconSource, pin.IconSizeInPixels));
                    markerOption.SetPosition(new LatLng(pin.Location.Latitude, pin.Location.Longitude));
                    markerOption.Anchor(pin.HorizontalAnchor, pin.VerticalAnchor);
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
