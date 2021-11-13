using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Network.Algorithms
{
	[TestFixture]
	public sealed class SolvingNodeLayoutAlgorithmTest
	{
		private Mock<INodeLayoutAlgorithm> _algorithm;

		[SetUp]
		public void Setup()
		{
			_algorithm = new Mock<INodeLayoutAlgorithm>();
		}

		[Test]
		public void TestCtor()
		{
			var algorithm = new SolvingNodeLayoutAlgorithm(_algorithm.Object);
			var result = algorithm.Result;
			result.Should().NotBeNull();
			result.Should().BeEmpty();
		}
	}
}