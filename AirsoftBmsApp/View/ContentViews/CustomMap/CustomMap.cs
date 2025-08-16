using Microsoft.Maui.Controls.Maps;

namespace AirsoftBmsApp.View.ContentViews.CustomMap;

public class CustomMap : Microsoft.Maui.Controls.Maps.Map
{
    public static readonly BindableProperty CustomPinsProperty =
        BindableProperty.Create(nameof(CustomPins), typeof(List<CustomPin>), typeof(CustomMap), null);

    public static readonly BindableProperty BindableMapElementsProperty =
        BindableProperty.Create(
            nameof(BindableMapElements),
            typeof(IEnumerable<MapElement>),
            typeof(CustomMap),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (bindable is CustomMap map && newValue is IEnumerable<MapElement> elements)
                {
                    map.MapElements.Clear();
                    foreach (var e in elements)
                        map.MapElements.Add(e);
                }
            });

    public List<CustomPin> CustomPins
    {
        get => (List<CustomPin>)GetValue(CustomPinsProperty);
        set => SetValue(CustomPinsProperty, value);
    }

    public IEnumerable<MapElement> BindableMapElements
    {
        get => (IEnumerable<MapElement>)GetValue(BindableMapElementsProperty);
        set => SetValue(BindableMapElementsProperty, value);
    }
}
