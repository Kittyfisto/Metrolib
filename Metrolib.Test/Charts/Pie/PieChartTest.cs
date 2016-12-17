using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
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
			chart.LabelTemplate.Should().BeNull();
			chart.ValueTemplate.Should().BeNull();
			chart.Outline.Should().BeNull();
			chart.SumOfSlices.Should().Be(0);
			chart.MinimumArcLength.Should().Be(0);
			chart.ClipToBounds.Should().BeTrue();
		}

		[Test]
		[STAThread]
		[Description("Verifies that Load/Unload/Load is allowed")]
		public void TestLoad1()
		{
			var chart = new PieChart {Series = new PieSeries {Slices = {new PieSlice()}}};
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));
			new Action(() => chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent))).ShouldNotThrow("because the control should allow for loading having been unloaded");
		}

		[Test]
		[STAThread]
		[Description("Verifies that Arrange can be called even if the control hasn't been loaded yet")]
		public void TestArrange1()
		{
			var chart = new PieChart { Series = new PieSeries { Slices = { new PieSlice() } } };
			new Action(() => chart.Arrange(new Rect(0, 0, 128, 128))).ShouldNotThrow("because the chart should be arrangeable without having been loaded");
		}

		[Test]
		[STAThread]
		[Description("Verifies that Arrange can be called even if the control is unloaded")]
		public void TestArrange2()
		{
			var chart = new PieChart { Series = new PieSeries { Slices = { new PieSlice() } } };
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));
			new Action(() => chart.Arrange(new Rect(0, 0, 128, 128))).ShouldNotThrow("because the chart should be arrangeable after having been previously unloaded and loaded again");
		}

		[Test]
		[STAThread]
		[Description("Verifies that setting a series with no slices is allowed")]
		public void TestPieSeries1()
		{
			var series = new PieSeries {Slices = new List<IPieSlice>()};
			var chart = new PieChart();
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
			new Action(() => chart.Series = series).ShouldNotThrow();
			chart.Children.Should().BeEmpty();
		}

		[Test]
		[STAThread]
		[Description("Verifies that setting a series with null slices is allowed")]
		public void TestPieSeries2()
		{
			var series = new PieSeries {Slices = null};
			var chart = new PieChart();
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));
			new Action(() => chart.Series = series).ShouldNotThrow();
			chart.Children.Should().BeEmpty();
		}

		[Test]
		[STAThread]
		[Description("Verifies that setting a series with one slice is allowed")]
		public void TestPieSeries3()
		{
			var chart = new PieChart();
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));

			var series = new PieSeries();
			series.Slices.Add(new PieSlice());
			chart.Series = series;
			chart.Children.Count.Should().Be(3, "because three items should be created for each item we add");
		}

		[Test]
		[STAThread]
		[Description("Verifies that the original DisplayValue property is represented by an item")]
		public void TestPieSeries4()
		{
			var chart = new PieChart();
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));

			var series = new PieSeries();
			series.Slices.Add(new PieSlice
				{
					DisplayedValue = "Display me"
				});
			chart.Series = series;
			PieChartValueItem item = chart.Children.OfType<PieChartValueItem>().FirstOrDefault();
			item.Should().NotBeNull();
			item.Content.Should().Be("Display me", "because the item should present the DisplayValue in a content presenter");
		}

		[Test]
		[STAThread]
		[Description("Verifies that the ValueTemplate is used to present the DisplayValue, if one is set")]
		public void TestPieSeries5()
		{
			var template = new DataTemplate();
			var chart = new PieChart {ValueTemplate = template};
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));

			var series = new PieSeries();
			series.Slices.Add(new PieSlice());
			chart.Series = series;
			PieChartValueItem item = chart.Children.OfType<PieChartValueItem>().FirstOrDefault();
			item.Should().NotBeNull();
			item.ContentTemplate.Should()
			    .BeSameAs(template,
			              "because the chart is supposed to forward the ValueTemplate to the ContentPresenter that presents DisplayValue");
		}

		[Test]
		[STAThread]
		[Description("Verifies that the chart listens to changes of the Slices property")]
		public void TestPieSeries6()
		{
			var template = new DataTemplate();
			var chart = new PieChart {ValueTemplate = template};
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));

			var series = new PieSeries();
			series.Slices.Add(new PieSlice());
			chart.Series = series;
			PieChartValueItem item = chart.Children.OfType<PieChartValueItem>().FirstOrDefault();
			item.Should().NotBeNull();

			series.Slices = null;
			chart.Children.Should().BeEmpty();
		}

		[Test]
		[STAThread]
		[Description("Verifies that the chart listens to changes of the Slices property")]
		public void TestPieSeries7()
		{
			var template = new DataTemplate();
			var chart = new PieChart {ValueTemplate = template};
			chart.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent));

			var series = new PieSeries();
			chart.Series = series;
			chart.Children.Should().BeEmpty();

			series.Slices = new List<IPieSlice> {new PieSlice()};
			PieChartValueItem item = chart.Children.OfType<PieChartValueItem>().FirstOrDefault();
			item.Should().NotBeNull("because we've exchanched the empty list of slices with one that offers one slice");
		}
	}
}