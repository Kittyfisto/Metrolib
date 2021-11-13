using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A textblock that displays "flavour" text to tell a user what to do next.
	///     Can be used to indicate a drop area (when nothing has been dropped yet).
	/// </summary>
	public class FlavourTextBlock : Control
	{
		/// <summary>
		///     Definition of the <see cref="Text" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register("Text", typeof (string), typeof (FlavourTextBlock), new PropertyMetadata(default(string)));

		/// <summary>
		///     Definition of the <see cref="BorderRadius" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty BorderRadiusProperty =
			DependencyProperty.Register("BorderRadius", typeof (double), typeof (FlavourTextBlock),
			                            new PropertyMetadata(default(double)));

		static FlavourTextBlock()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlavourTextBlock),
			                                         new FrameworkPropertyMetadata(typeof (FlavourTextBlock)));
		}

		/// <summary>
		///     The radius of the border around the text.
		/// </summary>
		public double BorderRadius
		{
			get { return (double) GetValue(BorderRadiusProperty); }
			set { SetValue(BorderRadiusProperty, value); }
		}

		/// <summary>
		///     The text that shall be displayed.
		/// </summary>
		public string Text
		{
			get { return (string) GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}
	}
}