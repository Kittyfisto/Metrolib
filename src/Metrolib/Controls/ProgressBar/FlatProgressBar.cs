using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A progress bar in a "flat" style.
	///     Displays the relative value as 'X%' in the progress bar.
	/// </summary>
	[TemplatePart(Name = "PART_Progress", Type = typeof(FrameworkElement))]
	[TemplatePart(Name = "PART_ValuePositive", Type = typeof(FrameworkElement))]
	[TemplatePart(Name = "PART_ValueNegative", Type = typeof(FrameworkElement))]
	public class FlatProgressBar
		: AbstractProgressBar
	{
		/// <summary>
		///     Definition of the <see cref="IndeterminateValue" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IndeterminateValueProperty =
			DependencyProperty.Register("IndeterminateValue", typeof(double), typeof(FlatProgressBar),
			                            new PropertyMetadata(default(double), OnIndeterminateValueChanged));

		private FrameworkElement _progress;
		private FrameworkElement _valueNegative;
		private FrameworkElement _valuePositive;

		static FlatProgressBar()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatProgressBar),
			                                         new FrameworkPropertyMetadata(typeof(FlatProgressBar)));
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

		private static void OnIndeterminateValueChanged(DependencyObject dependencyObject,
		                                                DependencyPropertyChangedEventArgs args)
		{
			((FlatProgressBar) dependencyObject).OnIndeterminateValueChanged();
		}

		private void OnIndeterminateValueChanged()
		{
			UpdateClip();
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
				var width = ActualWidth;
				var height = ActualHeight;

				if (IsIndeterminate)
				{
					var relativeValue = IndeterminateValue;
					var knobwidth = width * 0.2;
					var relativeValueWidth = relativeValue * (width + knobwidth) - knobwidth;

					var clip = CreateGeometry(relativeValueWidth,
					                          relativeValueWidth + knobwidth,
					                          top: 0, bottom: height);

					_progress.Clip = clip;
				}
				else
				{
					var relativeValue = (Value - Minimum) / (Maximum - Minimum);
					var relativeValueWidth = relativeValue * width;

					var clipLeft = CreateGeometry(left: 0, right: relativeValueWidth, top: 0, bottom: height);
					var clipRight = CreateGeometry(relativeValueWidth, width, top: 0, bottom: height);

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
			using (var dc = geometry.Open())
			{
				dc.BeginFigure(new Point(left, top), isFilled: true, isClosed: true);
				dc.LineTo(new Point(right, top), isStroked: false, isSmoothJoin: false);
				dc.LineTo(new Point(right, bottom), isStroked: false, isSmoothJoin: false);
				dc.LineTo(new Point(left, bottom), isStroked: false, isSmoothJoin: false);
				dc.LineTo(new Point(left, top), isStroked: false, isSmoothJoin: false);
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