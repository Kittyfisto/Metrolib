// ReSharper disable CheckNamespace

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     This textbox allows a user to manually enter a path to a folder/file or click on a "more" button.
	///     Clients of this control must hook up this command to dialogs of their choice.
	/// </summary>
	[TemplatePart(Name = "PART_ChooseFolderButton", Type = typeof (Button))]
	public class PathChooserTextBox
		: TextBox
	{
		public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register(
		                                                "Watermark", typeof(string), typeof(PathChooserTextBox), new PropertyMetadata(default(string)));

		public static readonly DependencyProperty PathChooserCommandProperty = DependencyProperty.Register(
		                                                "PathChooserCommand", typeof(ICommand), typeof(PathChooserTextBox), new PropertyMetadata(default(ICommand)));

		static PathChooserTextBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(PathChooserTextBox), new FrameworkPropertyMetadata(typeof(PathChooserTextBox)));
		}

		/// <summary>
		/// The watermark which is displayed if <see cref="TextBox.Text"/> is empty.
		/// </summary>
		public string Watermark
		{
			get { return (string) GetValue(WatermarkProperty); }
			set { SetValue(WatermarkProperty, value); }
		}

		/// <summary>
		/// The command which is invoked when the more button is pressed.
		/// Clients of this control MUST hook up this command with a folder chooser
		/// dialog of their choice.
		/// </summary>
		public ICommand PathChooserCommand
		{
			get { return (ICommand) GetValue(PathChooserCommandProperty); }
			set { SetValue(PathChooserCommandProperty, value); }
		}
	}
}