using System.ComponentModel;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace
namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Defines the algorithm and its parameters, used to layout the graph.
	/// </summary>
	/// <remarks>
	/// <see cref="ForceDirectedLayout"/> for the default algorithm used to layout graphs.
	/// Can be subclassed in order to allow the use of a custom algorithm.
	/// </remarks>
	public abstract class Layout
		: INotifyPropertyChanged
	{
		/// <summary>
		///     Is fired whenever a property's value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		///     Creates a new algorithm that realizes the layout described by this class.
		/// </summary>
		/// <remarks>
		///     The algorithm shall react to changes made to this object, even after its construction
		///     when the next call to <see cref="INodeLayoutAlgorithm.Update" /> occurs.
		/// </remarks>
		/// <returns></returns>
		public abstract INodeLayoutAlgorithm CreateAlgorithm();

		/// <summary>
		///     Fires the <see cref="PropertyChanged" /> event.
		/// </summary>
		/// <param name="propertyName"></param>
		protected virtual void EmitPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}