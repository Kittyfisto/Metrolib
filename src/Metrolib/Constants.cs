using System.Windows.Media;

namespace Metrolib
{
	/// <summary>
	/// </summary>
	public static class Constants
	{
		#region Colors

		/// <summary>
		/// </summary>
		public static readonly Color ForegroundColor;

		/// <summary>
		/// </summary>
		public static readonly Color ForegroundColorAccent;

		/// <summary>
		/// </summary>
		public static readonly Color ForegroundColorHovered;

		/// <summary>
		/// </summary>
		public static readonly Color ForegroundColorPressed;

		/// <summary>
		/// </summary>
		public static readonly Color ForegroundColorDisabled;

		#endregion

		#region Brushes

		/// <summary>
		/// </summary>
		public static readonly SolidColorBrush ForegroundBrush;

		/// <summary>
		/// </summary>
		public static readonly SolidColorBrush ForegroundBrushAccent;

		/// <summary>
		/// </summary>
		public static readonly SolidColorBrush ForegroundBrushHovered;

		/// <summary>
		/// </summary>
		public static readonly SolidColorBrush ForegroundBrushPressed;

		/// <summary>
		/// </summary>
		public static readonly SolidColorBrush ForegroundBrushDisabled;

		#endregion

		static Constants()
		{
			ForegroundColor = Color("#333333");
			ForegroundColorAccent = Color("#3998D6");
			ForegroundColorHovered = Color("#2061A1");
			ForegroundColorPressed = Color("#0B4680");
			ForegroundColorDisabled = Color("#C9DAEB");

			ForegroundBrush = new SolidColorBrush(ForegroundColor);
			ForegroundBrush.Freeze();

			ForegroundBrushAccent = new SolidColorBrush(ForegroundColorAccent);
			ForegroundBrushAccent.Freeze();

			ForegroundBrushHovered = new SolidColorBrush(ForegroundColorHovered);
			ForegroundBrushHovered.Freeze();

			ForegroundBrushPressed = new SolidColorBrush(ForegroundColorPressed);
			ForegroundBrushPressed.Freeze();

			ForegroundBrushDisabled = new SolidColorBrush(ForegroundColorDisabled);
			ForegroundBrushDisabled.Freeze();
		}

		private static Color Color(string color)
		{
			return (Color) ColorConverter.ConvertFromString(color);
		}
	}
}