using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.Converters
{
    public class TextToColorConverter: IValueConverter
    {
        public TextToColorConverter()
        {

        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = (string)value;
            if(text == "" || text == null)
            {
                return null;
            }
            else if (text == "Account successfully created")
            {
                return Color.Green;
            }
            else
            {
                return Color.Red;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
