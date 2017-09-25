using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A textbox meant to edit text.
	///     Displays a watermark while no text has been entered.
	/// </summary>
	public sealed class EditorTextBox
		: TextBox
	{
		/// <summary>
		///     Definition of the <see cref="Watermark" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty WatermarkProperty =
			DependencyProperty.Register("Watermark", typeof(string), typeof(EditorTextBox),
				new PropertyMetadata(default(string)));

		static EditorTextBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(EditorTextBox),
				new FrameworkPropertyMetadata(typeof(EditorTextBox)));
		}

		/// <summary>
		///     Initializes this text box.
		/// </summary>
		public EditorTextBox()
		{
			InputBindings.Add(new KeyBinding(new DelegateCommand(ToggleBoldness), new KeyGesture(Key.B, ModifierKeys.Control)));
		}

		/// <summary>
		///     The watermark text that shall appear until <see cref="TextBox.Text" /> is no longer empty.
		/// </summary>
		public string Watermark
		{
			get { return (string) GetValue(WatermarkProperty); }
			set { SetValue(WatermarkProperty, value); }
		}

		private void ToggleBoldness()
		{
			var n = 0;
		}
	}
}