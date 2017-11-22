using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A control which allows the user to input single-line text
	///     which then displays a list of suggesstions from which the user
	///     can select some, or continue typing...
	/// </summary>
	[TemplatePart(Name = PART_SuggestionPopup, Type = typeof(Popup))]
	public sealed class SuggestionInputControl
		: Control
	{
		/// <summary>
		/// </summary>
		public const string PART_SuggestionPopup = "PART_SuggestionPopup";

		/// <summary>
		///     Definition of the <see cref="SuggestionTemplate" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SuggestionTemplateProperty = DependencyProperty.Register(
		                                                                                                   "SuggestionTemplate",
		                                                                                                   typeof(DataTemplate
		                                                                                                   ),
		                                                                                                   typeof(
			                                                                                                   SuggestionInputControl
		                                                                                                   ),
		                                                                                                   new
			                                                                                                   PropertyMetadata(propertyChangedCallback
			                                                                                                                    : null))
			;

		/// <summary>
		///     Definition of the <see cref="Text" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string),
		                                                                                     typeof(SuggestionInputControl),
		                                                                                     new
			                                                                                     FrameworkPropertyMetadata(defaultValue: null,
			                                                                                                               flags:
			                                                                                                               FrameworkPropertyMetadataOptions
				                                                                                                               .BindsTwoWayByDefault))
			;

		/// <summary>
		///     Definition of the <see cref="Watermark" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register(
		                                                                                          "Watermark", typeof(string),
		                                                                                          typeof(
			                                                                                          SuggestionInputControl),
		                                                                                          new
			                                                                                          PropertyMetadata(propertyChangedCallback
			                                                                                                           : null));

		/// <summary>
		///     Definition of the <see cref="Suggestions" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SuggestionsProperty = DependencyProperty.Register(
		                                                                                            "Suggestions",
		                                                                                            typeof(IReadOnlyList<
			                                                                                            object>),
		                                                                                            typeof(
			                                                                                            SuggestionInputControl),
		                                                                                            new
			                                                                                            PropertyMetadata(defaultValue: null,
			                                                                                                             propertyChangedCallback
			                                                                                                             : OnSuggestionsChanged))
			;

		/// <summary>
		///     Definition of the <see cref="SelectedSuggestions" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SelectedSuggestionsProperty = DependencyProperty.Register(
		                                                                                                    "SelectedSuggestions",
		                                                                                                    typeof(object),
		                                                                                                    typeof(
			                                                                                                    SuggestionInputControl
		                                                                                                    ),
		                                                                                                    new
			                                                                                                    PropertyMetadata(propertyChangedCallback
			                                                                                                                     : null))
			;

		/// <summary>
		///     Definition of the <see cref="SuggestionTemplateSelector" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SuggestionTemplateSelectorProperty = DependencyProperty.Register(
		                                                                                                           "SuggestionTemplateSelector",
		                                                                                                           typeof(
			                                                                                                           DataTemplateSelector
		                                                                                                           ),
		                                                                                                           typeof(
			                                                                                                           SuggestionInputControl
		                                                                                                           ),
		                                                                                                           new
			                                                                                                           PropertyMetadata(propertyChangedCallback
			                                                                                                                            : null))
			;

		private Popup _popup;

		static SuggestionInputControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(SuggestionInputControl),
			                                         new FrameworkPropertyMetadata(typeof(SuggestionInputControl)));
		}

		/// <summary>
		///     The template with which a suggestions is displayed.
		/// </summary>
		public DataTemplate SuggestionTemplate
		{
			get { return (DataTemplate) GetValue(SuggestionTemplateProperty); }
			set { SetValue(SuggestionTemplateProperty, value); }
		}

		/// <summary>
		///     The template selector which can provide individual data templates for each suggestions.
		/// </summary>
		public DataTemplateSelector SuggestionTemplateSelector
		{
			get { return (DataTemplateSelector) GetValue(SuggestionTemplateSelectorProperty); }
			set { SetValue(SuggestionTemplateSelectorProperty, value); }
		}

		/// <summary>
		///     The selection currently selected, if any.
		/// </summary>
		public object SelectedSuggestions
		{
			get { return GetValue(SelectedSuggestionsProperty); }
			set { SetValue(SelectedSuggestionsProperty, value); }
		}

		/// <summary>
		///     The watermark being displayed while no text has been entered.
		/// </summary>
		public string Watermark
		{
			get { return (string) GetValue(WatermarkProperty); }
			set { SetValue(WatermarkProperty, value); }
		}

		/// <summary>
		///     The text displayed (likely entered by the user).
		/// </summary>
		public string Text
		{
			get { return (string) GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		/// <summary>
		///     The list of suggestions to present to the user.
		/// </summary>
		public IReadOnlyList<object> Suggestions
		{
			get { return (IReadOnlyList<object>) GetValue(SuggestionsProperty); }
			set { SetValue(SuggestionsProperty, value); }
		}

		private static void OnSuggestionsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((SuggestionInputControl) dependencyObject).OnSuggestionsChanged((IReadOnlyList<object>) args.NewValue);
		}

		private void OnSuggestionsChanged(IReadOnlyList<object> suggestions)
		{
			if (suggestions != null && suggestions.Count > 0)
				_popup.IsOpen = true;
			else
				_popup.IsOpen = false;
		}

		/// <inheritdoc />
		public override void OnApplyTemplate()
		{
			_popup = (Popup) GetTemplateChild(PART_SuggestionPopup);

			base.OnApplyTemplate();
		}
	}
}