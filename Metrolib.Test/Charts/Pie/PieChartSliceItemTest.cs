using System;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Pie
{
	[TestFixture]
	public sealed class PieChartSliceItemTest
	{
		[Test]
		[STAThread]
		public void TestCtor()
		{
			var item = new PieChartSliceItem();
			item.StartAngle.Should().Be(0);
			item.Direction.Should().Be(SliceDirection.BottomLeft);
		}
	}
}