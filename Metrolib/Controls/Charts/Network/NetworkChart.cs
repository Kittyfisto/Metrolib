using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace
namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class NetworkChart
		: ItemsControl
	{
		static NetworkChart()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(NetworkChart), new FrameworkPropertyMetadata(typeof(NetworkChart)));
		}

		protected override DependencyObject GetContainerForItemOverride()
		{
			return base.GetContainerForItemOverride();
		}
	}
}
