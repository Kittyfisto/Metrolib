using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A textbox that displays a watermark text if no text has been entered.
	/// </summary>
	public sealed class WatermarkTextBox
		: TextBox
	{
		public static readonly DependencyProperty WatermarkProperty =
			DependencyProperty.Register("Watermark", typeof (string), typeof (WatermarkTextBox),
			                            new PropertyMetadata(default(string)));

		static WatermarkTextBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (WatermarkTextBox),
			                                         new FrameworkPropertyMetadata(typeof (WatermarkTextBox)));
		}

		/// <summary>
		/// The watermark text that shall appear until <see cref="TextBox.Text"/> is no longer empty.
		/// </summary>
		public string Watermark
		{
			get { return (string) GetValue(WatermarkProperty); }
			set { SetValue(WatermarkProperty, value); }
		}
	}
}