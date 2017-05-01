namespace Metrolib
{
	/// <summary>
	/// 
	/// </summary>
	public static class Icons
	{
		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry Pen;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry Email;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry EmailOpen;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ChevronDown;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ChevronUp;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ViewDashboard;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ViewQuilt;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ViewGrid;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ViewList;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ViewHeadline;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ViewModule;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ViewColumn;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ViewSequential;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ViewDay;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ViewWeek;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry Eye;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry EyeOff;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry Add;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry Delete;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry FileExport;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry WindowMinimize;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry WindowMaximize;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry WindowRestore;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry DotsVertical;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry Refresh;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry Remove;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry Magnify;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry Undo;

		static Icons()
		{
			Add = CreateGeometry("M19,13H13V19H11V13H5V11H11V5H13V11H19V13Z");
			Delete = CreateGeometry("M19,4H15.5L14.5,3H9.5L8.5,4H5V6H19M6,19A2,2 0 0,0 8,21H16A2,2 0 0,0 18,19V7H6V19Z");
			Pen = CreateGeometry("M20.71,7.04C21.1,6.65 21.1,6 20.71,5.63L18.37,3.29C18,2.9 17.35,2.9 16.96,3.29L15.12,5.12L18.87,8.87M3,17.25V21H6.75L17.81,9.93L14.06,6.18L3,17.25Z");
			Email = CreateGeometry("M20,8L12,13L4,8V6L12,11L20,6M20,4H4C2.89,4 2,4.89 2,6V18A2,2 0 0,0 4,20H20A2,2 0 0,0 22,18V6C22,4.89 21.1,4 20,4Z");
			EmailOpen = CreateGeometry("M4,8L12,13L20,8V8L12,3L4,8V8M22,8V18A2,2 0 0,1 20,20H4A2,2 0 0,1 2,18V8C2,7.27 2.39,6.64 2.97,6.29L12,0.64L21.03,6.29C21.61,6.64 22,7.27 22,8Z");
			ChevronDown = CreateGeometry("M7.41,8.58L12,13.17L16.59,8.58L18,10L12,16L6,10L7.41,8.58Z");
			ChevronUp = CreateGeometry("M7.41,15.41L12,10.83L16.59,15.41L18,14L12,8L6,14L7.41,15.41Z");

			ViewList = CreateGeometry("M9,5V9H21V5M9,19H21V15H9M9,14H21V10H9M4,9H8V5H4M4,19H8V15H4M4,14H8V10H4V14Z");
			ViewGrid = CreateGeometry("M3,11H11V3H3M3,21H11V13H3M13,21H21V13H13M13,3V11H21V3");
			ViewDashboard = CreateGeometry("M13,3V9H21V3M13,21H21V11H13M3,21H11V15H3M3,13H11V3H3V13Z");
			ViewQuilt = CreateGeometry("M10,5V11H21V5M16,18H21V12H16M4,18H9V5H4M10,18H15V12H10V18Z");
			ViewHeadline = CreateGeometry("M4,5V7H21V5M4,11H21V9H4M4,19H21V17H4M4,15H21V13H4V15Z");
			ViewModule = CreateGeometry("M16,5V11H21V5M10,11H15V5H10M16,18H21V12H16M10,18H15V12H10M4,18H9V12H4M4,11H9V5H4V11Z");
			ViewColumn = CreateGeometry("M16,5V18H21V5M4,18H9V5H4M10,18H15V5H10V18Z");
			ViewSequential = CreateGeometry("M3,4H21V8H3V4M3,10H21V14H3V10M3,16H21V20H3V16Z");
			ViewDay = CreateGeometry("M2,3V6H21V3M20,8H3A1,1 0 0,0 2,9V15A1,1 0 0,0 3,16H20A1,1 0 0,0 21,15V9A1,1 0 0,0 20,8M2,21H21V18H2V21Z");
			ViewWeek = CreateGeometry("M13,5H10A1,1 0 0,0 9,6V18A1,1 0 0,0 10,19H13A1,1 0 0,0 14,18V6A1,1 0 0,0 13,5M20,5H17A1,1 0 0,0 16,6V18A1,1 0 0,0 17,19H20A1,1 0 0,0 21,18V6A1,1 0 0,0 20,5M6,5H3A1,1 0 0,0 2,6V18A1,1 0 0,0 3,19H6A1,1 0 0,0 7,18V6A1,1 0 0,0 6,5Z");

			Eye = CreateGeometry("M12,9A3,3 0 0,0 9,12A3,3 0 0,0 12,15A3,3 0 0,0 15,12A3,3 0 0,0 12,9M12,17A5,5 0 0,1 7,12A5,5 0 0,1 12,7A5,5 0 0,1 17,12A5,5 0 0,1 12,17M12,4.5C7,4.5 2.73,7.61 1,12C2.73,16.39 7,19.5 12,19.5C17,19.5 21.27,16.39 23,12C21.27,7.61 17,4.5 12,4.5Z");
			EyeOff = CreateGeometry("M11.83,9L15,12.16C15,12.11 15,12.05 15,12A3,3 0 0,0 12,9C11.94,9 11.89,9 11.83,9M7.53,9.8L9.08,11.35C9.03,11.56 9,11.77 9,12A3,3 0 0,0 12,15C12.22,15 12.44,14.97 12.65,14.92L14.2,16.47C13.53,16.8 12.79,17 12,17A5,5 0 0,1 7,12C7,11.21 7.2,10.47 7.53,9.8M2,4.27L4.28,6.55L4.73,7C3.08,8.3 1.78,10 1,12C2.73,16.39 7,19.5 12,19.5C13.55,19.5 15.03,19.2 16.38,18.66L16.81,19.08L19.73,22L21,20.73L3.27,3M12,7A5,5 0 0,1 17,12C17,12.64 16.87,13.26 16.64,13.82L19.57,16.75C21.07,15.5 22.27,13.86 23,12C21.27,7.61 17,4.5 12,4.5C10.6,4.5 9.26,4.75 8,5.2L10.17,7.35C10.74,7.13 11.35,7 12,7Z");
			FileExport = CreateGeometry("M6,2C4.89,2 4,2.9 4,4V20A2,2 0 0,0 6,22H18A2,2 0 0,0 20,20V8L14,2M13,3.5L18.5,9H13M8.93,12.22H16V19.29L13.88,17.17L11.05,20L8.22,17.17L11.05,14.35");
			WindowMinimize = CreateGeometry("M20,14H4V10H20");
			WindowMaximize = CreateGeometry("M4,4H20V20H4V4M6,8V18H18V8H6Z");
			WindowRestore = CreateGeometry("M4,8H8V4H20V16H16V20H4V8M16,8V14H18V6H10V8H16M6,12V18H14V12H6Z");
			DotsVertical = CreateGeometry("M12,16A2,2 0 0,1 14,18A2,2 0 0,1 12,20A2,2 0 0,1 10,18A2,2 0 0,1 12,16M12,10A2,2 0 0,1 14,12A2,2 0 0,1 12,14A2,2 0 0,1 10,12A2,2 0 0,1 12,10M12,4A2,2 0 0,1 14,6A2,2 0 0,1 12,8A2,2 0 0,1 10,6A2,2 0 0,1 12,4Z");
			Refresh = CreateGeometry("M17.65,6.35C16.2,4.9 14.21,4 12,4A8,8 0 0,0 4,12A8,8 0 0,0 12,20C15.73,20 18.84,17.45 19.73,14H17.65C16.83,16.33 14.61,18 12,18A6,6 0 0,1 6,12A6,6 0 0,1 12,6C13.66,6 15.14,6.69 16.22,7.78L13,11H20V4L17.65,6.35Z");
			Remove = CreateGeometry("M13.46,12L19,17.54V19H17.54L12,13.46L6.46,19H5V17.54L10.54,12L5,6.46V5H6.46L12,10.54L17.54,5H19V6.46L13.46,12Z");
			Magnify = CreateGeometry("M9.5,3A6.5,6.5 0 0,1 16,9.5C16,11.11 15.41,12.59 14.44,13.73L14.71,14H15.5L20.5,19L19,20.5L14,15.5V14.71L13.73,14.44C12.59,15.41 11.11,16 9.5,16A6.5,6.5 0 0,1 3,9.5A6.5,6.5 0 0,1 9.5,3M9.5,5C7,5 5,7 5,9.5C5,12 7,14 9.5,14C12,14 14,12 14,9.5C14,7 12,5 9.5,5Z");
			Undo = CreateGeometry("M13.5,7A6.5,6.5 0 0,1 20,13.5A6.5,6.5 0 0,1 13.5,20H10V18H13.5C16,18 18,16 18,13.5C18,11 16,9 13.5,9H7.83L10.91,12.09L9.5,13.5L4,8L9.5,2.5L10.92,3.91L7.83,7H13.5M6,18H8V20H6V18Z");
			
		}

		private static System.Windows.Media.Geometry CreateGeometry(string source)
		{
			var geometry = System.Windows.Media.Geometry.Parse(source);
			geometry.Freeze();
			return geometry;
		}
	}
}