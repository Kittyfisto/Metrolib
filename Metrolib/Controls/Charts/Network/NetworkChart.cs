using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using Metrolib.Controls.Charts.Network.Layout;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls.Charts.Network
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A chart capable of displaying the relationships between nodes.
	///     Each item in <see cref="Nodes" /> represents a node and each item
	///     in <see cref="Edges" /> represents an edge between exactly two nodes.
	/// </summary>
	public sealed class NetworkChart
		: Canvas
	{
		/// <summary>
		///     Definition of the <see cref="NodeTemplate" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty NodeTemplateProperty =
			DependencyProperty.Register("NodeTemplate", typeof (DataTemplate), typeof (NetworkChart),
			                            new PropertyMetadata(default(DataTemplate)));

		/// <summary>
		///     Definition of the <see cref="Edges" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty EdgesProperty =
			DependencyProperty.Register("Edges", typeof (IEnumerable<IEdge>), typeof (NetworkChart),
			                            new PropertyMetadata(null, OnEdgesChanged));

		/// <summary>
		///     /// Definition of the <see cref="Nodes" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty NodesProperty =
			DependencyProperty.Register("Nodes", typeof (IEnumerable), typeof (NetworkChart),
			                            new PropertyMetadata(null, OnNodesChanged));

		private readonly DispatcherTimer _timer;
		private readonly Stopwatch _stopwatch;
		private readonly Dictionary<object, ContentPresenter> _items;
		private INodeLayoutAlgorithm _algorithm;
		private List<Node> _nodeBuffer;
		private bool _isLoaded;

		static NetworkChart()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (NetworkChart), new FrameworkPropertyMetadata(typeof (NetworkChart)));
		}

		/// <summary>
		///     Initializes this <see cref="NetworkChart" />.
		/// </summary>
		public NetworkChart()
		{
			_stopwatch = new Stopwatch();
			_items = new Dictionary<object, ContentPresenter>();
			_timer = new DispatcherTimer(TimeSpan.FromMilliseconds(60), DispatcherPriority.Normal, Update, Dispatcher);
			Loaded += OnLoaded;
			Unloaded += OnUnloaded;
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

		private void Arrange()
		{
			InvalidateArrange();
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
			if (_nodeBuffer != null)
			{
				foreach (var node in _nodeBuffer)
				{
					ContentPresenter item;
					if (_items.TryGetValue(node.DataContext, out item))
					{
						item.Arrange(new Rect(node.Position, item.DesiredSize));
					}
				}
			}
			return finalSize;
		}

		#endregion

		private void Update(object sender, EventArgs e)
		{
			var dt = _stopwatch.Elapsed;
			Update(dt);
			_stopwatch.Restart();
		}

		internal void Update(TimeSpan dt)
		{
			if (_algorithm != null)
			{
				_algorithm.Update(dt, _nodeBuffer);
				Arrange();
			}
		}

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			_isLoaded = true;
			_algorithm = new ForceDirectedLayoutAlgorithm();
			AddNodes(Nodes);
			_algorithm.AddEdges(Edges);
			_nodeBuffer = new List<Node>();
			_timer.Start();
		}

		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			_timer.Stop();
			_algorithm.Dispose();
			_isLoaded = false;
		}

		private static void OnNodesChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((NetworkChart) dependencyObject).OnNodesChanged((IEnumerable) args.OldValue, (IEnumerable) args.NewValue);
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

			foreach (var node in nodes)
			{
				if (node != null)
				{
					_algorithm.AddNode(node);
					var item = new ContentPresenter
						{
							Content = node
						};
					var binding = new Binding("NodeTemplate")
						{
							Source = this
						};
					BindingOperations.SetBinding(item, ContentPresenter.ContentTemplateProperty, binding);

					_items.Add(node, item);
					Children.Add(item);
				}
			}
		}

		private void ClearNodes()
		{
			RemoveNodes(_items.Keys.ToList());
		}

		private void RemoveNodes(IEnumerable nodes)
		{
			if (nodes == null)
				return;

			foreach (var node in nodes)
			{
				if (node != null)
				{
					_algorithm.RemoveNode(node);
					ContentPresenter item;
					if (_items.TryGetValue(node, out item))
					{
						_items.Remove(node);
						Children.Remove(item);
					}
				}
			}
		}

		private static void OnEdgesChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((NetworkChart) dependencyObject).OnEdgesChanged((IEnumerable<IEdge>) args.OldValue,
			                                                 (IEnumerable<IEdge>) args.NewValue);
		}

		private void OnEdgesChanged(IEnumerable<IEdge> oldEdges, IEnumerable<IEdge> newEdges)
		{
			var notifiable = oldEdges as INotifyCollectionChanged;
			if (notifiable != null)
			{
				notifiable.CollectionChanged -= EdgesOnCollectionChanged;
			}

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
					_algorithm.AddEdges(args.NewItems.Cast<IEdge>());
					break;

				case NotifyCollectionChangedAction.Move:
					// We probably don't need to do anything as we don't
					// deal with indices.
					break;

				case NotifyCollectionChangedAction.Remove:
					_algorithm.RemoveEdges(args.OldItems.Cast<IEdge>());
					break;

				case NotifyCollectionChangedAction.Replace:
					_algorithm.RemoveEdges(args.OldItems.Cast<IEdge>());
					_algorithm.AddEdges(args.NewItems.Cast<IEdge>());
					break;

				case NotifyCollectionChangedAction.Reset:
					_algorithm.ClearEdges();
					_algorithm.AddEdges(args.NewItems.Cast<IEdge>());
					break;
			}
		}
	}
}