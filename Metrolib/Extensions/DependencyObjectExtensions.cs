using System.Windows;
using System.Windows.Media;

// ReSharper disable once CheckNamespace
namespace Metrolib
{
	/// <summary>
	/// Contains extension methods for dependency objects.
	/// </summary>
	public static class DependencyObjectExtensions
	{
		/// <summary>
		/// Finds the first ancestor that is of the given type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="that"></param>
		/// <returns></returns>
		public static T FindFirstAncestorOfType<T>(this DependencyObject that) where T : DependencyObject
		{
			DependencyObject current = that;
			while (current != null && !(current is T))
			{
				current = VisualTreeHelper.GetParent(current);
			}

			return current as T;
		}
	}
}