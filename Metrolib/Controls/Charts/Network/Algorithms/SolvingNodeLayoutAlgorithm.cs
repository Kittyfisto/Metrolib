using System;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A cascaded <see cref="INodeLayoutAlgorithm" /> that executes the underlying
	///     algorithm as long as it takes until either
	///     - the result converges
	///     - a limit is reached
	/// </summary>
	public sealed class SolvingNodeLayoutAlgorithm
		: INodeLayoutAlgorithm
	{
		private readonly INodeLayoutAlgorithm _algorithm;
		private readonly int _maxIterations;
		private readonly ConcurrentQueue<AlgorithmAction> _pendingActions;
		private AlgorithmResult _currentResult;
		private CancellationTokenSource _cancellationTokenSource;
		private Task<AlgorithmResult> _solver;

		/// <summary>
		///     Creates a new solver for the given algorithm.
		/// </summary>
		/// <param name="algorithm"></param>
		public SolvingNodeLayoutAlgorithm(INodeLayoutAlgorithm algorithm)
		{
			if (algorithm == null)
				throw new ArgumentNullException("algorithm");

			_algorithm = algorithm;
			_pendingActions = new ConcurrentQueue<AlgorithmAction>();
			_maxIterations = 100000;

			_cancellationTokenSource = new CancellationTokenSource();
			_currentResult = AlgorithmResult.Empty;
			_solver = Task.FromResult(_currentResult);
		}

		/// <summary>
		///     The current result of the algorithm.
		/// </summary>
		public AlgorithmResult Result
		{
			get { return _currentResult; }
		}

		void INodeLayoutAlgorithm.Update(TimeSpan elapsed)
		{
			if (_solver.IsCompleted)
			{
				_currentResult = _solver.Result;
			}
			else if (_solver.IsFaulted)
			{
				_currentResult = AlgorithmResult.Empty;
			}
		}

		/// <summary>
		///     Adds the given node to the list of nodes of the graph.
		/// </summary>
		/// <param name="node"></param>
		public void AddNode(object node)
		{
			_pendingActions.Enqueue(new AlgorithmAction(Type.Add | Type.Node, node));
			StartNewTask();
		}

		/// <summary>
		///     Removes the given node from the list of nodes of the graph.
		/// </summary>
		/// <param name="node"></param>
		public void RemoveNode(object node)
		{
			_pendingActions.Enqueue(new AlgorithmAction(Type.Remove | Type.Node, node));
			StartNewTask();
		}

		/// <summary>
		///     Removes all nodes from the graph.
		/// </summary>
		public void ClearNodes()
		{
			_pendingActions.Enqueue(new AlgorithmAction(Type.Clear | Type.Node));
			StartNewTask();
		}

		/// <summary>
		///     Adds the given edge to the graph.
		/// </summary>
		/// <remarks>
		///     Edges may point to nodes that have not been added (yet).
		/// </remarks>
		/// <param name="edge"></param>
		public void AddEdge(IEdge edge)
		{
			_pendingActions.Enqueue(new AlgorithmAction(Type.Add | Type.Edge, edge));
			StartNewTask();
		}

		/// <summary>
		///     Removes the given list of edges from the graph.
		/// </summary>
		/// <remarks>
		///     Edges may point to nodes that have not been added (yet).
		/// </remarks>
		/// <param name="edge"></param>
		public void RemoveEdge(IEdge edge)
		{
			_pendingActions.Enqueue(new AlgorithmAction(Type.Remove | Type.Edge, edge));
			StartNewTask();
		}

		/// <summary>
		///     Removes all edges from the graph.
		/// </summary>
		public void ClearEdges()
		{
			_pendingActions.Enqueue(new AlgorithmAction(Type.Clear | Type.Edge));
			StartNewTask();
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			_cancellationTokenSource.Dispose();
		}

		private void StartNewTask()
		{
			_cancellationTokenSource.Cancel();
			_cancellationTokenSource = new CancellationTokenSource();
			_solver = Task.Factory.StartNew(() => Solve(_cancellationTokenSource.Token));
		}

		/// <summary>
		///     Is called by a background thread to "solve" the algorithm with brute force, so to speak.
		/// </summary>
		/// <returns></returns>
		private AlgorithmResult Solve(CancellationToken token)
		{
			// Our solver is quite simple:
			// We try to "solve" the problem with a maximum number of iterations.
			// If we happen to stumble upon a result that appears stable, then we
			// break early.
			TimeSpan dt = TimeSpan.FromMilliseconds(60);
			for (int i = 0; i < _maxIterations; ++i)
			{
				AlgorithmAction action;
				if (_pendingActions.TryDequeue(out action))
				{
					Execute(action);
				}

				AlgorithmResult previous = _algorithm.Result;
				_algorithm.Update(dt);

				if (Converges(previous, _algorithm.Result))
					break;

				if (token.IsCancellationRequested)
					break;
			}
			return _algorithm.Result;
		}

		private void Execute(AlgorithmAction action)
		{
			if (action.Type.HasFlag(Type.Node))
			{
				var fn = action.Type & ~Type.Node;
				switch (fn)
				{
					case Type.Add:
						_algorithm.AddNode(action.Node);
						break;
					case Type.Remove:
						_algorithm.RemoveNode(action.Node);
						break;
					case Type.Clear:
						_algorithm.ClearNodes();
						break;
				}
			}
			else if (action.Type.HasFlag(Type.Edge))
			{
				var fn = action.Type & ~Type.Edge;
				switch (fn)
				{
					case Type.Add:
						_algorithm.AddEdge(action.Edge);
						break;
					case Type.Remove:
						_algorithm.RemoveEdge(action.Edge);
						break;
					case Type.Clear:
						_algorithm.ClearEdges();
						break;
				}
			}
		}

		/// <summary>
		///     Tests if the given result can be seen as converging.
		/// </summary>
		/// <param name="previous"></param>
		/// <param name="current"></param>
		/// <returns></returns>
		[Pure]
		private bool Converges(AlgorithmResult previous, AlgorithmResult current)
		{
			const double maximumDisplacement = 0.1;
			const double maximumDisplacementSquared = maximumDisplacement*maximumDisplacement;

			// I define converging as every node moving within 0.1 units / tick.
			for (int i = 0; i < current.Count; ++i)
			{
				NodePosition previousNode = previous[i];
				NodePosition currentNode = current[i];
				Vector delta = previousNode.Position - currentNode.Position;
				double squaredLength = delta.LengthSquared;
				if (squaredLength > maximumDisplacementSquared)
					return false;
			}

			return false;
		}

		private struct AlgorithmAction
		{
			public readonly IEdge Edge;
			public readonly object Node;
			public readonly Type Type;

			public AlgorithmAction(Type type)
			{
				Type = type;
				Node = null;
				Edge = null;
			}

			public AlgorithmAction(Type type, IEdge edge)
			{
				Type = type;
				Edge = edge;
				Node = null;
			}

			public AlgorithmAction(Type type, object node)
			{
				Type = type;
				Node = node;
				Edge = null;
			}

			public override string ToString()
			{
				if (Type.HasFlag(Type.Node))
				{
					return string.Format("{0}: {1}", Type, Node);
				}
				if (Type.HasFlag(Type.Edge))
				{
					return string.Format("{0}: {1}", Type, Edge);
				}

				return string.Format("{0}", Type);
			}
		}

		[Flags]
		private enum Type
		{
			Add = 0x001,
			Remove = 0x002,
			Clear = 0x004,

			Node = 0x010,
			Edge = 0x020
		}
	}
}