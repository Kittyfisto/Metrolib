using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Windows;

namespace Metrolib
{
	/// <summary>
	///     Extension methods for the <see cref="FrameworkElement" /> class.
	/// </summary>
	/// <remarks>
	///     TODO: Move this class to the Metrolib project
	/// </remarks>
	public static class FrameworkElementExtensions
	{
		/// <summary>
		///     Finds the first ancestor element of the given type (or sub-type thereof).
		///     If none is found, then null is returned.
		/// </summary>
		/// <param name="element"></param>
		/// <returns>The first ancestor with of the given type or null if there is none</returns>
		public static T FirstAncestorOfTypeOrDefault<T>(this FrameworkElement element)
			where T : FrameworkElement
		{
			foreach(var parent in Parents(element))
			{
				if (parent is T t)
					return t;
			}

			return null;
		}

		/// <summary>
		///     Finds the first ancestor element that has the given name, starting
		///     with the given element.
		/// </summary>
		/// <param name="element"></param>
		/// <param name="name"></param>
		/// <returns>The first ancestor with the given name or null if there is none</returns>
		public static FrameworkElement FindFirstAncestorWithName(this FrameworkElement element, string name)
		{
			foreach(var parent in Parents(element))
			{
				if (parent.Name == name)
					return element;
			}

			return null;
		}

		[Pure]
		private static IEnumerable<FrameworkElement> Parents(this FrameworkElement element)
		{
			while (element != null)
			{
				yield return element;
				element = element.Parent as FrameworkElement;
			}
		}
	}
}