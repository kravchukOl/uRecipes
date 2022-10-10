using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uRecipes.Views.Converters
{
    public class SpecCharConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float num = (float)value;

            if (num == 0f)
                return "";

            if (num == 0.25f)
                return "¼";

            if (num == 0.5f)
                return "½";

            if (num == 0.75f)
                return "¾";

            if (num == (1 / 3))
                return "⅓";

            if (num == (2 / 3))
                return "⅔";

            return num.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
