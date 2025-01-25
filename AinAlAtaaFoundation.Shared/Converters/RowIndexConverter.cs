using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace AinAlAtaaFoundation.Shared.Converters
{
    public class RowIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataGridRow row = (DataGridRow)value;
            return row.GetIndex();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}