using System.Windows;

namespace DocumentationCreator
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