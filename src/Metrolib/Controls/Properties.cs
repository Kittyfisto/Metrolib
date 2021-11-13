using System.Windows;
using System.Windows.Media;

namespace Metrolib.Controls
{
	/// <summary>
	///     Contains the attached dependency properties defined by this library.
	/// </summary>
	public class Properties
		: DependencyObject
	{
		#region ActualForeground

		/// <summary>
		///     This dependency property can be used to tell a <see cref="FrameworkElement" /> or even an entire sub-tree of the visual tree
		///     to render the foreground using the specified brush.
		/// </summary>
		/// <remarks>
		///     Most controls offered by this library properly implement an inverted style.
		/// </remarks>
		public static readonly DependencyProperty ActualForegroundProperty =
			DependencyProperty.RegisterAttached(
				"ActualForeground",
				typeof (Brush),
				typeof (Properties),
				new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

		/// <summary>
		///     Retrieves the value of the <see cref="ActualForegroundProperty" />.
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static Brush GetActualForeground(DependencyObject d)
		{
			return (Brush) d.GetValue(ActualForegroundProperty);
		}

		/// <summary>
		///     Sets the value of the <see cref="ActualForegroundProperty" />.
		/// </summary>
		/// <param name="d"></param>
		/// <param name="value"></param>
		public static void SetActualForeground(DependencyObject d, Brush value)
		{
			d.SetValue(ActualForegroundProperty, value);
		}

		#endregion

		#region IsInverted

		/// <summary>
		///     This dependency property can be used to tell a <see cref="FrameworkElement" /> or even an entire sub-tree of the visual tree
		///     to render inverted.
		/// </summary>
		/// <remarks>
		///     Most controls offered by this library properly implement an inverted style.
		/// </remarks>
		public static readonly DependencyProperty IsInvertedProperty =
			DependencyProperty.RegisterAttached(
				"IsInverted",
				typeof (bool),
				typeof (Properties),
				new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

		/// <summary>
		///     Retrieves the value of the <see cref="IsInvertedProperty" />.
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static bool GetIsInverted(DependencyObject d)
		{
			return (bool) d.GetValue(IsInvertedProperty);
		}

		/// <summary>
		///     Sets the value of the <see cref="IsInvertedProperty" />.
		/// </summary>
		/// <param name="d"></param>
		/// <param name="value"></param>
		public static void SetIsInverted(DependencyObject d, bool value)
		{
			d.SetValue(IsInvertedProperty, value);
		}

		#endregion
	}
}