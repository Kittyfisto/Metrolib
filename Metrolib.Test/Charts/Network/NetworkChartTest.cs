using System;
using System.Windows;
using FluentAssertions;
using Metrolib.Controls.Charts.Network;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Network
{
	[TestFixture]
	public sealed class NetworkChartTest
	{
		[Test]
		[STAThread]
		public void TestNodes1()
		{
			var chart = new NetworkChart();
			new Action(() => chart.Nodes = null).ShouldNotThrow();
			chart.Nodes.Should().BeNull();
		}

		[Test]
		[STAThread]
		public void TestNodes2()
		{
			var chart = new NetworkChart();
			new Action(() => chart.Nodes = new object[0]).ShouldNotThrow();
			chart.Nodes.Should().BeEmpty();
		}

		[Test]
		[STAThread]
		public void TestNodes3()
		{
			var chart = new NetworkChart();
			var node = new object();
			new Action(() => chart.Nodes = new []{node}).ShouldNotThrow();
			chart.Nodes.Should().Equal(new[] {node});
		}

		[Test]
		[STAThread]
		public void TestNodes4()
		{
			var chart = new NetworkChart();
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));

			var node = new object();
			chart.Nodes = new[] { node };
			chart.Children.Count.Should().Be(1, "because children should have been created since the control is loaded now");
		}

		[Test]
		[STAThread]
		public void TestNodes5()
		{
			var chart = new NetworkChart();
			var node = new object();
			chart.Nodes = new[] { node };
			chart.Children.Should().BeEmpty("because children should only be created and added when the control is loaded");

			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
			chart.Children.Count.Should().Be(1, "because children should have been created since the control is loaded now");
		}

		[Test]
		[STAThread]
		public void TestLoad1()
		{
			var chart = new NetworkChart();
			var node = new object();
			chart.Nodes = new[] {node};
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
			chart.Children.Count.Should().Be(1);

			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));

			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
			chart.Children.Count.Should().Be(1);
		}
	}
}