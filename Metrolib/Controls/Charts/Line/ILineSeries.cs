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
		///     The amount of points in <see cref="Values" />.
		/// </summary>
		int Count { get; }

		/// <summary>
		///     The pen to draw the outline of this series with, if any.
		/// </summary>
		Pen Outline { get; }

		/// <summary>
		///     The brush to fill the area under this series, if any.
		/// </summary>
		Brush Fill { get; }

		/// <summary>
		///     The pen to draw the outline of the shape, representing individual data points.
		/// </summary>
		Pen PointOutline { get; }

		/// <summary>
		///     The brush to fill the shape, representing individual data points.
		/// </summary>
		Brush PointFill { get; }

		/// <summary>
		///     The radius to draw the circle, representing individual data points.
		/// </summary>
		double PointRadius { get; }

		/// <summary>
		///     The values to display.
		/// </summary>
		/// <remarks>
		///     Values are assumed to be ordered ascending by their <see cref="Point.X" /> value.
		///     If this is not the case then the wrong data might be displayed.
		/// </remarks>
		IEnumerable<Point> Values { get; }

		/// <summary>
		///     The minimum and maximum x values in <see cref="Values" />.
		/// </summary>
		Range XRange { get; }

		/// <summary>
		///     The minimum and maximum y values in <see cref="Values" />.
		/// </summary>
		Range YRange { get; }

		/// <summary>
		///     Returns the value that should be displayed instead of the given numerical value.
		///     Is used to annotate axes and popups / tooltips.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		object GetXValue(double value);

		/// <summary>
		///     Returns the value that should be displayed instead of the given numerical value.
		///     Is used to annotate axes and popups / tooltips.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		object GetYValue(double value);
	}
}