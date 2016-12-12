using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

// ReSharper disable CheckNamespace
namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A view capable of displaying a graph consisting of <see cref="INode" />s and <see cref="IEdge" />s.
	///     Supports different <see cref="Layout" />s that control how the graph is presented.
	/// </summary>
	public sealed class NetworkView
		: Canvas
	{
		/// <summary>
		///     The <see cref="Panel.ZIndexProperty" /> value assigned to <see cref="NetworkViewNodeItem" />.
		/// </summary>
		public const int NodeZIndex = 2;

		/// <summary>
		///     The <see cref="Panel.ZIndexProperty" /> value assigned to <see cref="System.Windows.Shapes.Line" />.
		/// </summary>
		public const int EdgeZIndex = 1;

		/// <summary>
		///     Definition of the <see cref="NodeTemplate" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty NodeTemplateProperty =
			DependencyProperty.Register("NodeTemplate", typeof (DataTemplate), typeof (NetworkView),
			                            new PropertyMetadata(default(DataTemplate)));

		/// <summary>
		///     Definition of the <see cref="Edges" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty EdgesProperty =
			DependencyProperty.Register("Edges", typeof (IEnumerable<IEdge>), typeof (NetworkView),
			                            new PropertyMetadata(null, OnEdgesChanged));

		/// <summary>
		///     Definition of the <see cref="Nodes" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty NodesProperty =
			DependencyProperty.Register("Nodes", typeof (IEnumerable<INode>), typeof (NetworkView),
			                            new PropertyMetadata(null, OnNodesChanged));

		/// <summary>
		///     Definition of the <see cref="Layout" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty LayoutProperty =
			DependencyProperty.Register("Layout", typeof (Layout), typeof (NetworkView),
			                            new PropertyMetadata(null, OnLayoutChanged));

		private readonly Dictionary<IEdge, Line> _edgesToItems;
		private readonly Dictionary<INode, NetworkViewNodeItem> _nodesToItems;
		private readonly Stopwatch _stopwatch;
		private readonly DispatcherTimer _timer;
		private INodeLayoutAlgorithm _algorithm;
		private AlgorithmResult _currentPositions;
		private bool _isLoaded;

		#region Dragging

		private readonly List<NetworkViewNodeItem> _draggingNodes;
		private NetworkViewNodeItem _mouseDraggingNode;

		#endregion

		static NetworkView()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (NetworkView), new FrameworkPropertyMetadata(typeof (NetworkView)));
		}

		/// <summary>
		///     Initializes this <see cref="NetworkView" />.
		/// </summary>
		public NetworkView()
		{
			_stopwatch = new Stopwatch();

			_nodesToItems = new Dictionary<INode, NetworkViewNodeItem>();
			_edgesToItems = new Dictionary<IEdge, Line>();
			_draggingNodes = new List<NetworkViewNodeItem>();

			_timer = new DispatcherTimer(TimeSpan.FromMilliseconds(60), DispatcherPriority.Normal, Update, Dispatcher);
			Loaded += OnLoaded;
			Unloaded += OnUnloaded;
		}

		/// <summary>
		///     Defines the layout
		/// </summary>
		public Layout Layout
		{
			get { return (Layout) GetValue(LayoutProperty); }
			set { SetValue(LayoutProperty, value); }
		}

		/// <summary>
		///     The template being used to display individual nodes.
		/// </summary>
		public DataTemplate NodeTemplate
		{
			get { return (DataTemplate) GetValue(NodeTemplateProperty); }
			set { SetValue(NodeTemplateProperty, value); }
		}

		/// <summary>
		///     The list of nodes being displayed by this chart.
		/// </summary>
		public IEnumerable<INode> Nodes
		{
			get { return (IEnumerable<INode>) GetValue(NodesProperty); }
			set { SetValue(NodesProperty, value); }
		}

		/// <summary>
		///     The list of edges connections the nodes in <see cref="Nodes" />.
		/// </summary>
		public IEnumerable<IEdge> Edges
		{
			get { return (IEnumerable<IEdge>) GetValue(EdgesProperty); }
			set { SetValue(EdgesProperty, value); }
		}

		#region Arrange

		/// <summary>
		///     The bounding rectangle (min/max) values of the raw results of the current algorithm.
		/// </summary>
		public Rect BoundingRectangle
		{
			get
			{
				var min = new Point(double.MaxValue, double.MaxValue);
				var max = new Point(double.MinValue, double.MinValue);

				foreach (NetworkViewNodeItem node in InternalChildren.OfType<NetworkViewNodeItem>())
				{
					Point position = node.Position;
					System.Windows.Size desiredSize = node.DesiredSize;

					if (double.IsNaN(position.X) || double.IsNaN(position.Y))
					{
						// This is a problem...
					}
					else
					{
						min.X = Math.Min(min.X, position.X);
						min.Y = Math.Min(min.Y, position.Y);

						max.X = Math.Max(max.X, position.X + desiredSize.Width);
						max.Y = Math.Max(max.Y, position.Y + desiredSize.Height);
					}
				}

				double width = max.X - min.X;
				double height = max.Y - min.Y;
				if (width >= 0 && height >= 0)
					return new Rect(min, max);

				return new Rect();
			}
		}

		public Vector NodeOffset
		{
			get { return GetNodeOffset(new System.Windows.Size(ActualWidth, ActualHeight)); }
		}

		private Vector GetNodeOffset(System.Windows.Size size)
		{
			Rect rect = BoundingRectangle;
			Vector dataCenter = (Vector)rect.TopLeft + (Vector)rect.Size / 2;
			Vector center = (Vector)size / 2;
			Vector offset = dataCenter - center;
			return offset;
		}

		/// <summary>
		///     Determine required size.
		/// </summary>
		/// <param name="availableSize"></param>
		/// <returns></returns>
		protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize)
		{
			foreach (UIElement child in InternalChildren)
			{
				child.Measure(availableSize);
			}

			Rect rect = BoundingRectangle;
			return rect.Size;
		}

		/// <summary>
		///     Position and resize all children.
		/// </summary>
		/// <param name="finalSize"></param>
		/// <returns></returns>
		protected override System.Windows.Size ArrangeOverride(System.Windows.Size finalSize)
		{
			var actualRect = new Rect(0, 0, ActualWidth, ActualHeight);
			foreach (Line line in _edgesToItems.Values)
			{
				line.Arrange(actualRect);
			}

			if (_currentPositions != null)
			{
				var offset = GetNodeOffset(finalSize);

				foreach (var node in _currentPositions)
				{
					Point position = node.Value - offset;
					Vector nodeOffset;

					NetworkViewNodeItem item;
					if (_nodesToItems.TryGetValue(node.Key, out item))
					{
						if (ReferenceEquals(item, _mouseDraggingNode))
						{
							_algorithm.SetPosition(node.Key, item.Position);
						}
						else
						{
							item.DisplayPosition = position;
							item.Position = node.Value;
						}

						item.Arrange(new Rect(item.DisplayPosition, item.DesiredSize));
						nodeOffset = new Vector(item.DesiredSize.Width / 2, item.DesiredSize.Height / 2);
					}
					else
					{
						nodeOffset = new Vector(0, 0);
					}

					foreach (var pair in _edgesToItems)
					{
						Point edgePosition = item.DisplayPosition + nodeOffset;
						IEdge edge = pair.Key;
						Line line = pair.Value;
						if (ReferenceEquals(edge.Node1, node.Key))
						{
							line.X1 = edgePosition.X;
							line.Y1 = edgePosition.Y;
						}
						if (ReferenceEquals(edge.Node2, node.Key))
						{
							line.X2 = edgePosition.X;
							line.Y2 = edgePosition.Y;
						}
					}
				}
			}
			return finalSize;
		}

		#endregion

		private static void OnLayoutChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((NetworkView) d).OnLayoutChanged((Layout) e.NewValue);
		}

		private void OnLayoutChanged(Layout newValue)
		{
			if (_algorithm != null)
			{
				_algorithm.Dispose();
				_algorithm = null;
			}

			if (_isLoaded)
			{
				CreateLayoutAlgorithm(newValue);
			}
		}

		private void CreateLayoutAlgorithm(Layout layout)
		{
			Layout actualLayout = layout ?? new ForceDirectedLayout();
			_algorithm = actualLayout.CreateAlgorithm();
		}

		private void Update(object sender, EventArgs e)
		{
			TimeSpan dt = _stopwatch.Elapsed;
			Update(dt);
			_stopwatch.Restart();
		}

		internal void Update(TimeSpan dt)
		{
			if (_algorithm != null)
			{
				_algorithm.Update(dt);
				_currentPositions = _algorithm.Result;
				InvalidateArrange();
			}
		}

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			// I don't really understand why Loaded can be fired without Unloaded in between.
			// Is FlatTabControl to blame?
			if (_isLoaded)
				return;

			_isLoaded = true;
			CreateLayoutAlgorithm(Layout);

			AttachToNodes(Nodes);
			AddNodes(Nodes);

			AttachToEdges(Edges);
			AddEdges(Edges);

			_timer.Start();
		}

		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			ClearNodes();
			DetachFromNodes(Nodes);

			ClearEdges();
			DetachFromEdges(Edges);

			_timer.Stop();
			_algorithm.Dispose();
			_isLoaded = false;
		}

		private static void OnNodesChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((NetworkView) dependencyObject).OnNodesChanged((IEnumerable<INode>) args.OldValue,
			                                                (IEnumerable<INode>) args.NewValue);
		}

		private void OnNodesChanged(IEnumerable<INode> oldValue, IEnumerable<INode> newValue)
		{
			if (!_isLoaded)
				return;

			DetachFromNodes(oldValue);

			ClearNodes();
			AddNodes(newValue);

			AttachToNodes(newValue);
		}

		private void AttachToNodes(IEnumerable<INode> newValue)
		{
			var notifiable = newValue as INotifyCollectionChanged;
			if (notifiable != null)
			{
				notifiable.CollectionChanged += NodesOnCollectionChanged;
			}
		}

		private void DetachFromNodes(IEnumerable<INode> oldValue)
		{
			var notifiable = oldValue as INotifyCollectionChanged;
			if (notifiable != null)
			{
				notifiable.CollectionChanged -= NodesOnCollectionChanged;
			}
		}

		private void NodesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			switch (args.Action)
			{
				case NotifyCollectionChangedAction.Add:
					AddNodes(args.NewItems.Cast<INode>());
					break;

				case NotifyCollectionChangedAction.Move:
					// We probably don't need to do anything as we don't
					// deal with indices.
					break;

				case NotifyCollectionChangedAction.Remove:
					RemoveNodes(args.OldItems.Cast<INode>());
					break;

				case NotifyCollectionChangedAction.Replace:
					RemoveNodes(args.OldItems.Cast<INode>());
					AddNodes(args.NewItems.Cast<INode>());
					break;

				case NotifyCollectionChangedAction.Reset:
					ClearNodes();
					AddNodes(args.NewItems.Cast<INode>());
					break;
			}
		}

		private void AddNodes(IEnumerable<INode> nodes)
		{
			if (nodes == null)
				return;

			foreach (INode node in nodes)
			{
				if (node != null)
				{
					_algorithm.AddNode(node);
					var item = new NetworkViewNodeItem
						{
							Content = node
						};
					var binding = new Binding("NodeTemplate")
						{
							Source = this
						};
					BindingOperations.SetBinding(item, ContentPresenter.ContentTemplateProperty, binding);
					SetZIndex(item, NodeZIndex);

					_nodesToItems.Add(node, item);
					Children.Add(item);
				}
			}
		}

		private void ClearNodes()
		{
			RemoveNodes(_nodesToItems.Keys.ToList());
		}

		private void RemoveNodes(IEnumerable<INode> nodes)
		{
			if (nodes == null)
				return;

			foreach (INode node in nodes)
			{
				if (node != null)
				{
					_algorithm.RemoveNode(node);
					NetworkViewNodeItem item;
					if (_nodesToItems.TryGetValue(node, out item))
					{
						_nodesToItems.Remove(node);
						Children.Remove(item);
					}
				}
			}
		}

		private static void OnEdgesChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((NetworkView) dependencyObject).OnEdgesChanged((IEnumerable<IEdge>) args.OldValue,
			                                                (IEnumerable<IEdge>) args.NewValue);
		}

		private void OnEdgesChanged(IEnumerable<IEdge> oldEdges, IEnumerable<IEdge> newEdges)
		{
			if (!_isLoaded)
				return;

			DetachFromEdges(oldEdges);

			ClearEdges();
			AddEdges(newEdges);

			AttachToEdges(newEdges);
		}

		private void AttachToEdges(IEnumerable<IEdge> newEdges)
		{
			var notifiable = newEdges as INotifyCollectionChanged;
			if (notifiable != null)
			{
				notifiable.CollectionChanged += EdgesOnCollectionChanged;
			}
		}

		private void DetachFromEdges(IEnumerable<IEdge> oldEdges)
		{
			var notifiable = oldEdges as INotifyCollectionChanged;
			if (notifiable != null)
			{
				notifiable.CollectionChanged -= EdgesOnCollectionChanged;
			}
		}

		private void EdgesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			switch (args.Action)
			{
				case NotifyCollectionChangedAction.Add:
					AddEdges(args.NewItems);
					break;

				case NotifyCollectionChangedAction.Move:
					// We probably don't need to do anything as we don't
					// deal with indices.
					break;

				case NotifyCollectionChangedAction.Remove:
					RemoveEdges(args.OldItems);
					break;

				case NotifyCollectionChangedAction.Replace:
					RemoveEdges(args.OldItems);
					AddEdges(args.NewItems);
					break;

				case NotifyCollectionChangedAction.Reset:
					ClearEdges();
					AddEdges(args.NewItems);
					break;
			}
		}

		private void AddEdges(IEnumerable newItems)
		{
			if (newItems == null)
				return;

			foreach (IEdge edge in newItems)
			{
				if (edge != null)
				{
					_algorithm.AddEdge(edge);
					var item = new Line
						{
							DataContext = edge,
							StrokeThickness = 1,
							Stroke = Brushes.Black
						};
					SetZIndex(item, EdgeZIndex);
					_edgesToItems.Add(edge, item);
					Children.Add(item);
				}
			}
		}

		private void RemoveEdges(IEnumerable oldItems)
		{
			if (oldItems == null)
				return;

			foreach (IEdge edge in oldItems)
			{
				if (edge != null)
				{
					_algorithm.RemoveEdge(edge);
					Line item;
					if (_edgesToItems.TryGetValue(edge, out item))
					{
						_edgesToItems.Remove(edge);
						Children.Remove(item);
					}
				}
			}
		}

		private void ClearEdges()
		{
			RemoveEdges(_edgesToItems.Keys.ToList());
		}

		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			var result = VisualTreeHelper.HitTest(this, e.GetPosition(this));
			var node = result.VisualHit.FindFirstAncestorOfType<NetworkViewNodeItem>();
			if (node != null)
			{
				if (CaptureMouse())
				{
					_mouseDraggingNode = node;
					_algorithm.Freeze(node.Content as INode);
					e.Handled = true;
				}
			}
			else
			{
				base.OnMouseLeftButtonDown(e);
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (_mouseDraggingNode != null)
			{
				var position = e.GetPosition(this);
				_mouseDraggingNode.DisplayPosition = position;
				_mouseDraggingNode.Position = position - NodeOffset;

				InvalidateVisual();

				e.Handled = true;
			}
			else
			{
				base.OnMouseMove(e);
			}
		}

		protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
		{
			if (_mouseDraggingNode != null)
			{
				_algorithm.Unfreeze(_mouseDraggingNode.Content as INode);
				_mouseDraggingNode = null;
				ReleaseMouseCapture();
			}

			base.OnMouseLeftButtonUp(e);
		}
	}
}