using System.Windows;
using System.Windows.Controls;

namespace Metrolib.Controls
{
	public class FlavourTextBlock : Control
	{
		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register("Text", typeof (string), typeof (FlavourTextBlock), new PropertyMetadata(default(string)));

		public static readonly DependencyProperty BorderRadiusProperty =
			DependencyProperty.Register("BorderRadius", typeof (double), typeof (FlavourTextBlock), new PropertyMetadata(default(double)));

		public double BorderRadius
		{
			get { return (double) GetValue(BorderRadiusProperty); }
			set { SetValue(BorderRadiusProperty, value); }
		}

		public string Text
		{
			get { return (string) GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		static FlavourTextBlock()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FlavourTextBlock), new FrameworkPropertyMetadata(typeof(FlavourTextBlock)));
		}
	}
}
