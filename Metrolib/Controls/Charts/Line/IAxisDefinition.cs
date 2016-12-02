using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;

// ReSharper disable CheckNamespace
namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Describes the x- or y-axis of a chart.
	/// </summary>
	public interface IAxisDefinition
		: INotifyPropertyChanged
	{
		/// <summary>
		///     The pen used to draw lines over the diagram.
		/// </summary>
		/// <remarks>
		///     Lines are only drawn when there is enough space (<see cref="Spacing" />) and when
		///     <see cref="ShowLines" /> is true.
		/// </remarks>
		Pen LinePen { get; }

		/// <summary>
		///     The spacing between ticks / lines in device independent units.
		/// </summary>
		double Spacing { get; }

		/// <summary>
		///     The caption next to the axis.
		///     Will be presented by a <see cref="ContentPresenter" />.
		/// </summary>
		/// <remarks>
		///     The content of the y-axis is rotated by 90° counter clockwise.
		/// </remarks>
		object Caption { get; }

		/// <summary>
		/// </summary>
		bool ShowTicks { get; }

		/// <summary>
		/// </summary>
		bool ShowLines { get; }
	}
}