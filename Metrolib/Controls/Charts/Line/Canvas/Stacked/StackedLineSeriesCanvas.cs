using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Metrolib.Controls.Charts.Line.Canvas.Stacked
{
	/// <summary>
	///     Responsible for drawing a single <see cref="ILineSeries" /> as an area on top
	///     of the previous' series.
	/// </summary>
	public sealed class StackedLineSeriesCanvas
		: AbstractLineSeriesCanvas
	{
		private readonly StreamGeometry _geometry;
		private StackedLineSeriesCanvas _previous;

		private Point[] _stackedValues;
		private Range _stackedYRange;

		/// <summary>
		///     Initializes this canvas.
		/// </summary>
		/// <param name="lineSeries"></param>
		public StackedLineSeriesCanvas(ILineSeries lineSeries) : base(lineSeries)
		{
			_geometry = new StreamGeometry();
			_stackedValues = new Point[0];
		}

		/// <summary>
		///     The previous canvas on top of which this one resides.
		/// </summary>
		public StackedLineSeriesCanvas Previous
		{
			get { return _previous; }
			set
			{
				if (value == _previous)
					return;

				_previous = value;
				MakeDirty();
			}
		}

		/// <summary>
		///     The stacked range of y values.
		/// </summary>
		public Range StackedYRange
		{
			get { return _stackedYRange; }
		}

		/// <summary>
		///     The stacked values of this series (i.e. this series + previous.stackedvalues)
		/// </summary>
		public IEnumerable<Point> StackedValues
		{
			get { return _stackedValues; }
		}

		/// <summary>
		///     Recalculates <see cref="StackedValues" />.
		/// </summary>
		public void StackWithPrevious()
		{
			if (_previous == null)
			{
				_stackedValues = Series.Values.ToArray();
				_stackedYRange = Series.YRange;
			}
			else
			{
				_stackedValues = Add(Series.Values, Series.Count, _previous._stackedValues,
				                     out _stackedYRange);
			}
		}

		private Point[] Add(IEnumerable<Point> values, int count, Point[] stackedValues,
		                    out Range yRange)
		{
			Point[] ret;

			double min = Double.MaxValue;
			double max = Double.MinValue;

			// The two lists might have a different number of samples.
			// We obviously need to create a series with the highest number of samples
			// (or otherwise we couldn't approximate its shape properly).
			if (count > stackedValues.Length)
			{
				ret = new Point[count];
				IEnumerator<Point> it = values.GetEnumerator();

				for (int i = 0; i < count; ++i)
				{
					it.MoveNext();
					Point point = it.Current;
					double baseValue = GetYValue(stackedValues, point.X);
					double actualValue = baseValue + point.Y;
					ret[i] = new Point(point.X, actualValue);

					min = Math.Min(actualValue, min);
					max = Math.Max(actualValue, max);
				}
			}
			else if (count == stackedValues.Length)
			{
				ret = new Point[count];
				IEnumerator<Point> it = values.GetEnumerator();

				for (int i = 0; i < count; ++i)
				{
					it.MoveNext();
					Point point = it.Current;
					double baseValue = stackedValues[i].Y;
					double actualValue = baseValue + point.Y;
					ret[i] = new Point(point.X, actualValue);

					min = Math.Min(actualValue, min);
					max = Math.Max(actualValue, max);
				}
			}
			else /* if (count < stackedValues.Length) */
			{
				ret = new Point[stackedValues.Length];

				Point[] tmp = values.ToArray();

				for (int i = 0; i < stackedValues.Length; ++i)
				{
				}
			}

			yRange = new Range(min, max);

			return ret;
		}

		/// <summary>
		///     Gets the y-value at the given x-value.
		///     Performs linear interpolation, if necessary.
		///     TODO: This method might need to be enhanced by the interpolation method, if one can be specified (min/max/avg)
		/// </summary>
		/// <param name="values"></param>
		/// <param name="x"></param>
		/// <returns></returns>
		[Pure]
		public static double GetYValue(IEnumerable<Point> values, double x)
		{
			// TODO: Replace with binary search

			var p0 = new Point();
			var p1 = new Point();
			foreach (Point point in values)
			{
				if (point.X == x)
				{
					return point.Y;
				}
				if (point.X > x)
				{
					p1 = point;
				}
				else
				{
					p0 = point;
				}
			}

			// first we find where the given x is between p0 and p1
			double rel = (x - p0.X)/(p1.X - p0.X);
			// then we lerp between the p0.Y and p1.Y using the given value
			double y = rel*(p1.Y + p0.Y)*p0.Y;
			return y;
		}

		public override Range XRange
		{
			get { return Series.XRange; }
		}

		public override Range YRange
		{
			get { return _stackedYRange; }
		}

		/// <summary>
		/// Is called to actually draw the series.
		/// </summary>
		/// <param name="drawingContext"></param>
		public override void OnRender(DrawingContext drawingContext)
		{
			CreateArea();

			drawingContext.DrawGeometry(Series.Fill, null, _geometry);
		}

		private void CreateArea()
		{
			using (StreamGeometryContext context = _geometry.Open())
			{
				// TODO: We should try to replace this by using a matrix
				//       transform that we push onto the DC; it might be faster
				//       than doing the transformation purely in software.
				List<Point> viewPoints = ProjectToView(_stackedValues, _stackedValues.Length);

				List<Point>.Enumerator it = viewPoints.GetEnumerator();
				if (it.MoveNext())
				{
					Point first = it.Current;
					Point last = first;
					context.BeginFigure(last, true, true);
					while (it.MoveNext())
					{
						last = it.Current;
						context.LineTo(last, false, false);
					}

					if (_previous != null)
					{
						// First, we draw "down" to the previous value:
						List<Point> previousViewPoints = ProjectToView(_previous._stackedValues, _previous._stackedValues.Length);
						double y = GetYValue(previousViewPoints, last.X);
						context.LineTo(new Point(last.X, y), false, false);

						previousViewPoints.Reverse();

						// Then we draw the baseline
						IEnumerator<Point> it2 = ((IEnumerable<Point>) previousViewPoints).GetEnumerator();
						while (it2.MoveNext())
						{
							context.LineTo(it2.Current, false, false);
						}

						// And after we're done, we have to draw up to our parent again,
						// but that's already done by specifying that this shape is closed
					}
					else
					{
						// Much more easier: We draw down, straight line to the most minimum x value,
						// then up again.
						context.LineTo(new Point(Width, Height), false, false);
						context.LineTo(new Point(0, Height), false, false);
					}
				}
			}
		}
	}
}