using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A progress bar in a "flat" style.
	///     Displays the relative value as 'X%' in the progress bar.
	/// </summary>
	[TemplatePart(Name = "PART_Progress", Type = typeof (FrameworkElement))]
	[TemplatePart(Name = "PART_ValuePositive", Type = typeof (FrameworkElement))]
	[TemplatePart(Name = "PART_ValueNegative", Type = typeof (FrameworkElement))]
	public class FlatProgressBar
		: ProgressBar
	{
		private FrameworkElement _progress;
		private FrameworkElement _valueNegative;
		private FrameworkElement _valuePositive;

		static FlatProgressBar()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlatProgressBar),
			                                         new FrameworkPropertyMetadata(typeof (FlatProgressBar)));
		}

		/// <summary>
		/// Initializes this object.
		/// </summary>
		public FlatProgressBar()
		{
			ValueChanged += OnValueChanged;
			SizeChanged += OnSizeChanged;
		}

		private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
		{
			UpdateClip();
		}

		private void OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> args)
		{
			UpdateClip();
		}

		private void UpdateClip()
		{
			if (_progress != null && _valuePositive != null && _valueNegative != null)
			{
				double width = ActualWidth;
				double height = ActualHeight;
				double relativeValue = (Value - Minimum)/(Maximum - Minimum);
				double relativeValueWidth = relativeValue*width;

				var clipLeft = new StreamGeometry();
				using (StreamGeometryContext dc = clipLeft.Open())
				{
					dc.BeginFigure(new Point(0, 0), true, true);
					dc.LineTo(new Point(relativeValueWidth, 0), false, false);
					dc.LineTo(new Point(relativeValueWidth, height), false, false);
					dc.LineTo(new Point(0, height), false, false);
					dc.LineTo(new Point(0, 0), false, false);
				}

				_progress.Clip = clipLeft;
				_valuePositive.Clip = clipLeft;

				var clipRight = new StreamGeometry();
				using (StreamGeometryContext dc = clipRight.Open())
				{
					dc.BeginFigure(new Point(width, 0), true, true);
					dc.LineTo(new Point(width, height), false, false);
					dc.LineTo(new Point(relativeValueWidth, height), false, false);
					dc.LineTo(new Point(relativeValueWidth, 0), false, false);
					dc.BeginFigure(new Point(width, 0), true, true);
				}

				_valueNegative.Clip = clipRight;
			}
		}

		/// <summary>
		///     Called when the template's tree is generated.
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_progress = (FrameworkElement) GetTemplateChild("PART_Progress");
			_valuePositive = (FrameworkElement) GetTemplateChild("PART_ValuePositive");
			_valueNegative = (FrameworkElement) GetTemplateChild("PART_ValueNegative");

			UpdateClip();
		}
	}
}