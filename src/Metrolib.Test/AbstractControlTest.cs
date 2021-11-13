using System.Threading;
using System.Windows;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public abstract class AbstractControlTest
	{
		[Test]
		public void TestNamespace()
		{
			var control = Create();
			var type = control.GetType();
			type.Namespace.Should().Be("Metrolib.Controls", "because every control offered by this library should be part of the Metrolib.Controls namespace");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected abstract FrameworkElement Create();
	}
}
