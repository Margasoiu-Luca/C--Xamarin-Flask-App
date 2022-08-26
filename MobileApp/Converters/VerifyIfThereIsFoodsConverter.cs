using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.Converters
{
    internal class VerifyIfThereIsFoodsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var number = (int)value;
            if (value == null|| number == 0)
            {
                return "You have no foods in your diet!";
            }
            return number;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
