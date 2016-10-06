using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A "flat" <see cref="TextBlock" /> that supports the <see cref="Properties.IsInvertedProperty" />.
	/// </summary>
	public class FlatTextBlock
		: TextBlock
	{
		/// <summary>
		///     Definition of the <see cref="InvertedForeground" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty InvertedForegroundProperty =
			DependencyProperty.Register("InvertedForeground", typeof (Brush), typeof (FlatTextBlock),
			                            new PropertyMetadata(default(Brush)));

		static FlatTextBlock()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlatTextBlock),
			                                         new FrameworkPropertyMetadata(typeof (FlatTextBlock)));
		}

		/// <summary>
		///     The foreground of this textblock when the <see cref="Properties.IsInvertedProperty" /> is set to true.
		/// </summary>
		public Brush InvertedForeground
		{
			get { return (Brush) GetValue(InvertedForegroundProperty); }
			set { SetValue(InvertedForegroundProperty, value); }
		}
	}
}