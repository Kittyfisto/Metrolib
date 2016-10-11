using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A control meant to inform the user about the application, for example:
	///     - Who wrote it
	///     - Which version is running
	///     - What dependencies does it use
	///     - etc...
	/// </summary>
	public class PageOverlay
		: HeaderedContentControl
	{
		private static readonly DependencyPropertyKey ShowCommandPropertyKey
			= DependencyProperty.RegisterReadOnly("ShowCommand", typeof (ICommand), typeof (PageOverlay),
			                                      new FrameworkPropertyMetadata(default(ICommand),
			                                                                    FrameworkPropertyMetadataOptions.None));

		/// <summary>
		///     Definition of the <see cref="IsOpen" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IsOpenProperty =
			DependencyProperty.Register("IsOpen", typeof (bool), typeof (PageOverlay),
			                            new PropertyMetadata(false, OnIsOpenChanged));

		/// <summary>
		///     Definition of the <see cref="ShowCommand" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ShowCommandProperty
			= ShowCommandPropertyKey.DependencyProperty;

		/// <summary>
		///     Definition of the <see cref="HorizontalOffset" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty HorizontalOffsetProperty =
			DependencyProperty.Register("HorizontalOffset", typeof (double), typeof (PageOverlay),
			                            new PropertyMetadata(default(double), OnHorizontalOffsetChanged));

		private static readonly DependencyPropertyKey ContentMarginPropertyKey
			= DependencyProperty.RegisterReadOnly("ContentMargin", typeof (Thickness), typeof (PageOverlay),
			                                      new FrameworkPropertyMetadata(default(Thickness),
			                                                                    FrameworkPropertyMetadataOptions.None));

		/// <summary>
		///     Definition of the <see cref="ContentMargin" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ContentMarginProperty
			= ContentMarginPropertyKey.DependencyProperty;

		static PageOverlay()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (PageOverlay), new FrameworkPropertyMetadata(typeof (PageOverlay)));
		}

		/// <summary>
		///     Initializes this object.
		/// </summary>
		public PageOverlay()
		{
			ShowCommand = new DelegateCommand(Show);
		}

		/// <summary>
		///     The margin used to animate the slide-in effect when the control is opened.
		/// </summary>
		public Thickness ContentMargin
		{
			get { return (Thickness) GetValue(ContentMarginProperty); }
			protected set { SetValue(ContentMarginPropertyKey, value); }
		}

		/// <summary>
		///     The horizontal offset used to animate the slide-in effect when the control is opened.
		/// </summary>
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

		private static void OnIsOpenChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((PageOverlay) dependencyObject).OnIsOpenChanged((bool) args.NewValue);
		}

		private void OnIsOpenChanged(bool isOpen)
		{
			if (isOpen)
			{
				// We want to take away focus from the last focused element
				// so the ugly dotted border disappears.
				Focus();
			}
		}

		private static void OnHorizontalOffsetChanged(DependencyObject dependencyObject,
		                                              DependencyPropertyChangedEventArgs args)
		{
			((PageOverlay) dependencyObject).OnHorizontalOffsetChanged((double) args.NewValue);
		}

		private void OnHorizontalOffsetChanged(double relativeHorizontalOffset)
		{
			double offset = relativeHorizontalOffset*ActualWidth;
			ContentMargin = new Thickness(150 + offset, 0, 0, 0);
		}

		private void Show()
		{
			IsOpen = true;
		}
	}
}