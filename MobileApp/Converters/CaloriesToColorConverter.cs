using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.Converters
{
    internal class CaloriesToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var number = (int)value;   
            if (number < 1200)
            {
                return Color.Red;
            }
            else if(number < 1600)
            {
                return Color.Green;
            }
            else if(number < 1900){
                return Color.LightGreen;
            }
            else if (number < 2100)
            {
                return Color.Peru;
            }
            else if (number < 2500)
            {
                return Color.Red;
            }
            else
            {
                return Color.DarkRed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
