using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// </summary>
	public interface ILineSeries
		: INotifyPropertyChanged
	{
		/// <summary>
		/// </summary>
		int Count { get; }

		/// <summary>
		/// </summary>
		Pen Outline { get; set; }

		/// <summary>
		/// </summary>
		Brush Fill { get; set; }

		/// <summary>
		/// </summary>
		IEnumerable<Point> Values { get; set; }

		/// <summary>
		/// </summary>
		Range XRange { get; }

		/// <summary>
		/// </summary>
		Range YRange { get; }

		/// <summary>
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		object GetXValue(double value);

		/// <summary>
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		object GetYValue(double value);
	}
}