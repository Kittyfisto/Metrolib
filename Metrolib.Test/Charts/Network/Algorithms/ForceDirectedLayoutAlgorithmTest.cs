using System;
using System.Windows;
using FluentAssertions;
using Metrolib.Controls.Charts.Network.Algorithms;
using Moq;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Network.Algorithms
{
	[TestFixture]
	public sealed class ForceDirectedLayoutAlgorithmTest
	{
		[SetUp]
		public void Setup()
		{
			_layout = new ForceDirectedLayout();
			_algorithm = new ForceDirectedLayoutAlgorithm(_layout);
		}

		private ForceDirectedLayout _layout;
		private ForceDirectedLayoutAlgorithm _algorithm;

		[Test]
		[Description("Verifies that a node can be frozen")]
		public void TestFreeze1()
		{
			var node = new Mock<INode>();
			_algorithm.AddNode(node.Object);
			new Action(() => _algorithm.Freeze(node.Object)).ShouldNotThrow();
		}

		[Test]
		[Description("Verifies that frozen nodes do not move, even if forces should act upon them")]
		public void TestFreeze2()
		{
			var node1 = new Mock<INode>().Object;
			var node2 = new Mock<INode>().Object;

			_algorithm.AddNode(node1);
			_algorithm.AddNode(node2);
			_algorithm.AddEdge(Edge.Create(node1, node2));
			_algorithm.Freeze(node1);

			_algorithm.Update(TimeSpan.FromMilliseconds(60));
			_algorithm.Result[node1].Should().Be(new Point(0, 0), "because the frozen node shouldn't have moved");
			_algorithm.Result[node2].Should().NotBe(new Point(0, 0), "because only the non-frozen node should've moved");
		}
	}
}