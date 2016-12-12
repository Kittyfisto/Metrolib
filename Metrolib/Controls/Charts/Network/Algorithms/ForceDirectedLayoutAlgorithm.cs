using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows;
using Metrolib.Physics;

namespace Metrolib.Controls.Charts.Network.Algorithms
{
	/// <summary>
	///     Algorithm that performs a physical simulation where nodes repulse each other and edges are springs.
	/// </summary>
	/// <remarks>
	///     http://profs.etsmtl.ca/mMcGuffin/research/2012-mcguffin-simpleNetVis/mcguffin-2012-simpleNetVis.pdf.
	/// </remarks>
	public sealed class ForceDirectedLayoutAlgorithm
		: INodeLayoutAlgorithm
	{
		private readonly Dictionary<INode, Body> _bodiesByNode;
		private readonly List<IEdge> _edges;
		private readonly HashSet<INode> _frozenNodes;
		private readonly ForceDirectedLayout _layout;
		private readonly Random _rng;

		#region Simulation

		private readonly Attractor _attractor;
		private readonly EulerIntegrator _integrator;
		private readonly Spring _spring;
		private AlgorithmResult _result;

		#endregion

		/// <summary>
		///     Initializes this object.
		/// </summary>
		/// <param name="layout"></param>
		public ForceDirectedLayoutAlgorithm(ForceDirectedLayout layout)
		{
			if (layout == null)
				throw new ArgumentNullException("layout");

			_layout = layout;
			_bodiesByNode = new Dictionary<INode, Body>();
			_frozenNodes = new HashSet<INode>();
			_edges = new List<IEdge>();
			_rng = new Random(42);

			_integrator = new EulerIntegrator();
			_attractor = new Attractor();
			_spring = new Spring(_rng);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
		}

		/// <summary>
		///     The current result of the algorithm.
		/// </summary>
		public AlgorithmResult Result
		{
			get { return _result; }
		}

		/// <summary>
		///     Updates the algorithm.
		/// </summary>
		/// <remarks>
		///     Is *always* invoked from the UI thread.
		///     This method should not block for longer than a few milliseconds or otherwise the UI might become stuck.
		/// </remarks>
		/// <param name="elapsed"></param>
		public void Update(TimeSpan elapsed)
		{
			if (elapsed > TimeSpan.Zero)
			{
				double dt = Math.Min(0.06, elapsed.TotalSeconds);

				Repulse();
				Attract();
				UpdatePositions(dt);

				_result = AlgorithmResult.Create(_bodiesByNode.Select(CreateResult));
			}
		}

		/// <summary>
		///     Adds the given node to the list of nodes of the graph.
		/// </summary>
		/// <param name="node"></param>
		public void AddNode(INode node)
		{
			if (node == null)
				return;

			_bodiesByNode.Add(node, new Body());
		}

		/// <summary>
		///     Removes the given node from the list of nodes of the graph.
		/// </summary>
		/// <param name="node"></param>
		public void RemoveNode(INode node)
		{
			if (node == null)
				return;

			_bodiesByNode.Remove(node);
		}

		/// <summary>
		///     Removes all nodes from the graph.
		/// </summary>
		public void ClearNodes()
		{
			_bodiesByNode.Clear();
		}

		/// <summary>
		///     Freezes the given so that its position doesn't change until the node is unfrozen (<see cref="Unfreeze" />) again.
		/// </summary>
		/// <remarks>
		///     Is invoked by the view when the user starts dragging nodes around.
		/// </remarks>
		/// <param name="node"></param>
		public void Freeze(INode node)
		{
			_frozenNodes.Add(node);
		}

		/// <summary>
		///     Unfreezes the given node so that its may change, if the algorithm deems it necessary, of-course.
		/// </summary>
		/// <param name="node"></param>
		public void Unfreeze(INode node)
		{
			_frozenNodes.Remove(node);
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
			if (edge == null)
				return;

			_edges.Add(edge);
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
			if (edge == null)
				return;

			_edges.Remove(edge);
		}

		/// <summary>
		///     Removes all edges from the graph.
		/// </summary>
		public void ClearEdges()
		{
			_edges.Clear();
		}

		public void SetPosition(INode node, Point position)
		{
			Body body;
			if (_bodiesByNode.TryGetValue(node, out body))
			{
				body.Position = position;
			}
		}

		private KeyValuePair<INode, Point> CreateResult(KeyValuePair<INode, Body> pair)
		{
			return new KeyValuePair<INode, Point>(pair.Key, pair.Value.Position);
		}

		private void Repulse()
		{
			_attractor.Force = -_layout.Repulsiveness;

			foreach (Body node1 in _bodiesByNode.Values)
			{
				foreach (Body node2 in _bodiesByNode.Values)
				{
					if (ReferenceEquals(node1, node2))
						continue;

					_attractor.ApplyForces(node2, node1);
				}
			}
		}

		private void Attract()
		{
			_spring.Length = _layout.Distance;
			_spring.Stiffness = _layout.SpringStiffness;
			_spring.Dampening = _layout.SpringDampening;

			foreach (IEdge edge in _edges)
			{
				Body node1 = GetBody(edge.Node1);
				if (node1 == null)
					continue;
				Body node2 = GetBody(edge.Node2);
				if (node2 == null)
					continue;

				_spring.ApplyForces(node1, node2);
			}
		}

		private void UpdatePositions(double dt)
		{
			foreach (var node in _frozenNodes)
			{
				var body = GetBody(node);
				if (body != null)
				{
					body.Velocity = new Vector(0, 0);
					body.Force = new Vector(0, 0);
				}
			}

			_integrator.Update(_bodiesByNode.Values, dt);
		}

		[Pure]
		private Body GetBody(INode node)
		{
			if (node == null)
				return null;

			Body body;
			if (!_bodiesByNode.TryGetValue(node, out body))
				return null;

			return body;
		}
	}
}