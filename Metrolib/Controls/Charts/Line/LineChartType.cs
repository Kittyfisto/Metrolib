// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Lists various "types" one or more <see cref="ILineSeries" /> can be displayed.
	/// </summary>
	public enum LineChartType
	{
		/// <summary>
		///     One line per <see cref="ILineSeries" />.
		///     The area under the line may be filled out, but a translucient fill brush is
		///     recommended in case more than one series is displayed.
		/// </summary>
		Normal,

		/// <summary>
		///     One line per <see cref="ILineSeries" />, however each series sits on top of
		///     the previous one. When this is used, <see cref="ILineSeries.Fill" /> should
		///     be set to the user understands this fact.
		/// </summary>
		Stacked,
	}
}