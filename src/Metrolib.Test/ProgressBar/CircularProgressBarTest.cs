using Metrolib.Controls;
using NUnit.Framework;

namespace Metrolib.Test.ProgressBar
{
	[TestFixture]
	public sealed class CircularProgressBarTest
		: AbstractProgressBarTest
	{
		protected override AbstractProgressBar CreateProgressBar()
		{
			return new CircularProgressBar();
		}
	}
}