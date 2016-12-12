using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     The content control that represents one node in <see cref="NetworkView.Nodes" />.
	/// </summary>
	public class NetworkViewNodeItem
		: ContentControl
	{
		/// <summary>
		///     Definition of the <see cref="IsSelected" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IsSelectedProperty =
			DependencyProperty.Register("IsSelected", typeof (bool), typeof (NetworkViewNodeItem),
			                            new PropertyMetadata(false));

		static NetworkViewNodeItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (NetworkViewNodeItem),
			                                         new FrameworkPropertyMetadata(typeof (NetworkViewNodeItem)));
		}

		/// <summary>
		///     Whether or not this item has been selected by the user.
		/// </summary>
		public bool IsSelected
		{
			get { return (bool) GetValue(IsSelectedProperty); }
			set { SetValue(IsSelectedProperty, value); }
		}

		/// <summary>
		///     The position of this node as determined by the <see cref="INodeLayoutAlgorithm" />
		/// </summary>
		public Point Position { get; set; }

		/// <summary>
		///     The actual position of this node on the canvas.
		/// </summary>
		/// <remarks>
		///     May differ from <see cref="Position" /> as the <see cref="NetworkView" /> tries to correct
		///     for drifts in the algorithm results.
		/// </remarks>
		public Point DisplayPosition { get; set; }
	}
}