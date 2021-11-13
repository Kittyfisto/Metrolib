// ReSharper disable CheckNamespace

using System.ComponentModel;

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// An optional view model that may be implemented by a view model that is being displayed
	/// in a <see cref="FlatTreeView"/>. The view model is notified about whether or not
	/// its hosting <see cref="FlatTreeViewItem"/> is selected and/or expanded.
	/// </summary>
	public interface ITreeViewItemViewModel
		: INotifyPropertyChanged
	{
		/// <summary>
		/// 
		/// </summary>
		bool IsSelected { get; set; }

		/// <summary>
		/// 
		/// </summary>
		bool IsExpanded { get; set; }
	}
}