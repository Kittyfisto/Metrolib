﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

// ReSharper disable CheckNamespace

namespace Metrolib.Converters
// ReSharper restore CheckNamespace
{
	public sealed class BoolFalseToHiddenConverter
		: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType != typeof (Visibility))
				return null;

			if (!(value is bool))
				return false;

			var val = (bool) value;
			if (!val)
			{
				return Visibility.Hidden;
			}

			return Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}