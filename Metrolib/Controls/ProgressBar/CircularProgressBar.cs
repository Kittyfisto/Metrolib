using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Metrolib.Geometry;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A circular progress bar (full circle).
	/// </summary>
	[TemplatePart(Name = "PART_Indicator", Type = typeof (Ellipse))]
	public class CircularProgressBar
		: AbstractProgressBar
	{
		/// <summary>
		///     Definition of the <see cref="Thickness" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ThicknessProperty =
			DependencyProperty.Register("Thickness", typeof (double), typeof (CircularProgressBar),
			                            new PropertyMetadata(default(double)));

		/// <summary>
		///     Definition of the <see cref="Content" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ContentProperty =
			DependencyProperty.Register("Content", typeof (object), typeof (CircularProgressBar),
			                            new PropertyMetadata(default(object)));

		/// <summary>
		///     Definition of the <see cref="ContentTemplate" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ContentTemplateProperty =
			DependencyProperty.Register("ContentTemplate", typeof (DataTemplate), typeof (CircularProgressBar),
			                            new PropertyMetadata(default(DataTemplate)));

		/// <summary>
		///     Definition of the <see cref="IndeterminateAngle" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IndeterminateAngleProperty =
			DependencyProperty.Register("IndeterminateAngle", typeof (double),
			                            typeof (CircularProgressBar), new PropertyMetadata(0.0, OnIndeterminateAngleChanged));

		private Ellipse _indicator;

		static CircularProgressBar()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (CircularProgressBar),
			                                         new FrameworkPropertyMetadata(typeof (CircularProgressBar)));
		}

		/// <summary>
		///     Initializes this object.
		/// </summary>
		public CircularProgressBar()
		{
			ValueChanged += OnValueChanged;
			SizeChanged += OnSizeChanged;
		}

		/// <summary>
		///     The current angle of of the circle segment when this progress bar is indeterminate.
		/// </summary>
		public double IndeterminateAngle
		{
			get { return (double) GetValue(IndeterminateAngleProperty); }
			set { SetValue(IndeterminateAngleProperty, value); }
		}

		/// <summary>
		///     The data template, if any, that is used to present the <see cref="Content" />.
		/// </summary>
		public DataTemplate ContentTemplate
		{
			get { return (DataTemplate) GetValue(ContentTemplateProperty); }
			set { SetValue(ContentTemplateProperty, value); }
		}

		/// <summary>
		///     The content being displayed in the center of the circular progress bar.
		/// </summary>
		public object Content
		{
			get { return GetValue(ContentProperty); }
			set { SetValue(ContentProperty, value); }
		}

		/// <summary>
		///     The thickness of the circular progress bar.
		/// </summary>
		public double Thickness
		{
			get { return (double) GetValue(ThicknessProperty); }
			set { SetValue(ThicknessProperty, value); }
		}

		private static void OnIndeterminateAngleChanged(DependencyObject dependencyObject,
		                                                DependencyPropertyChangedEventArgs args)
		{
			((CircularProgressBar) dependencyObject).UpdateClip();
		}

		/// <summary>
		///     Called when the template's tree is generated.
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_indicator = (Ellipse)GetTemplateChild("PART_Indicator");

			UpdateClip();
		}

		private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
		{
			UpdateClip();
		}

		private void OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> args)
		{
			if (!IsIndeterminate)
			{
				UpdateClip();
			}
		}

		private void UpdateClip()
		{
			if (_indicator != null)
			{
				var geometry = new StreamGeometry();
				using (StreamGeometryContext dc = geometry.Open())
				{
					var center = new Point(_indicator.ActualWidth/2, _indicator.ActualHeight/2);
					double radius = _indicator.ActualWidth/2;
					var circle = new Circle
						{
							Center = center,
							Radius = radius,
						};

					double start, angle;
					if (IsIndeterminate)
					{
						start = IndeterminateAngle;
						angle = Math.PI/4;
					}
					else
					{
						start = Math.PI;
						double relativeValue = (Value - Minimum)/(Maximum - Minimum);
						angle = relativeValue*2*Math.PI;
					}

					dc.BeginFigure(center, true, true);
					dc.LineTo(circle.GetPoint(start), true, true);
					dc.ArcTo(circle.GetPoint(start + angle),
					         new System.Windows.Size(radius, radius),
					         angle,
					         angle > Math.PI,
					         SweepDirection.Clockwise,
					         true,
					         true);
					dc.LineTo(center, true, true);
				}

				_indicator.Clip = geometry;
			}
		}
	}
}