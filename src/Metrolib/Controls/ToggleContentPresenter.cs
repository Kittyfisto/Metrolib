using System.Windows;
using System.Windows.Controls;

namespace Metrolib.Controls
{
	/// <summary>
	///     A control which is capable of presenting one of two available contents at a time.
	///     The content which is being presented is configured via the <see cref="ShowSideA"/> property and a nice
	///     "rotating" animation is used to present the new content.
	/// </summary>
	public class ToggleContentPresenter
		: Control
	{
		static ToggleContentPresenter()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleContentPresenter),
			                                         new FrameworkPropertyMetadata(typeof(ToggleContentPresenter)));
		}

		/// <summary>
		/// 
		/// </summary>
		public static readonly DependencyProperty SideAContentProperty = DependencyProperty.Register(
		                                                "SideAContent", typeof(object), typeof(ToggleContentPresenter), new PropertyMetadata(default(object)));

		/// <summary>
		/// 
		/// </summary>
		public static readonly DependencyProperty SideAContentTemplateProperty = DependencyProperty.Register(
		                                                "SideAContentTemplate", typeof(DataTemplate), typeof(ToggleContentPresenter), new PropertyMetadata(default(DataTemplate)));

		/// <summary>
		/// 
		/// </summary>
		public static readonly DependencyProperty SideAContentTemplateSelectorProperty = DependencyProperty.Register(
		                                                "SideAContentTemplateSelector", typeof(DataTemplateSelector), typeof(ToggleContentPresenter), new PropertyMetadata(default(DataTemplateSelector)));

		/// <summary>
		/// 
		/// </summary>
		public static readonly DependencyProperty SideBContentProperty = DependencyProperty.Register(
		                                                "SideBContent", typeof(object), typeof(ToggleContentPresenter), new PropertyMetadata(default(bool)));

		/// <summary>
		/// 
		/// </summary>
		public static readonly DependencyProperty SideBContentTemplateProperty = DependencyProperty.Register(
		                                                                                                     "SideBContentTemplate", typeof(DataTemplate), typeof(ToggleContentPresenter), new PropertyMetadata(default(DataTemplate)));

		/// <summary>
		/// 
		/// </summary>
		public static readonly DependencyProperty SideBContentTemplateSelectorProperty = DependencyProperty.Register(
		                                                "SideBContentTemplateSelector", typeof(DataTemplateSelector), typeof(ToggleContentPresenter), new PropertyMetadata(default(DataTemplateSelector)));

		/// <summary>
		/// 
		/// </summary>
		public static readonly DependencyProperty ShowSideAProperty = DependencyProperty.Register(
		                                                                                             "ShowSideA", typeof(bool), typeof(ToggleContentPresenter), new PropertyMetadata(true, OnShowSideAChanged));

		private static void OnShowSideAChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((ToggleContentPresenter)d).UpdateVisualState();
		}

		/// <inheritdoc />
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			UpdateVisualState(false);
		}

		/// <summary>
		/// The "side a" content which is presented when <see cref="ShowSideA"/> is true.
		/// </summary>
		public object SideAContent
		{
			get { return GetValue(SideAContentProperty); }
			set { SetValue(SideAContentProperty, value); }
		}

		/// <summary>
		/// The data template (if any) to present <see cref="SideAContent"/>.
		/// </summary>
		public DataTemplate SideAContentTemplate
		{
			get { return (DataTemplate) GetValue(SideAContentTemplateProperty); }
			set { SetValue(SideAContentTemplateProperty, value); }
		}

		/// <summary>
		/// The data template selector (if any) which is responsible for selecting a fitting template to present <see cref="SideAContent"/>.
		/// </summary>
		public DataTemplateSelector SideAContentTemplateSelector
		{
			get { return (DataTemplateSelector) GetValue(SideAContentTemplateSelectorProperty); }
			set { SetValue(SideAContentTemplateSelectorProperty, value); }
		}

		/// <summary>
		/// The "side b" content which is presented when <see cref="ShowSideA"/> is false.
		/// </summary>
		public object SideBContent
		{
			get { return GetValue(SideBContentProperty); }
			set { SetValue(SideBContentProperty, value); }
		}

		/// <summary>
		/// The data template (if any) to present <see cref="SideBContent"/>.
		/// </summary>
		public DataTemplate SideBContentTemplate
		{
			get { return (DataTemplate) GetValue(SideBContentTemplateProperty); }
			set { SetValue(SideBContentTemplateProperty, value); }
		}

		/// <summary>
		/// The data template selector (if any) which is responsible for selecting a fitting template to present <see cref="SideBContent"/>.
		/// </summary>
		public DataTemplateSelector SideBContentTemplateSelector
		{
			get { return (DataTemplateSelector) GetValue(SideBContentTemplateSelectorProperty); }
			set { SetValue(SideBContentTemplateSelectorProperty, value); }
		}

		/// <summary>
		/// When set to true, <see cref="SideAContent"/> is presented, otherwise <see cref="SideBContent"/> is.
		/// </summary>
		public bool ShowSideA
		{
			get { return (bool) GetValue(ShowSideAProperty); }
			set { SetValue(ShowSideAProperty, value); }
		}

		private void UpdateVisualState(bool useTransition = true)
		{
			VisualStateManager.GoToState(this, ShowSideA ? "ShowSideAState" : "ShowSideBState", useTransition);
		}
	}
}