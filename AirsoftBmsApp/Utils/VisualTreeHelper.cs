using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Utils
{
    public static class VisualTreeHelper
    {
        public static T? FindParent<T>(Element? element) where T : Element
        {
            while (element != null)
            {
                if (element is T typedElement)
                    return typedElement;

                element = element.Parent;
            }

            return null;
        }
    }
}
