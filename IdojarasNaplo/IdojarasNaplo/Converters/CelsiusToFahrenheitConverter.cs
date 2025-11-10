using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdojarasNaplo
{
	public class CelsiusToFahrenheitConverter : IMultiValueConverter
	{
		public object? Convert(object[] values, Type targetType, object? parameter, CultureInfo culture)
		{
			if (values[0] is double c && values[1] is bool showF)
			{
				return showF ? $"{(c * 9 / 5) + 32:F1} °F" : $"{c:F1} °C";
			}

			return null;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
