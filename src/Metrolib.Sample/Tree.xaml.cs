using System.Collections.Generic;

namespace Metrolib.Sample
{
	/// <summary>
	///     Interaction logic for Tree.xaml
	/// </summary>
	public partial class Tree
	{
		private readonly List<Format> _items;

		public Tree()
		{
			_items = new List<Format>
				{
					new Format("Movies")
						{
							Releases =
								{
									new Release("Avengers"),
									new Release("Avengers - Age of Ultron"),
									new Release("Captain America - Civil War")
								}
						},
					new Format("Series")
						{
							Releases =
								{
									new Release("Luke Cage"),
									new Release("Jessica Jones"),
									new Release("Daredevil")
								}
						}
				};

			InitializeComponent();
		}

		public IEnumerable<Format> Items
		{
			get { return _items; }
		}
	}

	public class Format
	{
		private readonly string _name;
		private readonly List<Release> _releases;

		public Format(string name)
		{
			_name = name;
			_releases = new List<Release>();
		}

		public List<Release> Releases {get { return _releases; }}

		public string Name
		{
			get { return _name; }
		}
	}

	public class Release
	{
		private readonly string _name;

		public Release(string name)
		{
			_name = name;
		}

		public string Name
		{
			get { return _name; }
		}
	}
}