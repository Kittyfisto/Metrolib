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
using System.Windows.Shapes;
using System.Windows.Threading;

// ReSharper disable CheckNamespace

namespace Metrolib
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
			DependencyProperty.Register("Nodes", typeof (IEnumerable), typeof (NetworkView),
			                            new PropertyMetadata(null, OnNodesChanged));

		/// <summary>
		///     Definition of the <see cref="Layout" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty LayoutProperty =
			DependencyProperty.Register("Layout", typeof (Layout), typeof (NetworkView),
			                            new PropertyMetadata(null, OnLayoutChanged));

		private readonly Dictionary<IEdge, Line> _edgesToItems;
		private readonly Dictionary<object, NetworkViewNodeItem> _nodesToItems;
		private readonly Stopwatch _stopwatch;
		private readonly DispatcherTimer _timer;
		private INodeLayoutAlgorithm _algorithm;
		private AlgorithmResult _currentPositions;
		private bool _isLoaded;

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
			_edgesToItems = new Dictionary<IEdge, Line>();

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
				Rect rect = BoundingRectangle;
				Vector dataCenter = (Vector) rect.TopLeft + (Vector) rect.Size/2;
				Vector center = (Vector) finalSize/2;
				Vector offset = dataCenter - center;

				foreach (NodePosition node in _currentPositions)
				{
					Point position = node.Position - offset;
					Vector nodeOffset;

					NetworkViewNodeItem item;
					if (_nodesToItems.TryGetValue(node.Node, out item))
					{
						item.Arrange(new Rect(position, item.DesiredSize));
						item.Position = node.Position;
						item.DisplayPosition = position;
						nodeOffset = new Vector(item.DesiredSize.Width/2, item.DesiredSize.Height/2);
					}
					else
					{
						nodeOffset = new Vector(0, 0);
					}

					foreach (var pair in _edgesToItems)
					{
						Point edgePosition = position + nodeOffset;
						IEdge edge = pair.Key;
						Line line = pair.Value;
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
	}
}