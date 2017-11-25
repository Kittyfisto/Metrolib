using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

// ReSharper disable once CheckNamespace
namespace Metrolib
{
	/// <summary>
	///     Contains extension methods for dependency objects.
	/// </summary>
	public static class DependencyObjectExtensions
	{
		/// <summary>
		///     Finds the first ancestor that is of the given type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="that"></param>
		/// <returns></returns>
		public static T FindFirstAncestorOfType<T>(this DependencyObject that) where T : DependencyObject
		{
			var current = that;
			while (current != null && !(current is T))
				current = VisualTreeHelper.GetParent(current);

			return current as T;
		}

		/// <summary>
		///     Finds all children of the given type <typeparamref name="T" />.
		///     Starts with the given <paramref name="parent" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="parent"></param>
		/// <returns></returns>
		public static IEnumerable<T> FindChildrenOfType<T>(this DependencyObject parent)
			where T : DependencyObject
		{
			var type = parent as T;
			if (type != null)
				yield return type;

			var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
			for (var i = 0; i < childrenCount; i++)
			{
				var child = VisualTreeHelper.GetChild(parent, i);
				foreach (var other in FindChildrenOfType<T>(child))
					yield return other;
			}
		}
	}
}