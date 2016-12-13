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
			chart.SumOfSlices.Should().Be(0);
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
			var item = chart.Children.OfType<PieChartValueItem>().FirstOrDefault();
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
			var item = chart.Children.OfType<PieChartValueItem>().FirstOrDefault();
			item.Should().NotBeNull();
			item.ContentTemplate.Should().BeSameAs(template, "because the chart is supposed to forward the ValueTemplate to the ContentPresenter that presents DisplayValue");
		}
	}
}