using System.ComponentModel;
using System.Windows.Input;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     This interface is used by <see cref="SearchTextBox" /> to allow for an advanced search
	///     that can focus through every occurence of the search term.
	///     Must be implemented by the user of a <see cref="SearchTextBox" /> and then set to
	///     <see cref="SearchTextBox.CurrentSearch" />.
	/// </summary>
	public interface ISearch
		: INotifyPropertyChanged
	{
		/// <summary>
		///     This command shall move focus to the previous location.
		/// </summary>
		ICommand PreviousCommand { get; }

		/// <summary>
		///     This command shall move focus to the next location.
		/// </summary>
		ICommand NextCommand { get; }

		/// <summary>
		///     The number of times the search term was located in whatever data source.
		/// </summary>
		int LocationCount { get; }

		/// <summary>
		///     The index of the currently focused location.
		/// </summary>
		int CurrentLocationIndex { get; }
	}
}