﻿using System;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Threading;
using FluentAssertions;
using Metrolib.Controls;
using NUnit.Framework;

namespace Metrolib.Test
{
	[TestFixture]
	public sealed class FilterTextBoxTest
	{
		[STAThread]
		[SetUp]
		public void SetUp()
		{
			_control = new FilterTextBox
				{
					Style = (Style) _app.FindResource(typeof (FilterTextBox))
				};
			_control.ApplyTemplate().Should().BeTrue();
		}

		private FilterTextBox _control;
		private Application _app;

		[STAThread]
		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			_app = new Application();
		}

		public static void ExecuteEvents()
		{
			var frame = new DispatcherFrame();
			Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
			                                         new DispatcherOperationCallback(ExitFrame), frame);
			Dispatcher.PushFrame(frame);
		}

		public static object ExitFrame(object f)
		{
			((DispatcherFrame) f).Continue = false;
			return null;
		}

		[Test]
		[STAThread]
		public void TestRemoveFilterText1()
		{
			_control.FilterText = null;

			var peer = new ButtonAutomationPeer(_control.RemoveFilterTextButton);
			var invokeProv = (IInvokeProvider) peer.GetPattern(PatternInterface.Invoke);
			invokeProv.Invoke();

			_control.FilterText.Should().BeNull();
		}

		[Test]
		[STAThread]
		public void TestRemoveFilterText2()
		{
			_control.FilterText = "Foobar";

			var peer = new ButtonAutomationPeer(_control.RemoveFilterTextButton);
			var invokeProv = (IInvokeProvider) peer.GetPattern(PatternInterface.Invoke);
			invokeProv.Invoke();
			ExecuteEvents();

			_control.FilterText.Should().BeNull();
		}
	}
}