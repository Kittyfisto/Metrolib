// ReSharper disable CheckNamespace

using System.Windows.Media;

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Represents a slice of a pie chart.
	/// </summary>
	public interface IPieSlice
	{
		/// <summary>
		///     The pen used to draw the outline of the slice, if any.
		/// </summary>
		Pen Outline { get; }

		/// <summary>
		///     The brush used to fill the area of the slice, if any.
		/// </summary>
		Brush Fill { get; }

		/// <summary>
		///     The value of this slice.
		/// </summary>
		/// <remarks>
		///     Is used to determine the arc-length of the circle segment that represents this slice.
		/// </remarks>
		double Value { get; }

		/// <summary>
		///     The value of this slice, as it shall be displayed.
		/// </summary>
		/// <remarks>
		///     Is placed inside the rendered circle segment.
		/// </remarks>
		object DisplayedValue { get; }

		/// <summary>
		///     The label of this slice, if any.
		/// </summary>
		/// <remarks>
		///     Is placed adjacent to the rendered circle segment.
		/// </remarks>
		object Label { get; }

		/// <summary>
		///     The tooltip of this slice, if any.
		/// </summary>
		/// <remarks>
		///     Is displayed when the mouse hovers over a circle segment.
		/// </remarks>
		object Tooltip { get; }
	}
}