// ReSharper disable CheckNamespace

using System.Windows;
using System.Windows.Controls;

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A "flat" expander:
	///     - no border
	///     - <see cref="ExpanderToggleButton" /> to expand content
	/// </summary>
	public class FlatExpander
		: Expander
	{
		/// <summary>
		///     Definition of the <see cref="HeaderIconHeight" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty HeaderIconHeightProperty =
			DependencyProperty.Register("HeaderIconHeight", typeof (double), typeof (FlatExpander),
			                            new PropertyMetadata(default(double)));

		static FlatExpander()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlatExpander), new FrameworkPropertyMetadata(typeof (FlatExpander)));
		}

		/// <summary>
		///     The height of the header's icon.
		/// </summary>
		public double HeaderIconHeight
		{
			get { return (double) GetValue(HeaderIconHeightProperty); }
			set { SetValue(HeaderIconHeightProperty, value); }
		}
	}
}