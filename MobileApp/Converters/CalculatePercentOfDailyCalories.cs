using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.Converters
{
    internal class CalculatePercentOfDailyCalories : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Need to specify the system namespace because the Convert class  has conflict with the function in this namespace
            double temp = System.Convert.ToDouble(value);
            double percent = temp / 2000.0;
            percent *= 100;
             return $"{value} calories, which is about {System.Convert.ToInt16(percent)}% of the daily caloric intake";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
