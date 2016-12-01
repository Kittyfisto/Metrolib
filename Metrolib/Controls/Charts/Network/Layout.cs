using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Metrolib.Controls.Charts.Network
{
	/// <summary>
	///     Defines the algorithm and its parameters, used to layout the graph.
	/// </summary>
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