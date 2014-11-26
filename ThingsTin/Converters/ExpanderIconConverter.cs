using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ThingsTin.Converters
{
    public class ExpanderIconConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isExpanded = (bool)value;
            if (isExpanded)
            {
                return "/ThingsTin;Component/Images/outline.png";
            }
            else
            {
                return "/ThingsTin;Component/Images/grid.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
