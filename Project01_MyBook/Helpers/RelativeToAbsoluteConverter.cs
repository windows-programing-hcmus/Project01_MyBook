using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Project01_MyBook.Helpers
{

    internal class RelativeToAbsoluteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String relativePath = (String)value;
            String folder = AppDomain.CurrentDomain.BaseDirectory;
            String absolutePath = $"{folder}/{relativePath}";

            Debug.WriteLine(absolutePath);
            return absolutePath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
