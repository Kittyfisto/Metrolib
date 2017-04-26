using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace
namespace Metrolib
	// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A chart displaying the density of a certain value over time.
	/// </summary>
	public class DateTimeHistogram
		: Control
	{
		static DateTimeHistogram()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(DateTimeHistogram), new FrameworkPropertyMetadata(typeof(DateTimeHistogram)));
		}
	}
}
