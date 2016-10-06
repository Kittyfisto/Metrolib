using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// </summary>
	public class FlatMenuItem
		: MenuItem
	{
		private FlatButton _button;

		static FlatMenuItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlatMenuItem), new FrameworkPropertyMetadata(typeof (FlatMenuItem)));
		}

		/// <summary>
		///     Called when the template's tree is generated.
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_button = (FlatButton) GetTemplateChild("PART_Button");
			_button.Click += ButtonOnClick;
		}

		private void ButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			// For some reason, the context menu doesn't close automatically
			var contextMenu = this.FindFirstAncestorOfType<ContextMenu>();
			if (contextMenu != null)
			{
				contextMenu.IsOpen = false;
			}
		}
	}
}