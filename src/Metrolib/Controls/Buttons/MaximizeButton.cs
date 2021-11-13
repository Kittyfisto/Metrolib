using System.Windows;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A button to maximize something, for example a window.
	/// </summary>
	public class MaximizeButton
		: FlatButton
	{
		/// <summary>
		///     Definition of the <see cref="IsMaximized" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IsMaximizedProperty =
			DependencyProperty.Register("IsMaximized", typeof (bool), typeof (MaximizeButton),
			                            new PropertyMetadata(default(bool)));

		static MaximizeButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (MaximizeButton),
			                                         new FrameworkPropertyMetadata(typeof (MaximizeButton)));
		}

		/// <summary>
		///     Whether or not this button shall represent the maximized state.
		///     When set to true, the button will show the icon for restore, otherwise for maximize.
		/// </summary>
		public bool IsMaximized
		{
			get { return (bool) GetValue(IsMaximizedProperty); }
			set { SetValue(IsMaximizedProperty, value); }
		}
	}
}