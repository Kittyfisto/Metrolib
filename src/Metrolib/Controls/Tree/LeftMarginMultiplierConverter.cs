using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	internal sealed class LeftMarginMultiplierConverter
		: IValueConverter
	{
		public double Multiplier { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var item = value as FlatTreeViewItem;
			if (item == null)
				return null;

			int depth = item.Depth;
			double margin = depth*Multiplier;
			return new Thickness(margin, 0, 0, 0);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}