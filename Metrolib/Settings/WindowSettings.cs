using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Windows;
using System.Xml;

namespace Metrolib.Settings
{
	/// <summary>
	///     Represents the configuration of a window.
	///     Currently only preserves a window's position + dimension, but may be expanded in the future.
	/// </summary>
	public sealed class WindowSettings
		: ICloneable
	{
		/// <summary>
		///     The height of the window.
		/// </summary>
		public double Height;

		/// <summary>
		///     The left coordinate of the window's position.
		/// </summary>
		public double Left;

		/// <summary>
		///     The state of the window (i.e. normal, minimized, maximized, etc...).
		/// </summary>
		public WindowState State;

		/// <summary>
		///     The top coordinate of the window's position.
		/// </summary>
		public double Top;

		/// <summary>
		///     The width of the window.
		/// </summary>
		public double Width;

		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		///     Restores the given window's values to the ones in this object.
		/// </summary>
		/// <param name="window"></param>
		public void RestoreTo(Window window)
		{
			window.Left = Left;
			window.Top = Top;
			window.Width = Width;
			window.Height = Height;
			window.WindowState = State;
		}

		/// <summary>
		///     Restores all values from the given xml reader.
		/// </summary>
		/// <param name="reader"></param>
		public void Restore(XmlReader reader)
		{
			int count = reader.AttributeCount;
			for (int i = 0; i < count; ++i)
			{
				reader.MoveToAttribute(i);
				switch (reader.Name)
				{
					case "top":
						Top = reader.ReadContentAsDouble();
						break;

					case "left":
						Left = reader.ReadContentAsDouble();
						break;

					case "width":
						Width = reader.ReadContentAsDouble();
						break;

					case "height":
						Height = reader.ReadContentAsDouble();
						break;

					case "state":
						State = (WindowState) Enum.Parse(typeof (WindowState), reader.Value);
						break;
				}
			}
		}

		/// <summary>
		///     Saves all values to the given xml writer.
		/// </summary>
		/// <param name="writer"></param>
		public void Save(XmlWriter writer)
		{
			writer.WriteAttributeString("top", Top.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString("left", Left.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString("width", Width.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString("height", Height.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString("state", State.ToString());
		}

		/// <summary>
		///     Fetches all values from the given window (to be saved to an xml file, for example).
		/// </summary>
		/// <param name="window"></param>
		public void UpdateFrom(Window window)
		{
			Left = window.Left;
			Top = window.Top;
			Width = window.Width;
			Height = window.Height;
			State = window.WindowState;
		}

		/// <summary>
		///     Returns a clone.
		/// </summary>
		/// <returns></returns>
		[Pure]
		public WindowSettings Clone()
		{
			return new WindowSettings
				{
					Left = Left,
					Top = Top,
					Width = Width,
					Height = Height,
					State = State,
				};
		}
	}
}