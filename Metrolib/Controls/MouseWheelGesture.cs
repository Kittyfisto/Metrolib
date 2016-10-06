using System.Windows.Input;

namespace Metrolib.Controls
{
	/// <summary>
	///     Allows the definition of gestures that depend on the user moving the mouse wheel
	///     in a given direction.
	/// </summary>
	public class MouseWheelGesture
		: MouseGesture
	{
		/// <summary>
		///     The direction the mouse wheel was moved in.
		/// </summary>
		public enum WheelDirection
		{
			/// <summary>
			///     The default value for <see cref="MouseWheelGesture.Direction" />.
			/// </summary>
			None,

			/// <summary>
			///     The "up" direction. This is usually the direction aimed away from the user (i.e. when
			///     the user scrolls a document upwards).
			/// </summary>
			Up,

			/// <summary>
			///     The "down" direction. This is usually the direction aimed towards the user (i.e. when
			///     the user scrolls a document downwards).
			/// </summary>
			Down,
		}

		/// <summary>
		///     Initializes this gesture.
		/// </summary>
		public MouseWheelGesture()
			: base(MouseAction.WheelClick)
		{
		}

		/// <summary>
		///     Initializes this gesture.
		/// </summary>
		/// <param name="modifiers"></param>
		public MouseWheelGesture(ModifierKeys modifiers)
			: base(MouseAction.WheelClick, modifiers)
		{
		}

		/// <summary>
		///     The direction the user has to move the mouse wheel in, in order
		///     for the gesture to match.
		/// </summary>
		public WheelDirection Direction { get; set; }

		/// <summary>
		///     A gesture that matches when the user moves mouse wheel down.
		/// </summary>
		public static MouseWheelGesture WheelDown
		{
			get { return new MouseWheelGesture(ModifierKeys.Control) {Direction = WheelDirection.Down}; }
		}

		/// <summary>
		///     A gesture that matches when the user moves mouse wheel up.
		/// </summary>
		public static MouseWheelGesture WheelUp
		{
			get { return new MouseWheelGesture(ModifierKeys.Control) {Direction = WheelDirection.Up}; }
		}

		public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
		{
			if (!(inputEventArgs is MouseWheelEventArgs))
				return false;

			var args = (MouseWheelEventArgs) inputEventArgs;
			switch (Direction)
			{
				case WheelDirection.None:
					return args.Delta == 0;
				case WheelDirection.Up:
					return args.Delta > 0;
				case WheelDirection.Down:
					return args.Delta < 0;
				default:
					return false;
			}
		}
	}
}