using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Network
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public sealed class NetworkViewTest
	{
		[Test]
		[Description("Verifies that null is an allowed value for the Nodes property")]
		public void TestNodes1()
		{
			var chart = new NetworkView();
			new Action(() => chart.Nodes = null).Should().NotThrow();
			chart.Nodes.Should().BeNull();
		}

		[Test]
		[Description("Verifies that an empty array is an allowed value for the Nodes property")]
		public void TestNodes2()
		{
			var chart = new NetworkView();
			new Action(() => chart.Nodes = new INode[0]).Should().NotThrow();
			chart.Nodes.Should().BeEmpty();
		}

		[Test]
		[Description("Verifies that we can set a list of one node to the Nodes property")]
		public void TestNodes3()
		{
			var chart = new NetworkView();
			var node = new Mock<INode>().Object;
			new Action(() => chart.Nodes = new []{node}).Should().NotThrow();
			chart.Nodes.Should().Equal(new[] {node});
		}

		[Test]
		[Description("Verifies that the items representing nodes are only created once the control has been loaded")]
		public void TestNodes4()
		{
			var chart = new NetworkView();
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));

			var node = new Mock<INode>().Object;
			chart.Nodes = new[] { node };
			chart.Children.Count.Should().Be(1, "because children should have been created since the control is loaded now");
		}

		[Test]
		[Description("Verifies that the items representing nodes are only created once the control has been loaded")]
		public void TestNodes5()
		{
			var chart = new NetworkView();
			var node = new Mock<INode>().Object;
			chart.Nodes = new[] { node };
			chart.Children.Count.Should().Be(0, "because children should only be created and added when the control is loaded");

			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
			chart.Children.Count.Should().Be(1, "because children should have been created since the control is loaded now");
		}

		[Test]
		[Description("Verifies that the items representing nodes are removed when the Nodes property is changed ot null")]
		public void TestNodes6()
		{
			var chart = new NetworkView();
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));

			var node = new Mock<INode>().Object;
			chart.Nodes = new[] { node };
			chart.Children.Count.Should().Be(1, "because children should have been created since the control is loaded now");

			chart.Nodes = null;
            chart.Children.Count.Should().Be(0);
        }

		[Test]
		[Description("Verifies that the item created actually represents the added node")]
		public void TestNodes7()
		{
			var chart = new NetworkView();
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));

			var node = new Mock<INode>().Object;
			chart.Nodes = new[] { node };
			var item = chart.Children.Cast<NetworkViewNodeItem>().FirstOrDefault();
			item.Should().NotBeNull();
			item.Content.Should().BeSameAs(node, "because the item should actually represent this node");
		}

		[Test]
		[Description("Verifies that a new item is created when a node is added to the list of nodes")]
		public void TestAddNodes1()
		{
			var chart = new NetworkView();
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));

			var nodes = new ObservableCollection<INode>();
			chart.Nodes = nodes;

			var node = new Mock<INode>().Object;
			nodes.Add(node);
			chart.Children.Count.Should().Be(1, "because the view should've reacted to the addition of a node");
			var item = chart.Children.Cast<NetworkViewNodeItem>().First();
			item.Should().NotBeNull();
			item.Content.Should().BeSameAs(node);
		}

		[Test]
		[Description("Verifies that the item representing a node is removed when the node itself is removed")]
		public void TestRemoveNodes1()
		{
			var chart = new NetworkView();
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));

			var node = new Mock<INode>().Object;
			var nodes = new ObservableCollection<INode> { node };
			chart.Nodes = nodes;
			chart.Children.Count.Should().Be(1, "because the view should've reacted to the addition of a node");

			nodes.Remove(node);
			chart.Children.Count.Should().Be(0, "because we've removed the only node and thus its representing item also should've been removed");
		}

		[Test]
		[Description("Verifies that null is an allowed value for the Edges property")]
		public void TestEdges1()
		{
			var chart = new NetworkView();
			new Action(() => chart.Edges = null).Should().NotThrow();
			chart.Nodes.Should().BeNull();
		}

		[Test]
		[STAThread]
		[Description("Verifies that an empty array is an allowed value for the Edges property")]
		public void TestEdges2()
		{
			var chart = new NetworkView();
			new Action(() => chart.Edges = new IEdge[0]).Should().NotThrow();
			chart.Edges.Should().BeEmpty();
		}

		[Test]
		[Description("Verifies that we can set a list of one edge to the Edges property")]
		public void TestEdges3()
		{
			var chart = new NetworkView();
			var edge = new Mock<IEdge>().Object;
			new Action(() => chart.Edges = new[] { edge }).Should().NotThrow();
			chart.Edges.Should().Equal(new[] { edge });
		}

		[Test]
		public void TestEdges4()
		{
			var chart = new NetworkView();
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));

			var edge = new Mock<IEdge>().Object;
			chart.Edges = new[] { edge };
			chart.Children.Count.Should().Be(1, "because children should have been created since the control is loaded now");
		}

		[Test]
		[Description("Verifies that the items representing edges are only created once the control has been loaded")]
		public void TestEdges5()
		{
			var chart = new NetworkView();
			var edge = new Mock<IEdge>().Object;
			chart.Edges = new[] { edge };
			chart.Children.Count.Should().Be(0, "because children should only be created and added when the control is loaded");

			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
			chart.Children.Count.Should().Be(1, "because children should have been created since the control is loaded now");
		}

		[Test]
		[Description("Verifies that the items representing edges are removed when the Edges property is changed ot null")]
		public void TestEdges6()
		{
			var chart = new NetworkView();
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));

			var edge = new Mock<IEdge>().Object;
			chart.Edges = new[] { edge };
			chart.Children.Count.Should().Be(1, "because children should have been created since the control is loaded now");

			chart.Edges = null;
            chart.Children.Count.Should().Be(0);
        }

		[Test]
		[Description("Verifies that the item created actually represents the added edge")]
		public void TestEdges7()
		{
			var chart = new NetworkView();
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));

			var edge = new Mock<IEdge>().Object;
			chart.Edges = new[] { edge };
			var item = chart.Children.Cast<FrameworkElement>().FirstOrDefault();
			item.Should().NotBeNull();
			item.DataContext.Should().BeSameAs(edge, "because the item should actually represent this edge");
		}

		[Test]
		[Description("Verifies that the chart can be loaded / unloaded multiple times")]
		public void TestLoad1()
		{
			var chart = new NetworkView();
			var node = new Mock<INode>().Object;
			chart.Nodes = new[] {node};
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
			chart.Children.Count.Should().Be(1);

			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));

			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
			chart.Children.Count.Should().Be(1);
		}

		[Test]
		[Description("Verifies that the chart can be loaded / unloaded multiple times")]
		public void TestLoad2()
		{
			var chart = new NetworkView();
			var edge = new Mock<IEdge>();
			chart.Edges = new[] { edge.Object };
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
			chart.Children.Count.Should().Be(1);

			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));

			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
			chart.Children.Count.Should().Be(1);
		}

		[Test]
		[Description("Verifies that the chart tollerates the Loaded event to be fired twice in succession")]
		public void TestLoad3()
		{
			var chart = new NetworkView();
			var node = new Mock<INode>().Object;
			chart.Nodes = new[] { node };
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
			chart.Children.Count.Should().Be(1);

			new Action(() => chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent))).Should().NotThrow();
			chart.Children.Count.Should().Be(1);
		}

		[Test]
		[Description("")]
		public void TestMeasure1()
		{
			var chart = new NetworkView();
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
			new Action(() => chart.Measure(new System.Windows.Size(double.MaxValue, double.MaxValue))).Should().NotThrow();
			chart.DesiredSize.Should().Be(new System.Windows.Size(), "because the chart doesn't have anything to display and thus consumes no area");
		}
	}
}