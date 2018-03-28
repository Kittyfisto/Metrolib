using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace ScreenshotCreator
{
	/// <summary>
	///     Responsible for creating documentation for a library.
	/// </summary>
	public sealed class DocumentationCreator
	{
		private readonly Dispatcher _dispatcher;
		private readonly ResourceDictionary _resourceDictionary;
		private readonly List<IControlDocumentationCreator> _controlDocumentationCreators;
		private readonly AssemblyDocumentationReader _assemblyDocumentationReader;

		public DocumentationCreator(Dispatcher dispatcher, ResourceDictionary resourceDictionary, Assembly assembly)
		{
			if (dispatcher == null)
				throw new ArgumentNullException(nameof(dispatcher));
			if (resourceDictionary == null)
				throw new ArgumentNullException(nameof(resourceDictionary));
			if (assembly == null)
				throw new ArgumentNullException(nameof(assembly));

			_dispatcher = dispatcher;
			_resourceDictionary = resourceDictionary;
			_controlDocumentationCreators = new List<IControlDocumentationCreator>();
			_assemblyDocumentationReader = new AssemblyDocumentationReader(assembly);
		}

		/// <summary>
		///     Returns an object with which the documentation for a particular control can be created.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public IControlDocumentationCreator<T> CreateDocumentationFor<T>() where T : FrameworkElement, new()
		{
			var creator = new ControlDocumentationCreator<T>(_dispatcher, _resourceDictionary, _assemblyDocumentationReader);
			_controlDocumentationCreators.Add(creator);
			return creator;
		}
	}
}