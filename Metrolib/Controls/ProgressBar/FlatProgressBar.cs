using System.Diagnostics.Contracts;
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
		/// <summary>
		///     Definition of the <see cref="IndeterminateValue" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IndeterminateValueProperty =
			DependencyProperty.Register("IndeterminateValue", typeof (double), typeof (FlatProgressBar),
			                            new PropertyMetadata(default(double)));

		private FrameworkElement _progress;
		private FrameworkElement _valueNegative;
		private FrameworkElement _valuePositive;

		static FlatProgressBar()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlatProgressBar),
			                                         new FrameworkPropertyMetadata(typeof (FlatProgressBar)));
		}

		/// <summary>
		///     Initializes this object.
		/// </summary>
		public FlatProgressBar()
		{
			ValueChanged += OnValueChanged;
			SizeChanged += OnSizeChanged;
		}

		/// <summary>
		///     The relative value used in favour of <see cref="ProgressBar.Value" /> when this one is
		///     <see cref="ProgressBar.IsIndeterminate" />.
		/// </summary>
		public double IndeterminateValue
		{
			get { return (double) GetValue(IndeterminateValueProperty); }
			set { SetValue(IndeterminateValueProperty, value); }
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

				if (IsIndeterminate)
				{
					double relativeValue = IndeterminateValue;
					double knobwidth = width * 0.2;
					double relativeValueWidth = relativeValue*(width + knobwidth) - knobwidth;

					var clip = CreateGeometry(relativeValueWidth,
					                          relativeValueWidth + knobwidth,
					                          0, height);

					_progress.Clip = clip;
				}
				else
				{
					double relativeValue = (Value - Minimum) / (Maximum - Minimum);
					double relativeValueWidth = relativeValue * width;

					var clipLeft = CreateGeometry(0, relativeValueWidth, 0, height);
					var clipRight = CreateGeometry(relativeValueWidth, width, 0, height);

					_progress.Clip = clipLeft;
					_valuePositive.Clip = clipLeft;

					_valueNegative.Clip = clipRight;
				}

			}
		}

		[Pure]
		private static StreamGeometry CreateGeometry(double left, double right, double top, double bottom)
		{
			var geometry = new StreamGeometry();
			using (StreamGeometryContext dc = geometry.Open())
			{
				dc.BeginFigure(new Point(left, top), true, true);
				dc.LineTo(new Point(right, top), false, false);
				dc.LineTo(new Point(right, bottom), false, false);
				dc.LineTo(new Point(left, bottom), false, false);
				dc.LineTo(new Point(left, top), false, false);
			}
			return geometry;
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