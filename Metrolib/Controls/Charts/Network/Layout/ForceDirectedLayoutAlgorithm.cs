using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Windows;

namespace Metrolib.Controls.Charts.Network.Layout
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
		private readonly List<IEdge> _edges;
		private readonly Dictionary<object, Node> _nodes;

		public ForceDirectedLayoutAlgorithm()
		{
			_nodes = new Dictionary<object, Node>();
			_edges = new List<IEdge>();
		}

		public void Dispose()
		{
		}

		public void Update()
		{
			double dt = TimeSpan.FromMilliseconds(60).TotalSeconds;
			Repulse();
			Attract();
			UpdatePositions(dt);
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
			_nodes.Clear();
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

		private void Repulse()
		{
			var k_r = new Vector(1, 1);
			foreach (Node node1 in _nodes.Values)
			{
				foreach (Node node2 in _nodes.Values)
				{
					if (ReferenceEquals(node1, node2))
						continue;

					Vector delta = node2.Position - node1.Position;
					if (delta.X != 0 || delta.Y != 0)
					{
						double distanceSquared = delta.LengthSquared;
						double distance = Math.Sqrt(distanceSquared);
						Vector force = k_r/distanceSquared;
						double dfx = force.X*delta.X/distance;
						double dfy = force.Y*delta.Y/distance;
						var df = new Vector(dfx, dfy);

						node1.Force -= df;
						node2.Force += df;
					}
				}
			}
		}

		private void Attract()
		{
			// spring rest length
			int L = 10;
			// spring constant
			int k_s = 1;

			foreach (IEdge edge in _edges)
			{
				Node node1 = GetNode(edge.Node1);
				if (node1 == null)
					continue;
				Node node2 = GetNode(edge.Node2);
				if (node2 == null)
					continue;

				Vector delta = node2.Position - node1.Position;
				if (delta.X != 0 || delta.Y != 0)
				{
					double distance = delta.Length;
					double force = k_s*(distance - L);
					Vector df = force*delta/distance;
					node1.Force += df;
					node2.Force -= df;
				}
			}
		}

		[Pure]
		private Node GetNode(object dataContext)
		{
			if (dataContext == null)
				return null;

			Node node;
			if (!_nodes.TryGetValue(dataContext, out node))
				return null;

			return node;
		}

		private void UpdatePositions(double dt)
		{
			const double maxDisplacementSquared = 10;

			foreach (Node node in _nodes.Values)
			{
				Vector dp = node.Force*dt;
				double displacementSquared = dp.LengthSquared;
				if (displacementSquared > maxDisplacementSquared)
				{
					double s = Math.Sqrt(maxDisplacementSquared/displacementSquared);
					dp /= s;
				}
				node.Position += dp;
			}
		}

		private void AddNode(object node)
		{
			if (node == null)
				return;

			_nodes.Add(node, new Node(node));
		}

		private void RemoveNode(object node)
		{
			if (node == null)
				return;

			_nodes.Remove(node);
		}

		private void RemoveEdge(IEdge edge)
		{
			if (edge == null)
				return;

			_edges.Remove(edge);
		}
	}
}