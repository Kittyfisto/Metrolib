using System;

// ReSharper disable once CheckNamespace
namespace Metrolib
{
	/// <summary>
	///     Provides extension methods for the <see cref="Version" /> class.
	/// </summary>
	public static class VersionExtensions
	{
		/// <summary>
		///     Produces a humna readable string of this version.
		/// </summary>
		/// <param name="version"></param>
		/// <returns></returns>
		public static string Format(this Version version)
		{
			var value = version.ToString(3);
			return value;
		}
	}
}