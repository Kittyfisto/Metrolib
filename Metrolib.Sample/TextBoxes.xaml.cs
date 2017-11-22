using System.Threading;
using System.Windows.Threading;

namespace Metrolib.Sample
{
	public partial class TextBoxes
	{
		public SuggestionsViewModel SuggestionsViewModel { get; set; }

		public TextBoxes()
		{
			SuggestionsViewModel = new SuggestionsViewModel(new SerialTaskScheduler(),
			                                                new UiDispatcher(Dispatcher.CurrentDispatcher));

			InitializeComponent();
		}
	}
}