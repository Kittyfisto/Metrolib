using System;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Pie
{
	[TestFixture]
	public sealed class PieChartLabelItemTest
	{
		[Test]
		[STAThread]
		public void TestCtor()
		{
			var item = new PieChartLabelItem();
			item.Content.Should().BeNull();
		}
	}
}