using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// </summary>
	public class AboutControl
		: ContentControl
	{
		private static readonly DependencyPropertyKey ShowCommandPropertyKey
			= DependencyProperty.RegisterReadOnly("ShowCommand", typeof (ICommand), typeof (AboutControl),
			                                      new FrameworkPropertyMetadata(default(ICommand),
			                                                                    FrameworkPropertyMetadataOptions.None));

		public static readonly DependencyProperty IsOpenProperty =
			DependencyProperty.Register("IsOpen", typeof (bool), typeof (AboutControl), new PropertyMetadata(false, OnIsOpenChanged));

		private static void OnIsOpenChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((AboutControl) dependencyObject).OnIsOpenChanged((bool) args.NewValue);
		}

		private void OnIsOpenChanged(bool isOpen)
		{
			if (IsOpen)
			{
				// We want to take away focus from the last focused element
				// so the ugly dotted border disappears.
				Focus();
			}
		}

		public static readonly DependencyProperty ShowCommandProperty
			= ShowCommandPropertyKey.DependencyProperty;

		public static readonly DependencyProperty HorizontalOffsetProperty =
			DependencyProperty.Register("HorizontalOffset", typeof (double), typeof (AboutControl),
			                            new PropertyMetadata(default(double), OnHorizontalOffsetChanged));

		private static readonly DependencyPropertyKey ContentMarginPropertyKey
			= DependencyProperty.RegisterReadOnly("ContentMargin", typeof(Thickness), typeof(AboutControl),
												  new FrameworkPropertyMetadata(default(Thickness),
			                                                                    FrameworkPropertyMetadataOptions.None));

		public static readonly DependencyProperty ContentMarginProperty
			= ContentMarginPropertyKey.DependencyProperty;

		public Thickness ContentMargin
		{
			get { return (Thickness)GetValue(ContentMarginProperty); }
			protected set { SetValue(ContentMarginPropertyKey, value); }
		}

		static AboutControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (AboutControl), new FrameworkPropertyMetadata(typeof (AboutControl)));
		}

		public AboutControl()
		{
			ShowCommand = new DelegateCommand(Show);
		}

		public double HorizontalOffset
		{
			get { return (double) GetValue(HorizontalOffsetProperty); }
			set { SetValue(HorizontalOffsetProperty, value); }
		}

		/// <summary>
		/// </summary>
		public bool IsOpen
		{
			get { return (bool) GetValue(IsOpenProperty); }
			set { SetValue(IsOpenProperty, value); }
		}

		/// <summary>
		///     This command can be used to show this dialog.
		/// </summary>
		public ICommand ShowCommand
		{
			get { return (ICommand) GetValue(ShowCommandProperty); }
			protected set { SetValue(ShowCommandPropertyKey, value); }
		}

		private static void OnHorizontalOffsetChanged(DependencyObject dependencyObject,
		                                              DependencyPropertyChangedEventArgs args)
		{
			((AboutControl) dependencyObject).OnHorizontalOffsetChanged((double) args.NewValue);
		}

		private void OnHorizontalOffsetChanged(double relativeHorizontalOffset)
		{
			var offset = relativeHorizontalOffset*ActualWidth;
			ContentMargin = new Thickness(150 + offset, 0, 0, 0);
		}

		private void Show()
		{
			IsOpen = true;
		}
	}
}