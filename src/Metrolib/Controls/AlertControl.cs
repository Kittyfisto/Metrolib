using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Metrolib.Controls
{
	/// <summary>
	///     This control is used to inform the user about the occurence of an unhandled exception.
	///     Can be used to let the user decide what should happen next.
	/// </summary>
	public class AlertControl : Control
	{
		/// <summary>
		///     Definition of the <see cref="Exception" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ExceptionProperty =
			DependencyProperty.Register("Exception", typeof (Exception), typeof (AlertControl),
			                            new PropertyMetadata(default(Exception), OnExceptionChanged));

		/// <summary>
		///     Definition of the <see cref="ExceptionType" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ExceptionTypeProperty =
			DependencyProperty.Register("ExceptionType", typeof (Type), typeof (AlertControl),
			                            new PropertyMetadata(default(Type)));

		/// <summary>
		///     Definition of the <see cref="CloseCommand" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty CloseCommandProperty =
			DependencyProperty.Register("CloseCommand", typeof (ICommand), typeof (AlertControl),
			                            new PropertyMetadata(default(ICommand)));

		static AlertControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (AlertControl), new FrameworkPropertyMetadata(typeof (AlertControl)));
		}

		/// <summary>
		///     The command that is executed when the user requests that this dialog be closed.
		/// </summary>
		public ICommand CloseCommand
		{
			get { return (ICommand) GetValue(CloseCommandProperty); }
			set { SetValue(CloseCommandProperty, value); }
		}

		/// <summary>
		///     The type of the exception that was encountered.
		/// </summary>
		public Type ExceptionType
		{
			get { return (Type) GetValue(ExceptionTypeProperty); }
			set { SetValue(ExceptionTypeProperty, value); }
		}

		/// <summary>
		///     The exception that was encountered.
		/// </summary>
		public Exception Exception
		{
			get { return (Exception) GetValue(ExceptionProperty); }
			set { SetValue(ExceptionProperty, value); }
		}

		private static void OnExceptionChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((AlertControl) dependencyObject).OnExceptionChanged((Exception) args.NewValue);
		}

		private void OnExceptionChanged(Exception exception)
		{
			ExceptionType = exception != null ? exception.GetType() : null;
		}
	}
}