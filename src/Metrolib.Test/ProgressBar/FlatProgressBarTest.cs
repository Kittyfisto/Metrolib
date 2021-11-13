using Metrolib.Controls;
using NUnit.Framework;

namespace Metrolib.Test.ProgressBar
{
	[TestFixture]
	public sealed class FlatProgressBarTest
		: AbstractProgressBarTest
	{
		protected override AbstractProgressBar CreateProgressBar()
		{
			return new FlatProgressBar();
		}
	}
}