using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Windows;

namespace Metrolib.Controls.Charts.Network.Algorithms
{
	/// <summary>
	///     Algorithm that performs a physical simulation where nodes repulse each other and edges are springs.
	/// </summary>
	/// <remarks>
	///     http://profs.etsmtl.ca/mMcGuffin/research/2012-mcguffin-simpleNetVis/mcguffin-2012-simpleNetVis.pdf.
	/// </remarks>
	internal sealed class ForceDirectedLayoutAlgorithm
		: INodeLayoutAlgorithm
	{
		private readonly ForceDirectedLayout _layout;
		private readonly List<IEdge> _edges;
		private readonly Dictionary<object, Node> _nodesByDataContext;
		private readonly Random _rng;

		public ForceDirectedLayoutAlgorithm(ForceDirectedLayout layout)
		{
			if (layout == null)
				throw new ArgumentNullException("layout");

			_layout = layout;
			_nodesByDataContext = new Dictionary<object, Node>();
			_edges = new List<IEdge>();
			_rng = new Random(42);
		}

		public void Dispose()
		{
		}

		public List<NodePosition> Update(TimeSpan elapsed)
		{
			if (elapsed <= TimeSpan.Zero)
				return new List<NodePosition>();

			const double k_r = 6;
			double dt = Math.Min(0.06, elapsed.TotalSeconds);
			//double l = _layout.L;
			double l = 100;
			double r = _layout.R;
			//var k_s = k_r/(r*l*l*l);
			const double k_s = 1;
			const double d = 0.7;

			//Repulse(k_r);
			Attract(l, k_s, d);
			UpdatePositions(dt);

			var nodes = new List<NodePosition>(_nodesByDataContext.Count);
			foreach (var node in _nodesByDataContext.Values)
			{
				nodes.Add(new NodePosition
					{
						Node = node.DataContext,
						Position = node.Position
					});
			}
			return nodes;
		}

		private void Repulse(double k_r)
		{
			foreach (Node node1 in _nodesByDataContext.Values)
			{
				foreach (Node node2 in _nodesByDataContext.Values)
				{
					if (ReferenceEquals(node1, node2))
						continue;

					Vector delta = node2.Position - node1.Position;
					double distance = delta.Length;
					if (Math.Abs(distance) >= 1)
					{
						double distanceSquared = distance*distance;
						double force = k_r / distanceSquared;
						var df = force * delta / distance;

						node1.Force -= df;
						node2.Force += df;
						break;
					}
					else
					{
						// Let's nudge 'em apart
						var df = new Vector(_rng.NextDouble(), _rng.NextDouble());
						df.Normalize();
						df *= 10;

						node1.Force -= df;
						node2.Force += df;
					}
				}
			}
		}

		private void Attract(double l, double k_s, double d)
		{
			var spring = new Spring(_rng, k_s, l, d);
			foreach (IEdge edge in _edges)
			{
				Node node1 = GetNode(edge.Node1);
				if (node1 == null)
					continue;
				Node node2 = GetNode(edge.Node2);
				if (node2 == null)
					continue;

				var velocity = node1.Velocity - node2.Velocity;
				var force = spring.GetForce(node1.Position,
				                            node2.Position,
				                            velocity);
				node1.Force += force;
				node2.Force -= force;
			}
		}

		private void UpdatePositions(double dt)
		{
			const double maxDisplacementSquared = 500;

			foreach (Node node in _nodesByDataContext.Values)
			{
				Vector dv = node.Force*dt;
				node.Velocity += dv;
				Vector dp = node.Velocity * dt;
				node.Position += dp;

				node.Velocity *= 0.97;
				node.Force *= 0.8;
			}
		}

		public void AddNodes(IEnumerable nodes)
		{
			if (nodes == null)
				return;

			foreach (object node in nodes)
			{
				AddNode(node);
			}
		}

		public void RemoveNodes(IEnumerable nodes)
		{
			if (nodes == null)
				return;

			foreach (object node in nodes)
			{
				RemoveNode(node);
			}
		}

		public void ClearNodes()
		{
			_nodesByDataContext.Clear();
		}

		public void AddEdges(IEnumerable<IEdge> edges)
		{
			if (edges == null)
				return;

			_edges.AddRange(edges);
		}

		public void RemoveEdges(IEnumerable<IEdge> edges)
		{
			if (edges == null)
				return;

			foreach (IEdge edge in edges)
			{
				RemoveEdge(edge);
			}
		}

		public void ClearEdges()
		{
			_edges.Clear();
		}

		[Pure]
		private Node GetNode(object dataContext)
		{
			if (dataContext == null)
				return null;

			Node node;
			if (!_nodesByDataContext.TryGetValue(dataContext, out node))
				return null;

			return node;
		}

		public void AddNode(object node)
		{
			if (node == null)
				return;

			_nodesByDataContext.Add(node, new Node(node));
		}

		public void RemoveNode(object node)
		{
			if (node == null)
				return;

			_nodesByDataContext.Remove(node);
		}

		private void RemoveEdge(IEdge edge)
		{
			if (edge == null)
				return;

			_edges.Remove(edge);
		}
	}
}