using System.Windows;
using System.Windows.Controls;

// ReSharper disable once CheckNamespace

namespace Metrolib.Controls
{
	/// <summary>
	///     A button that can be used to refresh things.
	/// </summary>
	/// <remarks>
	///     Shows a circular progress indicator while being refreshed.
	/// </remarks>
	public class RefreshButton : FlatButton
	{
		/// <summary>
		///     Definition of the <see cref="IsRefreshing" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IsRefreshingProperty = DependencyProperty.Register(
			"IsRefreshing", typeof(bool), typeof(RefreshButton), new PropertyMetadata(default(bool)));

		static RefreshButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(RefreshButton), new FrameworkPropertyMetadata(typeof(RefreshButton)));
		}

		/// <summary>
		///     When set to true, the button will show a <see cref="CircularProgressBar" /> with
		///     <see cref="ProgressBar.IsIndeterminate" />
		///     set to true.
		/// </summary>
		public bool IsRefreshing
		{
			get { return (bool) GetValue(IsRefreshingProperty); }
			set { SetValue(IsRefreshingProperty, value); }
		}
	}
}