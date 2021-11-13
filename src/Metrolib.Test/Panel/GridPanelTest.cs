using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Panel
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public sealed class GridPanelTest
	{
		[Test]
		public void TestCtor()
		{
			var panel = new GridPanel();
			panel.Orientation.Should().Be(Orientation.Vertical);
			panel.ColumnDefinitions.Should().BeEmpty();
			panel.RowDefinitions.Should().BeEmpty();
            panel.Children.Count.Should().Be(0);
        }

		[Test]
		[Description("Verifies that Measure() can produce a proper desired size, even when it is given infinite size")]
		public void TestMeasure1()
		{
			var panel = new GridPanel();
			panel.Children.Add(new TextBlock {Text = "Hello, World!"});
			new Action(() => panel.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity)))
				.Should().NotThrow();
		}

		[Test]
		[Ignore("Rewrite to use measure/arrange")]
		public void TestVerticalAdd1()
		{
			var panel = new GridPanel();
			var element = new UIElement();
			panel.Children.Add(element);
			panel.RowDefinitions.Count.Should().Be(1);
			panel.RowDefinitions[0].Height.Should().Be(new GridLength(1, GridUnitType.Star));
			Grid.GetRow(element).Should().Be(0);
		}

		[Test]
		[Ignore("Rewrite to use measure/arrange")]
		public void TestVerticalAdd2()
		{
			var panel = new GridPanel();
			var element1 = new UIElement();
			var element2 = new FrameworkElement();
			panel.Children.Add(element1);
			panel.Children.Add(element2);
			panel.RowDefinitions.Count.Should().Be(2);
			panel.RowDefinitions[0].Height.Should().Be(new GridLength(1, GridUnitType.Star));
			panel.RowDefinitions[1].Height.Should().Be(new GridLength(1, GridUnitType.Star));
			Grid.GetRow(element1).Should().Be(0);
			Grid.GetRow(element2).Should().Be(1);
		}

		[Test]
		[Ignore("Rewrite to use measure/arrange")]
		public void TestVerticalInsert1()
		{
			var panel = new GridPanel();
			var element1 = new UIElement();
			var element2 = new FrameworkElement();
			panel.Children.Add(element1);
			panel.Children.Insert(0, element2);
			Grid.GetRow(element1).Should().Be(1, "because the index of the first child should've been changed now that the 2nd child is at [0]");
			Grid.GetRow(element2).Should().Be(0);
		}

		[Test]
		[Ignore("Rewrite to use measure/arrange")]
		public void TestVerticalInsert2()
		{
			var panel = new GridPanel();
			var element1 = new UIElement();
			var element2 = new FrameworkElement();
			var element3 = new Control();
			panel.Children.Add(element1);
			panel.Children.Insert(0, element2);
			panel.Children.Insert(1, element3);
			Grid.GetRow(element1).Should().Be(2);
			Grid.GetRow(element2).Should().Be(0);
			Grid.GetRow(element3).Should().Be(1);
		}

		[Test]
		[Ignore("Rewrite to use measure/arrange")]
		public void TestVerticalRemove1()
		{
			var panel = new GridPanel();
			var element1 = new UIElement();
			panel.Children.Add(element1);
			panel.RowDefinitions.Count.Should().Be(1);
			panel.Children.Remove(element1);
			panel.RowDefinitions.Should().BeEmpty();
		}

		[Test]
		[Ignore("Rewrite to use measure/arrange")]
		public void TestVerticalRemove2()
		{
			var panel = new GridPanel();
			var element1 = new UIElement();
			var element2 = new FrameworkElement();
			panel.Children.Add(element1);
			panel.Children.Add(element2);
			panel.RowDefinitions.Count.Should().Be(2);
			panel.Children.Remove(element1);
			panel.RowDefinitions.Count.Should().Be(1);

			Grid.GetRow(element2).Should().Be(0);
		}

		[Test]
		[Ignore("Rewrite to use measure/arrange")]
		public void TestVerticalRemove3()
		{
			var panel = new GridPanel();
			var element1 = new UIElement();
			var element2 = new FrameworkElement();
			var element3 = new Control();
			panel.Children.Add(element1);
			panel.Children.Add(element2);
			panel.Children.Add(element3);
			panel.RowDefinitions.Count.Should().Be(3);

			panel.Children.Remove(element1);
			panel.RowDefinitions.Count.Should().Be(2);

			Grid.GetRow(element2).Should().Be(0);
			Grid.GetRow(element3).Should().Be(1);
		}

		[Test]
		[Ignore("Rewrite to use measure/arrange")]
		public void TestHorizontalAdd1()
		{
			var panel = new GridPanel {Orientation = Orientation.Horizontal};
			var element = new UIElement();
			panel.Children.Add(element);
			panel.ColumnDefinitions.Count.Should().Be(1);
			panel.ColumnDefinitions[0].Width.Should().Be(new GridLength(1, GridUnitType.Star));
			Grid.GetColumn(element).Should().Be(0);
		}

		[Test]
		[Ignore("Rewrite to use measure/arrange")]
		public void TestHorizontalAdd2()
		{
			var panel = new GridPanel {Orientation = Orientation.Horizontal};
			var element1 = new UIElement();
			var element2 = new FrameworkElement();
			panel.Children.Add(element1);
			panel.Children.Add(element2);
			panel.ColumnDefinitions.Count.Should().Be(2);
			panel.ColumnDefinitions[0].Width.Should().Be(new GridLength(1, GridUnitType.Star));
			panel.ColumnDefinitions[1].Width.Should().Be(new GridLength(1, GridUnitType.Star));
			Grid.GetColumn(element1).Should().Be(0);
			Grid.GetColumn(element2).Should().Be(1);
		}

		[Test]
		[Ignore("Rewrite to use measure/arrange")]
		public void TestHorizontalInsert1()
		{
			var panel = new GridPanel {Orientation = Orientation.Horizontal};
			var element1 = new UIElement();
			var element2 = new FrameworkElement();
			panel.Children.Add(element1);
			panel.Children.Insert(0, element2);
			Grid.GetColumn(element1).Should().Be(1, "because the index of the first child should've been changed now that the 2nd child is at [0]");
			Grid.GetColumn(element2).Should().Be(0);
		}

		[Test]
		[Ignore("Rewrite to use measure/arrange")]
		public void TestHorizontalInsert2()
		{
			var panel = new GridPanel {Orientation = Orientation.Horizontal};
			var element1 = new UIElement();
			var element2 = new FrameworkElement();
			var element3 = new Control();
			panel.Children.Add(element1);
			panel.Children.Insert(0, element2);
			panel.Children.Insert(1, element3);
			Grid.GetColumn(element1).Should().Be(2);
			Grid.GetColumn(element2).Should().Be(0);
			Grid.GetColumn(element3).Should().Be(1);
		}

		[Test]
		[Ignore("Rewrite to use measure/arrange")]
		public void TestHorizontalRemove1()
		{
			var panel = new GridPanel { Orientation = Orientation.Horizontal };
			var element1 = new UIElement();
			panel.Children.Add(element1);
			panel.ColumnDefinitions.Count.Should().Be(1);
			panel.Children.Remove(element1);
			panel.ColumnDefinitions.Should().BeEmpty();
		}

		[Test]
		[Ignore("Rewrite to use measure/arrange")]
		public void TestHorizontalRemove2()
		{
			var panel = new GridPanel { Orientation = Orientation.Horizontal };
			var element1 = new UIElement();
			var element2 = new FrameworkElement();
			panel.Children.Add(element1);
			panel.Children.Add(element2);
			panel.ColumnDefinitions.Count.Should().Be(2);
			panel.Children.Remove(element1);
			panel.ColumnDefinitions.Count.Should().Be(1);

			Grid.GetColumn(element2).Should().Be(0);
		}

		[Test]
		[Ignore("Rewrite to use measure/arrange")]
		public void TestHorizontalRemove3()
		{
			var panel = new GridPanel { Orientation = Orientation.Horizontal };
			var element1 = new UIElement();
			var element2 = new FrameworkElement();
			var element3 = new Control();
			panel.Children.Add(element1);
			panel.Children.Add(element2);
			panel.Children.Add(element3);
			panel.ColumnDefinitions.Count.Should().Be(3);

			panel.Children.Remove(element1);
			panel.ColumnDefinitions.Count.Should().Be(2);

			Grid.GetColumn(element2).Should().Be(0);
			Grid.GetColumn(element3).Should().Be(1);
		}
	}
}