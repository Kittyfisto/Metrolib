// ReSharper disable CheckNamespace

using System.Windows;
using System.Windows.Controls;

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Similar to a <see cref="StackPanel" /> in that children are either laid out in a vertical
	///     or horizontal manner. But each child is assigned a 1* column, giving each equal width/height.
	/// </summary>
	public class GridPanel
		: Grid
	{
		/// <summary>
		///     Definition of the <see cref="Orientation" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty OrientationProperty =
			DependencyProperty.Register("Orientation", typeof (Orientation), typeof (GridPanel),
			                            new PropertyMetadata(Orientation.Vertical));

		/// <summary>
		///     The orientation of the children added to this panel.
		/// </summary>
		public Orientation Orientation
		{
			get { return (Orientation) GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}

		/// <summary>
		///     Measures the children of a System.Windows.Controls.Grid in anticipation of arranging them during the System.Windows.Controls.Grid.ArrangeOverride(System.Windows.Size) pass.
		/// </summary>
		/// <param name="availableSize"></param>
		/// <returns></returns>
		protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize)
		{
			if (Children.Count > 0)
			{
				System.Windows.Size size;
				if (Orientation == Orientation.Horizontal)
				{
					size = new System.Windows.Size(availableSize.Width/Children.Count, availableSize.Height);
				}
				else
				{
					size = new System.Windows.Size(availableSize.Width, availableSize.Height/Children.Count);
				}

				foreach (UIElement child in Children)
				{
					child.Measure(size);
				}
			}
			return availableSize;
		}

		/// <summary>
		///     Arranges the content of a System.Windows.Controls.Grid element.
		/// </summary>
		/// <param name="finalSize"></param>
		/// <returns></returns>
		protected override System.Windows.Size ArrangeOverride(System.Windows.Size finalSize)
		{
			if (Children.Count > 0)
			{
				for (int i = 0; i < Children.Count; ++i)
				{
					Rect rect;
					if (Orientation == Orientation.Horizontal)
					{
						double width = finalSize.Width/Children.Count;
						rect = new Rect(i*width, 0, width, finalSize.Height);
					}
					else
					{
						double height = finalSize.Height/Children.Count;
						rect = new Rect(0, i*height, finalSize.Width, height);
					}
					Children[i].Arrange(rect);
				}
			}
			return finalSize;
		}
	}
}