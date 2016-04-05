using System.Windows.Input;

namespace Metrolib.Controls
{
	public class MouseWheelGesture : MouseGesture
	{
		public enum WheelDirection
		{
			None,
			Up,
			Down,
		}

		public MouseWheelGesture()
			: base(MouseAction.WheelClick)
		{
		}

		public MouseWheelGesture(ModifierKeys modifiers)
			: base(MouseAction.WheelClick, modifiers)
		{
		}

		public WheelDirection Direction { get; set; }

		public static MouseWheelGesture WheelDown
		{
			get { return new MouseWheelGesture(ModifierKeys.Control) {Direction = WheelDirection.Down}; }
		}

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