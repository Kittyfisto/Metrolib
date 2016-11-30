using System;
using System.Windows;
using System.Windows.Controls;

namespace Metrolib.Controls.Charts.Network
{
	internal sealed class NetworkPanel
		: Panel
	{
		public NetworkPanel()
		{
		}

		/// <summary>
		///     Determine required size.
		/// </summary>
		/// <param name="availableSize"></param>
		/// <returns></returns>
		protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize)
		{
			var maxSize = new System.Windows.Size();

			foreach (UIElement child in InternalChildren)
			{
				child.Measure(availableSize);

				maxSize.Height = Math.Max(child.DesiredSize.Height, maxSize.Height);
				maxSize.Width = Math.Max(child.DesiredSize.Width, maxSize.Width);
			}

			return maxSize;
		}

		/// <summary>
		///     Position and resize all children.
		/// </summary>
		/// <param name="finalSize"></param>
		/// <returns></returns>
		protected override System.Windows.Size ArrangeOverride(System.Windows.Size finalSize)
		{
			foreach (UIElement child in InternalChildren)
			{
				child.Arrange(new Rect(child.DesiredSize));
			}
			return finalSize;
		}
	}
}