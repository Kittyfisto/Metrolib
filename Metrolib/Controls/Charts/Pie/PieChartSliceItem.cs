using System;
using System.Diagnostics.Contracts;
using System.Windows;
using System.Windows.Media;
using Metrolib.Geometry;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Responsible for drawing the actual slice that represents a <see cref="IPieSlice" />.
	/// </summary>
	public class PieChartSliceItem
		: FrameworkElement
	{
		/// <summary>
		///     Definition of the <see cref="StartAngle" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty StartAngleProperty =
			DependencyProperty.Register("StartAngle", typeof (double), typeof (PieChartSliceItem),
			                            new PropertyMetadata(0.0, OnStartAngleChanged));

		/// <summary>
		///     Definition of the <see cref="Center" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty CenterProperty =
			DependencyProperty.Register("Center", typeof (Point), typeof (PieChartSliceItem),
			                            new PropertyMetadata(default(Point), OnCenterChanged));

		private static readonly DependencyPropertyKey AnglePropertyKey
			= DependencyProperty.RegisterReadOnly("Angle", typeof (double), typeof (PieChartSliceItem),
			                                      new FrameworkPropertyMetadata(default(double),
			                                                                    FrameworkPropertyMetadataOptions.None,
			                                                                    OnAngleChanged));

		/// <summary>
		///     Definition of the <see cref="Angle" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty AngleProperty
			= AnglePropertyKey.DependencyProperty;

		/// <summary>
		///     Definition of the <see cref="Direction" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty DirectionProperty =
			DependencyProperty.Register("Direction", typeof (SliceDirection), typeof (PieChartSliceItem),
			                            new PropertyMetadata(SliceDirection.BottomLeft));

		/// <summary>
		///     Definition of the <see cref="OpenAngle" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty OpenAngleProperty =
			DependencyProperty.Register("OpenAngle", typeof (double), typeof (PieChartSliceItem),
			                            new PropertyMetadata(0.0, OnOpenAngleChanged));

		/// <summary>
		///     Definition of the <see cref="Slice" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SliceProperty =
			DependencyProperty.Register("Slice", typeof (IPieSlice), typeof (PieChartSliceItem),
			                            new PropertyMetadata(default(IPieSlice)));

		/// <summary>
		///     Definition of the <see cref="Radius" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty RadiusProperty =
			DependencyProperty.Register("Radius", typeof (double), typeof (PieChartSliceItem),
			                            new PropertyMetadata(0.0, OnRadiusChanged));

		private static readonly DependencyPropertyKey ShapePropertyKey
			= DependencyProperty.RegisterReadOnly("Shape", typeof (CircleSegment), typeof (PieChartSliceItem),
			                                      new FrameworkPropertyMetadata(default(CircleSegment),
			                                                                    FrameworkPropertyMetadataOptions.None));

		/// <summary>
		///     Definition of the <see cref="Shape" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ShapeProperty
			= ShapePropertyKey.DependencyProperty;

		/// <summary>
		///     The center of the circle.
		/// </summary>
		public Point Center
		{
			get { return (Point) GetValue(CenterProperty); }
			set { SetValue(CenterProperty, value); }
		}

		/// <summary>
		///     The bounding shape that approximates the slice's drawn area and is used to determine the
		///     placement of labels.
		/// </summary>
		public CircleSegment Shape
		{
			get { return (CircleSegment) GetValue(ShapeProperty); }
			protected set { SetValue(ShapePropertyKey, value); }
		}

		/// <summary>
		///     The angle halfway between <see cref="StartAngle" /> and <see cref="StartAngle" />+<see cref="OpenAngle" />.
		/// </summary>
		public double Angle
		{
			get { return (double) GetValue(AngleProperty); }
			protected set { SetValue(AnglePropertyKey, value); }
		}

		/// <summary>
		///     The direction the slice is pointing at, from the center of the pie chart.
		/// </summary>
		public SliceDirection Direction
		{
			get { return (SliceDirection) GetValue(DirectionProperty); }
			set { SetValue(DirectionProperty, value); }
		}

		/// <summary>
		///     The radius of this slice.
		/// </summary>
		public double Radius
		{
			get { return (double) GetValue(RadiusProperty); }
			set { SetValue(RadiusProperty, value); }
		}

		/// <summary>
		///     The slice being drawn.
		/// </summary>
		public IPieSlice Slice
		{
			get { return (IPieSlice) GetValue(SliceProperty); }
			set { SetValue(SliceProperty, value); }
		}

		/// <summary>
		///     The opening angle in radians of the circle segment.
		/// </summary>
		public double OpenAngle
		{
			get { return (double) GetValue(OpenAngleProperty); }
			set { SetValue(OpenAngleProperty, value); }
		}

		/// <summary>
		///     The start angle in radians of the circle segment with 0 being the bottom.
		/// </summary>
		public double StartAngle
		{
			get { return (double) GetValue(StartAngleProperty); }
			set { SetValue(StartAngleProperty, value); }
		}

		private static void OnCenterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((PieChartSliceItem) d).OnCenterChanged((Point) e.NewValue);
		}

		private void OnCenterChanged(Point newValue)
		{
			Shape = DetermineBoundingShape(newValue, StartAngle, OpenAngle, Radius);
		}

		private static void OnRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((PieChartSliceItem) d).OnRadiusChanged((double) e.NewValue);
		}

		private void OnRadiusChanged(double newValue)
		{
			Shape = DetermineBoundingShape(Center, StartAngle, OpenAngle, newValue);
		}

		[Pure]
		private static CircleSegment DetermineBoundingShape(Point center, double startAngle, double openAngle, double radius)
		{
			return new CircleSegment
				{
					Circle = new Circle
						{
							Center = center,
							Radius = radius
						},
					StartAngle = startAngle,
					EndAngle = startAngle + openAngle
				};
		}

		private static void OnAngleChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((PieChartSliceItem) dependencyObject).OnAngleChanged((double) args.NewValue);
		}

		private static void OnOpenAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((PieChartSliceItem) d).OnOpenAngleChanged((double) e.NewValue);
		}

		private void OnOpenAngleChanged(double newValue)
		{
			Angle = StartAngle + newValue/2;
			Shape = DetermineBoundingShape(Center, StartAngle, newValue, Radius);
		}

		private static void OnStartAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((PieChartSliceItem) d).OnStartAngleChanged((double) e.NewValue);
		}

		private void OnStartAngleChanged(double newValue)
		{
			Angle = newValue + OpenAngle/2;
			Shape = DetermineBoundingShape(Center, newValue, OpenAngle, Radius);
		}

		protected override void OnRender(DrawingContext drawingContext)
		{
			var shape = Shape;
			var geometry = new StreamGeometry();
			var pen = Slice.Outline;
			var brush = Slice.Fill;
			bool isStroked = pen != null;

			using (StreamGeometryContext context = geometry.Open())
			{
				context.BeginFigure(shape.Circle.Center, true, true);
				context.LineTo(shape.StartPoint, isStroked, false);
				context.ArcTo(shape.EndPoint,
							  new System.Windows.Size(shape.Circle.Radius, shape.Circle.Radius),
							  shape.StartAngle * 180 / Math.PI,
							  shape.OpenAngle > Math.PI,
							  SweepDirection.Clockwise,
							  isStroked,
							  false);
				context.LineTo(shape.Circle.Center, isStroked, true);
			}

			drawingContext.DrawGeometry(brush, pen, geometry);
		}

		private void OnAngleChanged(double angle)
		{
			if (angle >= 0 && angle < Math.PI/2)
			{
				Direction = SliceDirection.BottomLeft;
			}
			else if (angle >= Math.PI/2 && angle < Math.PI)
			{
				Direction = SliceDirection.TopLeft;
			}
			else if (angle >= Math.PI && angle < Math.PI*1.5)
			{
				Direction = SliceDirection.TopRight;
			}
			else
			{
				Direction = SliceDirection.BottomRight;
			}
		}
	}
}