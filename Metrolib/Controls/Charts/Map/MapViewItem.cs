using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Control representing an item in the map.
	/// </summary>
	public abstract class MapViewItem
		: ContentControl
	{
		/// <summary>
		///     Definition of the <see cref="IsSelected" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IsSelectedProperty =
			DependencyProperty.Register("IsSelected", typeof (bool), typeof (MapViewItem), new PropertyMetadata(default(bool)));

		/// <summary>
		///     Whether or not this item is selected.
		/// </summary>
		public bool IsSelected
		{
			get { return (bool) GetValue(IsSelectedProperty); }
			set { SetValue(IsSelectedProperty, value); }
		}
	}
}