using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ThingsTin.Utils
{
    public class ResourcesLoader
    {
        public static ResourceDictionary LoadResource()
        {
            var resourceDictionary = Application.LoadComponent(
                              new Uri(
                                  "/ThingsTin;component/Views/Resource.xaml",
                                  UriKind.Relative)) as ResourceDictionary;

            return resourceDictionary;
        }

        public static object LoadResource(string resourceKey)
        {
            var resourceDictionary = LoadResource();
            if(resourceDictionary.Contains(resourceKey))
            {
                return resourceDictionary[resourceKey];
            }

            return null;
        }
    }
}
