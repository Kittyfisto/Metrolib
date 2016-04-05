using System;
using FluentAssertions;
using Metrolib.Controls;
using NUnit.Framework;

namespace Metrolib.Test
{
	[TestFixture]
	public sealed class RemoveButtonTest
	{
		[SetUp]
		[STAThread]
		public void SetUp()
		{
			_button = new RemoveButton();
		}

		private RemoveButton _button;

		[Test]
		[STAThread]
		public void TestChangeInverted()
		{
			_button.IsInverted = true;
			_button.IsWhite.Should().BeTrue();
			_button.IsBlack.Should().BeFalse();

			_button.IsInverted = false;
			_button.IsWhite.Should().BeFalse();
			_button.IsBlack.Should().BeTrue();
		}

		[Test]
		[STAThread]
		public void TestCtor()
		{
			_button.IsInverted.Should().BeFalse();
			_button.IsWhite.Should().BeFalse();
			_button.IsBlack.Should().BeTrue();
		}
	}
}