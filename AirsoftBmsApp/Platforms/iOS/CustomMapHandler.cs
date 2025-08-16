using AirsoftBmsApp.View.ContentViews.CustomMap;
using CoreLocation;
using MapKit;
using Microsoft.Maui.Maps.Handlers;
using Microsoft.Maui.Maps.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace AirsoftBmsApp.Platforms.iOS
{
    public class CustomMapHandler : MapHandler
    {
        private readonly Dictionary<string, UIImage> _iconMap = [];

        public static new IPropertyMapper<CustomMap, CustomMapHandler> Mapper = new PropertyMapper<CustomMap, CustomMapHandler>(MapHandler.Mapper)
        {
            [nameof(CustomMap.CustomPins)] = CustomPins
        };

        public Dictionary<IMKAnnotation, CustomPin> MarkerMap { get; } = [];

        public CustomMapHandler()
            : base(Mapper)
        {
        }

        protected override void ConnectHandler(MauiMKMapView platformView)
        {
            base.ConnectHandler(platformView);

            platformView.DidSelectAnnotationView += CustomMapHandler_DidSelectAnnotationView;
            platformView.GetViewForAnnotation += GetViewForAnnotation;
        }

        protected override void DisconnectHandler(MauiMKMapView platformView)
        {
            base.DisconnectHandler(platformView);

            platformView.DidSelectAnnotationView -= CustomMapHandler_DidSelectAnnotationView;
            platformView.GetViewForAnnotation -= GetViewForAnnotation;
        }

        private void CustomMapHandler_DidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            if (MarkerMap.TryGetValue(e.View.Annotation, out CustomPin value))
            {
                value.ClickedCommand?.Execute(null);
                PlatformView.DeselectAnnotation(e.View.Annotation, false);
            }
        }

        private static new void CustomPins(IMapHandler handler, Microsoft.Maui.Maps.IMap map)
        {
            if (handler is CustomMapHandler mapHandler && handler.VirtualView is CustomMap CustomMap)
            {
                handler.PlatformView.RemoveAnnotations(mapHandler.MarkerMap.Select(x => x.Key).ToArray());

                mapHandler.MarkerMap.Clear();

                mapHandler.AddPins();
            }
        }

        private void AddPins()
        {
            if (VirtualView is CustomMap CustomMap)
            {
                foreach (var pin in CustomMap.CustomPins)
                {
                    var marker = new MKPointAnnotation(new CLLocationCoordinate2D(pin.Location.Latitude, pin.Location.Longitude));

                    PlatformView.AddAnnotation(marker);

                    MarkerMap.Add(marker, pin);
                }
            }
        }

        private MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            if (annotation == null || annotation is MKUserLocation)
            {
                return null;
            }

            var customPin = GetCustomPin(annotation);

            if (customPin == null)
            {
                return null;
            }

            return mapView.DequeueReusableAnnotation(customPin.Id.ToString()) ?? new MKAnnotationView
            {
                Image = GetIcon(customPin.IconSource),
                CanShowCallout = false
            };
        }

        private CustomPin GetCustomPin(IMKAnnotation mkPointAnnotation)
        {
            if (MarkerMap.TryGetValue(mkPointAnnotation, out CustomPin value))
            {
                return value;
            }

            return null;
        }

        private UIImage GetIcon(string icon)
        {
            if (_iconMap.TryGetValue(icon, out UIImage? value))
            {
                return value;
            }

            var image = UIImage.FromBundle(icon);

            _iconMap[icon] = image;

            return image;
        }
    }
}
