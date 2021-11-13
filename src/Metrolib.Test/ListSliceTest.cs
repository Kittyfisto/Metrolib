using System;
using System.Collections.Generic;
using FluentAssertions;
using Metrolib.Controls.TextBlocks;
using NUnit.Framework;

namespace Metrolib.Test
{
	[TestFixture]
	public sealed class ListSliceTest
	{
		[Test]
		public void TestSlice1()
		{
			var items = new List<int> {1};
			new Action(() => items.Slice(-1, 0)).Should().Throw<ArgumentOutOfRangeException>();
			new Action(() => items.Slice(0, -1)).Should().Throw<ArgumentOutOfRangeException>();
			new Action(() => items.Slice(0, 2)).Should().Throw<ArgumentOutOfRangeException>();
		}

		[Test]
		public void TestSlice2()
		{
			var items = new List<int> { 1 };
			var slice = items.Slice(0, 1);
			slice.Count.Should().Be(1);
			slice.Should().Equal(new int[] {1});
		}

		[Test]
		public void TestSlice3()
		{
			var items = new List<int> { 1, 2, 3, 4, 5 };
			var slice = items.Slice(1, 1);
			slice.Count.Should().Be(1);
			slice.Should().Equal(new int[] { 2 });
		}

		[Test]
		public void TestCreateEmptySlice()
		{
			var items = new List<int> { 1, 2, 3, 4, 5 };
			var slice = items.Slice(0, 0);
			slice.Count.Should().Be(0);
			slice.Should().Equal(new int[0]);
		}
	}
}