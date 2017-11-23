using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using log4net;

namespace Metrolib.Sample
{
	public sealed class SuggestionsViewModel
		: INotifyPropertyChanged
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public SuggestionsViewModel(ISerialTaskScheduler scheduler, IDispatcher dispatcher)
		{
			if (scheduler == null)
				throw new ArgumentNullException(nameof(scheduler));
			if (dispatcher == null)
				throw new ArgumentNullException(nameof(dispatcher));

			_scheduler = scheduler;
			_dispatcher = dispatcher;
			_allSuggestions = new[]
			{
				"foo",
				"bar",
				"Qwertz",
				"I feel a disturbance in the force",
				"I'm dumb",
				"Rise, Lord Vader!",
				"Yolo"
			};
		}

		private readonly IReadOnlyList<string> _allSuggestions;
		private readonly ISerialTaskScheduler _scheduler;
		private readonly IDispatcher _dispatcher;

		private string _text;
		private IReadOnlyList<string> _suggestions;
		private string _selectedSuggestion;
		private bool _disableSuggestions;

		public string Text
		{
			get { return _text; }
			set
			{
				if (value == _text)
					return;

				_text = value;
				EmitPropertyChanged();

				if (!_disableSuggestions)
				{
					// Finding suggestions for any given search term might take a very long time and thus we
					// should perform the search on a BG thread so the UI isn't blocked...
					_scheduler.StartNew(() => FindSuggestions(value))
					          .ContinueWith(t => _dispatcher.BeginInvoke(() =>
					          {
						          try
						          {
							          Suggestions = t.Result;
						          }
						          catch (Exception e)
						          {
							          Log.ErrorFormat("Caught unexpected exception: {0}", e);
						          }
					          }));
				}
			}
		}

		private IReadOnlyList<string> FindSuggestions(string text)
		{
			// Yes, this doesn't take long in this sample, but I'm too lazy to
			// program a non-silly long-running method
			if (text == null)
				return new string[0];

			return _allSuggestions.Where(x => x.StartsWith(text)).ToList();
		}

		public IReadOnlyList<string> Suggestions
		{
			get { return _suggestions; }
			private set
			{
				_suggestions = value;
				EmitPropertyChanged();
			}
		}

		public string SelectedSuggestion
		{
			get { return _selectedSuggestion; }
			set
			{
				if (value == _selectedSuggestion)
					return;

				_selectedSuggestion = value;
				EmitPropertyChanged();
			}
		}

		public ICommand SelectSuggestionCommand => new DelegateCommand<string>(OnSuggestionChosen);

		public bool IsThinking => true;

		private void OnSuggestionChosen(string suggestion)
		{
			try
			{
				_disableSuggestions = true;
				Text = suggestion;
			}
			finally
			{
				_disableSuggestions = false;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void EmitPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}