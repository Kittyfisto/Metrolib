using System.Windows;
using System.Windows.Input;

// ReSharper disable once CheckNamespace
namespace Metrolib
{
	/// <summary>
	///     Defines the <see cref="IsFocusedProperty" /> attached dependency property.
	///     Can be used to control the focus of a control from a view model.
	/// </summary>
	public static class FocusExtension
	{
		public static readonly DependencyProperty IsFocusedProperty =
			DependencyProperty.RegisterAttached(
				"IsFocused", typeof(bool), typeof(FocusExtension),
				new UIPropertyMetadata(false, null, OnCoerceValue));

		public static bool GetIsFocused(DependencyObject obj)
		{
			return (bool) obj.GetValue(IsFocusedProperty);
		}

		public static void SetIsFocused(DependencyObject obj, bool value)
		{
			obj.SetValue(IsFocusedProperty, value);
		}

		private static object OnCoerceValue(DependencyObject d, object baseValue)
		{
			if ((bool) baseValue)
				((UIElement) d).Focus();
			else if (((UIElement) d).IsFocused)
				Keyboard.ClearFocus();
			return (bool) baseValue;
		}
	}
}