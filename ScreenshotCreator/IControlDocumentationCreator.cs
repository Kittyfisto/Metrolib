using System.Windows;

namespace ScreenshotCreator
{
	public interface IControlDocumentationCreator
	{
		void SaveAllPoses(string basePath);
	}

	public interface IControlDocumentationCreator<out T>
		: IControlDocumentationCreator
		where T : FrameworkElement
	{
		IControlExampleCreator<T> AddExample(string name);
	}
}