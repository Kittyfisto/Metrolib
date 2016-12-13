using System;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Pie
{
	[TestFixture]
	public sealed class PieChartTest
	{
		[Test]
		[STAThread]
		[Description("Verifies that the correct default values are set upon construction")]
		public void TestCtor()
		{
			var chart = new PieChart();
			chart.Series.Should().BeNull();
			chart.TitleTemplate.Should().BeNull();
			chart.SumOfSlices.Should().Be(0);
		}
	}
}