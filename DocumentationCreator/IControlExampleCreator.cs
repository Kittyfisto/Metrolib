using System;
using System.Windows;

namespace DocumentationCreator
{
	/// <summary>
	///     Responsible for creating the documentation for a particular control in a particular example.
	/// </summary>
	public interface IControlExampleCreator
		: IDisposable
	{
		/// <summary>
		///     Set a property to the given value.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="value"></param>
		void SetValue(DependencyProperty property, object value);

		/// <summary>
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		void Resize(int width, int height);

		/// <summary>
		/// </summary>
		void Focus();

		/// <summary>
		/// </summary>
		/// <param name="timeout"></param>
		void Wait(TimeSpan timeout);
	}

	/// <summary>
	///     Responsible for creating the documentation for a particular control in a particular example.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IControlExampleCreator<out T>
		: IControlExampleCreator where T : FrameworkElement
	{
		/// <summary>
		/// </summary>
		/// <param name="action"></param>
		void Prepare(Action<T> action);
	}
}