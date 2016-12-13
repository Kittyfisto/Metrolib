// ReSharper disable CheckNamespace

using System.Windows.Media;

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// </summary>
	public interface IPieSlice
	{
		/// <summary>
		///     The pen used to draw the outline of the slice, if any.
		/// </summary>
		Pen Outline { get; }

		/// <summary>
		/// The brush used to fill the area of the slice, if any.
		/// </summary>
		Brush Fill { get; }

		/// <summary>
		///     The value of this slice.
		/// </summary>
		double Value { get; }

		/// <summary>
		///     The title of this slice, if any.
		/// </summary>
		object Title { get; }

		/// <summary>
		///     The tooltip of this slice, if any.
		/// </summary>
		object Tooltip { get; }
	}
}