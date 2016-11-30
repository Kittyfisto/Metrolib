using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
		: Control
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
			                            new PropertyMetadata(null, OnEdgeSourceChanged));

		/// <summary>
		///     /// Definition of the <see cref="Nodes" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty NodesProperty =
			DependencyProperty.Register("Nodes", typeof (IEnumerable), typeof (NetworkChart),
			                            new PropertyMetadata(default(IEnumerable)));

		private INodeLayoutAlgorithm _algorithm;
		private readonly DispatcherTimer _timer;

		static NetworkChart()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (NetworkChart), new FrameworkPropertyMetadata(typeof (NetworkChart)));
		}

		/// <summary>
		///     Initializes this <see cref="NetworkChart" />.
		/// </summary>
		public NetworkChart()
		{
			_timer = new DispatcherTimer(TimeSpan.FromMilliseconds(60), DispatcherPriority.Normal, Update, Dispatcher);
			Loaded += OnLoaded;
			Unloaded += OnUnloaded;
		}

		private void Update(object sender, EventArgs e)
		{
			if (_algorithm != null)
			{
				_algorithm.Update();
			}
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

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			_algorithm = new ForceDirectedLayoutAlgorithm();
			_algorithm.AddNodes(Nodes);
			_algorithm.AddEdges(Edges);
			_timer.Start();
		}

		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			_timer.Stop();
			_algorithm.Dispose();
		}

		private static void OnEdgeSourceChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((NetworkChart) dependencyObject).OnEdgeSourceChanged((IEnumerable<IEdge>) args.OldValue,
			                                                      (IEnumerable<IEdge>) args.NewValue);
		}

		private void OnEdgeSourceChanged(IEnumerable<IEdge> oldEdges, IEnumerable<IEdge> newEdges)
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