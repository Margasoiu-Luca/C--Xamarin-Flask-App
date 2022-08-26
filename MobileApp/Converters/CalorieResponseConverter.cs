using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.Converters
{
    internal class CalorieResponseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var number = (int)value;
            if (value == null || number == 0)
            {
                return "";
            }
            if (number < 1200)
            {
                return "You will lose weight dangerously fast!";
            }
            else if (number < 1600)
            {
                return "You will lose weight quickly";
            }
            else if (number < 1900)
            {
                return "You will lose weight slowly";
            }
            else if (number < 2100)
            {
                return "You will maintain your weight";
            }
            else if (number < 2500)
            {
                return "You will gain weight quickly";
            }
            else
            {
                return "You will gain weight at an alarming rate!";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
