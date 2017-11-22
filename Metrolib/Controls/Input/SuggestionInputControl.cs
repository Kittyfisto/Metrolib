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
		///     Definition of the <see cref="Text" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string),
		                                                                                     typeof(SuggestionInputControl),
		                                                                                     new
			                                                                                     FrameworkPropertyMetadata(default(string),
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
		                                                                                          new PropertyMetadata(default
		                                                                                                               (string
		                                                                                                               )));

		/// <summary>
		///     Definition of the <see cref="Suggestions" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SuggestionsProperty = DependencyProperty.Register(
		                                                                                            "Suggestions",
		                                                                                            typeof(IReadOnlyList<
			                                                                                            string>),
		                                                                                            typeof(
			                                                                                            SuggestionInputControl),
		                                                                                            new
			                                                                                            PropertyMetadata(default(
				                                                                                                             IReadOnlyList
				                                                                                                             <string
				                                                                                                             >),
			                                                                                                             OnSuggestionsChanged))
			;

		/// <summary>
		///     Definition of the <see cref="SelectedSuggestions" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SelectedSuggestionsProperty = DependencyProperty.Register(
		                                                                                                    "SelectedSuggestions",
		                                                                                                    typeof(string),
		                                                                                                    typeof(
			                                                                                                    SuggestionInputControl
		                                                                                                    ),
		                                                                                                    new
			                                                                                                    PropertyMetadata(default
			                                                                                                                     (string
			                                                                                                                     )))
			;	

		private Popup _popup;

		static SuggestionInputControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(SuggestionInputControl),
			                                         new FrameworkPropertyMetadata(typeof(SuggestionInputControl)));
		}

		/// <summary>
		///     The selection currently selected, if any.
		/// </summary>
		public string SelectedSuggestions
		{
			get { return (string) GetValue(SelectedSuggestionsProperty); }
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
		public IReadOnlyList<string> Suggestions
		{
			get { return (IReadOnlyList<string>) GetValue(SuggestionsProperty); }
			set { SetValue(SuggestionsProperty, value); }
		}

		private static void OnSuggestionsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((SuggestionInputControl) dependencyObject).OnSuggestionsChanged((IReadOnlyList<string>) args.NewValue);
		}

		private void OnSuggestionsChanged(IReadOnlyList<string> suggestions)
		{
			if (suggestions != null && suggestions.Count > 0)
				_popup.IsOpen = true;
			else
				_popup.IsOpen = false;
		}

		public override void OnApplyTemplate()
		{
			_popup = (Popup) GetTemplateChild(PART_SuggestionPopup);

			base.OnApplyTemplate();
		}
	}
}