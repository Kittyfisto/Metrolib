using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Base class for progress bars of this library.
	/// </summary>
	public abstract class AbstractProgressBar
		: ProgressBar
	{
		private static readonly DependencyPropertyKey RelativeValuePropertyKey
			= DependencyProperty.RegisterReadOnly("RelativeValue", typeof (double), typeof (AbstractProgressBar),
			                                      new FrameworkPropertyMetadata(default(double),
			                                                                    FrameworkPropertyMetadataOptions.None));

		static AbstractProgressBar()
		{
			MinimumProperty.OverrideMetadata(typeof(AbstractProgressBar),
				new FrameworkPropertyMetadata(0.0, OnMinimumChanged));
			MaximumProperty.OverrideMetadata(typeof(AbstractProgressBar),
				new FrameworkPropertyMetadata(100.0, OnMaximumChanged));
		}

		private static void OnMinimumChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((AbstractProgressBar)dependencyObject).OnMinimumChanged((double)args.NewValue);
		}

		private void OnMinimumChanged(double newValue)
		{
			UpdateRelativeValue(newValue, Maximum, Value);
		}

		private static void OnMaximumChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((AbstractProgressBar)dependencyObject).OnMaximumChanged((double)args.NewValue);
		}

		private void OnMaximumChanged(double newValue)
		{
			UpdateRelativeValue(Minimum, newValue, Value);
		}

		/// <summary>
		///     Initializes this object.
		/// </summary>
		protected AbstractProgressBar()
		{
			ValueChanged += OnValueChanged;
		}

		private void OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> args)
		{
			UpdateRelativeValue(Minimum, Maximum, args.NewValue);
		}

		private void UpdateRelativeValue(double minimum, double maximum, double value)
		{
			if (maximum != minimum)
			{
				RelativeValue = (value - minimum) / (maximum - minimum);
			}
			else
			{
				RelativeValue = 0;
			}
		}

		/// <summary>
		///     Definition of the <see cref="RelativeValue" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty RelativeValueProperty
			= RelativeValuePropertyKey.DependencyProperty;

		/// <summary>
		///     The <see cref="ProgressBar.Value" />, scaled between [0, 1] with respect
		///     to <see cref="ProgressBar.Minimum" /> and <see cref="ProgressBar.Maximum" />.
		/// </summary>
		public double RelativeValue
		{
			get { return (double) GetValue(RelativeValueProperty); }
			protected set { SetValue(RelativeValuePropertyKey, value); }
		}
	}
}