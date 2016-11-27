using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows;
using System.Windows.Media;

// ReSharper disable CheckNamespace
namespace Metrolib
// ReSharper restore CheckNamespace
{
	internal sealed class StackedLineChartCanvas
		: AbstractLineChartCanvas
	{
		sealed class LineSeriesCanvas
			: AbstractLineSeriesCanvas
		{
			private readonly StreamGeometry _geometry;
			private LineSeriesCanvas _previous;

			public LineSeriesCanvas Previous
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

			private Point[] _stackedValues;

			public LineSeriesCanvas(ILineSeries lineSeries) : base(lineSeries)
			{
				_geometry = new StreamGeometry();
				_stackedValues = new Point[0];
			}

			public override bool Update()
			{
				if (Previous == null)
				{
					_stackedValues = Series.Values.ToArray();
				}
				else
				{
					_stackedValues = Add(Series.Values, Series.Count, Previous._stackedValues);
				}

				return base.Update();
			}

			private Point[] Add(IEnumerable<Point> values, int count, Point[] stackedValues)
			{
				Point[] ret;

				// The two lists might have a different number of samples.
				// We obviously need to create a series with the highest number of samples
				// (or otherwise we couldn't approximate its shape properly).
				if (count > stackedValues.Length)
				{
					ret = new Point[count];
					var it = values.GetEnumerator();

					for (int i = 0; i < count; ++i)
					{
						it.MoveNext();
						var point = it.Current;
						var baseValue = GetYValue(stackedValues, point.X);
						var actualValue = baseValue + point.Y;
						ret[i] = new Point(point.X, actualValue);
					}
				}
				else if (count == stackedValues.Length)
				{
					ret = new Point[count];
					var it = values.GetEnumerator();

					for (int i = 0; i < count; ++i)
					{
						it.MoveNext();
						var point = it.Current;
						var baseValue = stackedValues[i].Y;
						var actualValue = baseValue + point.Y;
						ret[i] = new Point(point.X, actualValue);
					}
				}
				else /* if (count < stackedValues.Length) */
				{
					ret = new Point[stackedValues.Length];

					var tmp = values.ToArray();

					for (int i = 0; i < stackedValues.Length; ++i)
					{
						
					}
				}

				return ret;
			}

			/// <summary>
			/// Gets the y-value at the given x-value.
			/// Performs linear interpolation, if necessary.
			/// TODO: This method might need to be enhanced by the interpolation method, if one can be specified (min/max/avg)
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
				foreach (var point in values)
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
				double rel = (x - p0.X) / (p1.X - p0.X);
				// then we lerp between the p0.Y and p1.Y using the given value
				double y = rel * (p1.Y + p0.Y) * p0.Y;
				return y;
			}

			public override void OnRender(DrawingContext drawingContext)
			{
				CreateArea();

				drawingContext.DrawGeometry(Series.Fill, null, _geometry);
			}

			private void CreateArea()
			{
				using (var context = _geometry.Open())
				{
					// TODO: We should try to replace this by using a matrix
					//       transform that we push onto the DC; it might be faster
					//       than doing the transformation purely in software.
					var viewPoints = ProjectToView(_stackedValues, _stackedValues.Length);

					var it = viewPoints.GetEnumerator();
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
							var previousViewPoints = ProjectToView(_previous._stackedValues, _previous._stackedValues.Length);
							var y = GetYValue(previousViewPoints, last.X);
							context.LineTo(new Point(last.X, y), false, false);

							previousViewPoints.Reverse();

							// Then we draw the baseline
							var it2 = ((IEnumerable<Point>)previousViewPoints).GetEnumerator();
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

		internal override void Update()
		{
			LineSeriesCanvas previous = null;
			foreach (LineSeriesCanvas series in SeriesCanvasses)
			{
				series.Previous = previous;
				previous = series;
			}

			base.Update();
		}

		protected override AbstractLineSeriesCanvas CreateCanvas(ILineSeries series)
		{
			return new LineSeriesCanvas(series);
		}
	}
}