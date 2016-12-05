using System;
using System.Globalization;
using System.Windows.Data;

namespace Metrolib.Controls.Charts.Network
{
	internal sealed class WidthHeightToSizeConverter
		: IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var width = (double)values[0];
			var height = (double)values[1];
			var edge = Math.Max(width, height);
			var diameter = Math.Sqrt(edge * edge + edge * edge);
			return diameter;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}