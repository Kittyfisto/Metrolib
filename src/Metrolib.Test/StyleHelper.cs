using System;
using System.Windows;
using FluentAssertions;

namespace Metrolib.Test
{
	public static class StyleHelper
	{
		public static Style Load<T>()
		{
			var path = new Uri("/Metrolib;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute);
			var dictionary = new ResourceDictionary
				{
					Source = path
				};
			var style = dictionary[typeof (T)];
			style.Should().BeOfType<Style>();
			return (Style) style;
		}
	}
}