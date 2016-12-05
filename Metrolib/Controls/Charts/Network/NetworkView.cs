using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls.Charts.Network
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A chart capable of displaying the relationships between nodes.
	///     Each item in <see cref="Nodes" /> represents a node and each item
	///     in <see cref="Edges" /> represents an edge between exactly two nodes.
	/// </summary>
	public sealed class NetworkView
		: Canvas
	{
		/// <summary>
		/// The <see cref="Panel.ZIndexProperty"/> value assigned to <see cref="NetworkViewNodeItem"/>.
		/// </summary>
		public const int NodeZIndex = 2;

		/// <summary>
		/// The <see cref="Panel.ZIndexProperty"/> value assigned to <see cref="System.Windows.Shapes.Line"/>.
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
			DependencyProperty.Register("Nodes", typeof (IEnumerable), typeof (NetworkView),
			                            new PropertyMetadata(null, OnNodesChanged));

		/// <summary>
		///     Definition of the <see cref="Layout" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty LayoutProperty =
			DependencyProperty.Register("Layout", typeof (Layout), typeof (NetworkView),
			                            new PropertyMetadata(null, OnLayoutChanged));

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
			var actualLayout = layout ?? new ForceDirectedLayout();
			_algorithm = actualLayout.CreateAlgorithm();
		}

		private readonly Dictionary<object, NetworkViewNodeItem> _nodesToItems;
		private readonly Dictionary<IEdge, System.Windows.Shapes.Line> _edgesToItems;
		private readonly Stopwatch _stopwatch;
		private readonly DispatcherTimer _timer;
		private INodeLayoutAlgorithm _algorithm;
		private bool _isLoaded;
		private List<NodePosition> _nodeBuffer;

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

			_nodesToItems = new Dictionary<object, NetworkViewNodeItem>();
			_edgesToItems = new Dictionary<IEdge, System.Windows.Shapes.Line>();

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
		public IEnumerable Nodes
		{
			get { return (IEnumerable) GetValue(NodesProperty); }
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
			var actualRect = new Rect(0, 0, ActualWidth, ActualHeight);
			foreach (var line in _edgesToItems.Values)
			{
				line.Arrange(actualRect);
			}

			if (_nodeBuffer != null)
			{
				double minX = double.MaxValue;
				double maxX = double.MinValue;
				double minY = double.MaxValue;
				double maxY = double.MinValue;

				for (int i = 0; i < _nodeBuffer.Count; ++i)
				{
					minX = Math.Min(minX, _nodeBuffer[i].Position.X);
					maxX = Math.Max(maxX, _nodeBuffer[i].Position.X);

					minY = Math.Min(minY, _nodeBuffer[i].Position.Y);
					maxY = Math.Max(maxY, _nodeBuffer[i].Position.Y);
				}

				double width = maxX - minX;
				double height = maxY - minY;

				if (width >= 0 && height >= 0)
				{
					var graphRect = new Rect(minX, minY, maxX - minX, maxY - minY);
					var rect = new Rect(0, 0, ActualWidth, ActualHeight);
					//var offset = rect.TopLeft - graphRect.TopLeft;
					var offset = new Vector(100, 100);

					foreach (NodePosition node in _nodeBuffer)
					{
						var position = node.Position + offset;
						Vector nodeOffset;

						NetworkViewNodeItem item;
						if (_nodesToItems.TryGetValue(node.Node, out item))
						{
							item.Arrange(new Rect(position, item.DesiredSize));
							nodeOffset = new Vector(item.DesiredSize.Width/2, item.DesiredSize.Height/2);
						}
						else
						{
							nodeOffset = new Vector(0, 0);
						}

						foreach (var pair in _edgesToItems)
						{
							var edgePosition = position + nodeOffset;
							var edge = pair.Key;
							var line = pair.Value;
							if (ReferenceEquals(edge.Node1, node.Node))
							{
								line.X1 = edgePosition.X;
								line.Y1 = edgePosition.Y;
							}
							if (ReferenceEquals(edge.Node2, node.Node))
							{
								line.X2 = edgePosition.X;
								line.Y2 = edgePosition.Y;
							}
						}
					}
				}
			}
			return finalSize;
		}

		#endregion

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
				_nodeBuffer = _algorithm.Update(dt);
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
			AddNodes(Nodes);
			AddEdges(Edges);
			_timer.Start();
		}

		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			ClearNodes();
			ClearEdges();
			_timer.Stop();
			_algorithm.Dispose();
			_isLoaded = false;
		}

		private static void OnNodesChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((NetworkView) dependencyObject).OnNodesChanged((IEnumerable) args.OldValue, (IEnumerable) args.NewValue);
		}

		private void OnNodesChanged(IEnumerable oldValue, IEnumerable newValue)
		{
			if (!_isLoaded)
				return;

			var notifiable = oldValue as INotifyCollectionChanged;
			if (notifiable != null)
			{
				notifiable.CollectionChanged -= NodesOnCollectionChanged;
			}

			ClearNodes();
			AddNodes(newValue);

			notifiable = newValue as INotifyCollectionChanged;
			if (notifiable != null)
			{
				notifiable.CollectionChanged += NodesOnCollectionChanged;
			}
		}

		private void NodesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			switch (args.Action)
			{
				case NotifyCollectionChangedAction.Add:
					AddNodes(args.NewItems);
					break;

				case NotifyCollectionChangedAction.Move:
					// We probably don't need to do anything as we don't
					// deal with indices.
					break;

				case NotifyCollectionChangedAction.Remove:
					RemoveNodes(args.OldItems);
					break;

				case NotifyCollectionChangedAction.Replace:
					RemoveNodes(args.OldItems);
					AddNodes(args.NewItems);
					break;

				case NotifyCollectionChangedAction.Reset:
					ClearNodes();
					AddNodes(args.NewItems);
					break;
			}
		}

		private void AddNodes(IEnumerable nodes)
		{
			if (nodes == null)
				return;

			foreach (object node in nodes)
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

		private void RemoveNodes(IEnumerable nodes)
		{
			if (nodes == null)
				return;

			foreach (object node in nodes)
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

			var notifiable = oldEdges as INotifyCollectionChanged;
			if (notifiable != null)
			{
				notifiable.CollectionChanged -= EdgesOnCollectionChanged;
			}

			ClearEdges();
			AddEdges(newEdges);

			notifiable = newEdges as INotifyCollectionChanged;
			if (notifiable != null)
			{
				notifiable.CollectionChanged += EdgesOnCollectionChanged;
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
					var item = new System.Windows.Shapes.Line
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
					System.Windows.Shapes.Line item;
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
	}
}