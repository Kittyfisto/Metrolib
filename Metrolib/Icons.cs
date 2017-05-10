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

		#region View

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
		public static readonly System.Windows.Media.Geometry Widgets;

		#endregion

		#region Bookmark

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry Bookmark;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry BookmarkCheck;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry BookmarkOutline;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry BookmarkPlus;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry BookmarkPlusOutline;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry BookmarkRemove;

		#endregion

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

		#region Filter

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry Filter;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry FilterOutline;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry FilterRemove;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry FilterRemoveOutline;

		#endregion

		#region File

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry FileExport;

		#endregion

		#region Charts

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ChartAreaSpline;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ChartBar;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ChartBarStacked;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ChartGantt;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ChartHistogram;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ChartLine;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ChartLineStacked;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ChartPie;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ChartScatterplotHexabin;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ChartTimeline;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ChartArc;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ChartBubble;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ChartDonut;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry ChartDonutVariant;

		#endregion

		#region Database

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry Database;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry DatabaseMinus;

		/// <summary>
		/// 
		/// </summary>
		public static readonly System.Windows.Media.Geometry DatabasePlus;

		#endregion

		static Icons()
		{
			Add = CreateGeometry("M19,13H13V19H11V13H5V11H11V5H13V11H19V13Z");
			Delete = CreateGeometry("M19,4H15.5L14.5,3H9.5L8.5,4H5V6H19M6,19A2,2 0 0,0 8,21H16A2,2 0 0,0 18,19V7H6V19Z");
			Pen = CreateGeometry("M20.71,7.04C21.1,6.65 21.1,6 20.71,5.63L18.37,3.29C18,2.9 17.35,2.9 16.96,3.29L15.12,5.12L18.87,8.87M3,17.25V21H6.75L17.81,9.93L14.06,6.18L3,17.25Z");
			Email = CreateGeometry("M20,8L12,13L4,8V6L12,11L20,6M20,4H4C2.89,4 2,4.89 2,6V18A2,2 0 0,0 4,20H20A2,2 0 0,0 22,18V6C22,4.89 21.1,4 20,4Z");
			EmailOpen = CreateGeometry("M4,8L12,13L20,8V8L12,3L4,8V8M22,8V18A2,2 0 0,1 20,20H4A2,2 0 0,1 2,18V8C2,7.27 2.39,6.64 2.97,6.29L12,0.64L21.03,6.29C21.61,6.64 22,7.27 22,8Z");
			ChevronDown = CreateGeometry("M7.41,8.58L12,13.17L16.59,8.58L18,10L12,16L6,10L7.41,8.58Z");
			ChevronUp = CreateGeometry("M7.41,15.41L12,10.83L16.59,15.41L18,14L12,8L6,14L7.41,15.41Z");

			Bookmark = CreateGeometry("M17,3H7A2,2 0 0,0 5,5V21L12,18L19,21V5C19,3.89 18.1,3 17,3Z");
			BookmarkCheck = CreateGeometry("M17,3A2,2 0 0,1 19,5V21L12,18L5,21V5C5,3.89 5.9,3 7,3H17M11,14L17.25,7.76L15.84,6.34L11,11.18L8.41,8.59L7,10L11,14Z");
			BookmarkOutline = CreateGeometry("M17,18L12,15.82L7,18V5H17M17,3H7A2,2 0 0,0 5,5V21L12,18L19,21V5C19,3.89 18.1,3 17,3Z");
			BookmarkPlus = CreateGeometry("M17,3A2,2 0 0,1 19,5V21L12,18L5,21V5C5,3.89 5.9,3 7,3H17M11,7V9H9V11H11V13H13V11H15V9H13V7H11Z");
			BookmarkPlusOutline = CreateGeometry("M17,18V5H7V18L12,15.82L17,18M17,3A2,2 0 0,1 19,5V21L12,18L5,21V5C5,3.89 5.9,3 7,3H17M11,7H13V9H15V11H13V13H11V11H9V9H11V7Z");
			BookmarkRemove = CreateGeometry("M17,3A2,2 0 0,1 19,5V21L12,18L5,21V5C5,3.89 5.9,3 7,3H17M8.17,8.58L10.59,11L8.17,13.41L9.59,14.83L12,12.41L14.41,14.83L15.83,13.41L13.41,11L15.83,8.58L14.41,7.17L12,9.58L9.59,7.17L8.17,8.58Z");

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
			Widgets = CreateGeometry("M3,3H11V7.34L16.66,1.69L22.31,7.34L16.66,13H21V21H13V13H16.66L11,7.34V11H3V3M3,13H11V21H3V13Z");

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

			Filter = CreateGeometry("M3,2H21V2H21V4H20.92L14,10.92V22.91L10,18.91V10.91L3.09,4H3V2Z");
			FilterOutline = CreateGeometry("M3,2H21V2H21V4H20.92L15,9.92V22.91L9,16.91V9.91L3.09,4H3V2M11,16.08L13,18.08V9H13.09L18.09,4H5.92L10.92,9H11V16.08Z");
			FilterRemove = CreateGeometry("M14.76,20.83L17.6,18L14.76,15.17L16.17,13.76L19,16.57L21.83,13.76L23.24,15.17L20.43,18L23.24,20.83L21.83,22.24L19,19.4L16.17,22.24L14.76,20.83M2,2H20V2H20V4H19.92L13,10.92V22.91L9,18.91V10.91L2.09,4H2V2Z");
			FilterRemoveOutline = CreateGeometry("M14.73,20.83L17.58,18L14.73,15.17L16.15,13.76L19,16.57L21.8,13.76L23.22,15.17L20.41,18L23.22,20.83L21.8,22.24L19,19.4L16.15,22.24L14.73,20.83M2,2H20V2H20V4H19.92L14,9.92V22.91L8,16.91V9.91L2.09,4H2V2M10,16.08L12,18.08V9H12.09L17.09,4H4.92L9.92,9H10V16.08Z");

			ChartAreaSpline = CreateGeometry("M17.45,15.18L22,7.31V19L22,21H2V3H4V15.54L9.5,6L16,9.78L20.24,2.45L21.97,3.45L16.74,12.5L10.23,8.75L4.31,19H6.57L10.96,11.44L17.45,15.18Z");
			ChartBar = CreateGeometry("M22,21H2V3H4V19H6V10H10V19H12V6H16V19H18V14H22V21Z");
			ChartBarStacked = CreateGeometry("M22,21H2V3H4V19H6V17H10V19H12V16H16V19H18V17H22V21M18,14H22V16H18V14M12,6H16V9H12V6M16,15H12V10H16V15M6,10H10V12H6V10M10,16H6V13H10V16Z");
			ChartGantt = CreateGeometry("M2,5H10V2H12V22H10V18H6V15H10V13H4V10H10V8H2V5M14,5H17V8H14V5M14,10H19V13H14V10M14,15H22V18H14V15Z");
			ChartHistogram = CreateGeometry("M3,3H5V13H9V7H13V11H17V15H21V21H3V3Z");
			ChartLine = CreateGeometry("M16,11.78L20.24,4.45L21.97,5.45L16.74,14.5L10.23,10.75L5.46,19H22V21H2V3H4V17.54L9.5,8L16,11.78Z");
			ChartLineStacked = CreateGeometry("M17.45,15.18L22,6.81V19L22,21H2V3H4V15.54L4,19H4.31L6,19H6.57L10.96,11.44L17.45,15.18M22,3L21.97,3.45L17,11L10,6L6,12V3H22Z");
			ChartPie = CreateGeometry("M21,11H13V3A8,8 0 0,1 21,11M19,13C19,15.78 17.58,18.23 15.43,19.67L11.58,13H19M11,21C8.22,21 5.77,19.58 4.33,17.43L10.82,13.68L14.56,20.17C13.5,20.7 12.28,21 11,21M3,13A8,8 0 0,1 11,5V12.42L3.83,16.56C3.3,15.5 3,14.28 3,13Z");
			ChartScatterplotHexabin = CreateGeometry("M2,2H4V20H22V22H2V2M14,14.5L12,18H7.94L5.92,14.5L7.94,11H12L14,14.5M14.08,6.5L12.06,10H8L6,6.5L8,3H12.06L14.08,6.5M21.25,10.5L19.23,14H15.19L13.17,10.5L15.19,7H19.23L21.25,10.5Z");
			ChartTimeline = CreateGeometry("M2,2H4V20H22V22H2V2M7,10H17V13H7V10M11,15H21V18H11V15M6,4H22V8H20V6H8V8H6V4Z");
			ChartArc = CreateGeometry("M16.18,19.6L14.17,16.12C15.15,15.4 15.83,14.28 15.97,13H20C19.83,15.76 18.35,18.16 16.18,19.6M13,7.03V3C17.3,3.26 20.74,6.7 21,11H16.97C16.74,8.91 15.09,7.26 13,7.03M7,12.5C7,13.14 7.13,13.75 7.38,14.3L3.9,16.31C3.32,15.16 3,13.87 3,12.5C3,7.97 6.54,4.27 11,4V8.03C8.75,8.28 7,10.18 7,12.5M11.5,21C8.53,21 5.92,19.5 4.4,17.18L7.88,15.17C8.7,16.28 10,17 11.5,17C12.14,17 12.75,16.87 13.3,16.62L15.31,20.1C14.16,20.68 12.87,21 11.5,21Z");
			ChartBubble = CreateGeometry("M7.2,11.2C8.97,11.2 10.4,12.63 10.4,14.4C10.4,16.17 8.97,17.6 7.2,17.6C5.43,17.6 4,16.17 4,14.4C4,12.63 5.43,11.2 7.2,11.2M14.8,16A2,2 0 0,1 16.8,18A2,2 0 0,1 14.8,20A2,2 0 0,1 12.8,18A2,2 0 0,1 14.8,16M15.2,4A4.8,4.8 0 0,1 20,8.8C20,11.45 17.85,13.6 15.2,13.6A4.8,4.8 0 0,1 10.4,8.8C10.4,6.15 12.55,4 15.2,4Z");
			ChartDonut = CreateGeometry("M13,2.05V5.08C16.39,5.57 19,8.47 19,12C19,12.9 18.82,13.75 18.5,14.54L21.12,16.07C21.68,14.83 22,13.45 22,12C22,6.82 18.05,2.55 13,2.05M12,19A7,7 0 0,1 5,12C5,8.47 7.61,5.57 11,5.08V2.05C5.94,2.55 2,6.81 2,12A10,10 0 0,0 12,22C15.3,22 18.23,20.39 20.05,17.91L17.45,16.38C16.17,18 14.21,19 12,19Z");
			ChartDonutVariant = CreateGeometry("M13,2.05C18.05,2.55 22,6.82 22,12C22,13.45 21.68,14.83 21.12,16.07L18.5,14.54C18.82,13.75 19,12.9 19,12C19,8.47 16.39,5.57 13,5.08V2.05M12,19C14.21,19 16.17,18 17.45,16.38L20.05,17.91C18.23,20.39 15.3,22 12,22C6.47,22 2,17.5 2,12C2,6.81 5.94,2.55 11,2.05V5.08C7.61,5.57 5,8.47 5,12A7,7 0 0,0 12,19M12,6A6,6 0 0,1 18,12C18,14.97 15.84,17.44 13,17.92V14.83C14.17,14.42 15,13.31 15,12A3,3 0 0,0 12,9L11.45,9.05L9.91,6.38C10.56,6.13 11.26,6 12,6M6,12C6,10.14 6.85,8.5 8.18,7.38L9.72,10.05C9.27,10.57 9,11.26 9,12C9,13.31 9.83,14.42 11,14.83V17.92C8.16,17.44 6,14.97 6,12Z");

			Database = CreateGeometry("M12,3C7.58,3 4,4.79 4,7C4,9.21 7.58,11 12,11C16.42,11 20,9.21 20,7C20,4.79 16.42,3 12,3M4,9V12C4,14.21 7.58,16 12,16C16.42,16 20,14.21 20,12V9C20,11.21 16.42,13 12,13C7.58,13 4,11.21 4,9M4,14V17C4,19.21 7.58,21 12,21C16.42,21 20,19.21 20,17V14C20,16.21 16.42,18 12,18C7.58,18 4,16.21 4,14Z");
			DatabaseMinus = CreateGeometry("M9,3C4.58,3 1,4.79 1,7C1,9.21 4.58,11 9,11C13.42,11 17,9.21 17,7C17,4.79 13.42,3 9,3M1,9V12C1,14.21 4.58,16 9,16C13.42,16 17,14.21 17,12V9C17,11.21 13.42,13 9,13C4.58,13 1,11.21 1,9M1,14V17C1,19.21 4.58,21 9,21C10.41,21 11.79,20.81 13,20.46V17.46C11.79,17.81 10.41,18 9,18C4.58,18 1,16.21 1,14M15,17V19H23V17");
			DatabasePlus = CreateGeometry("M9,3C4.58,3 1,4.79 1,7C1,9.21 4.58,11 9,11C13.42,11 17,9.21 17,7C17,4.79 13.42,3 9,3M1,9V12C1,14.21 4.58,16 9,16C13.42,16 17,14.21 17,12V9C17,11.21 13.42,13 9,13C4.58,13 1,11.21 1,9M1,14V17C1,19.21 4.58,21 9,21C10.41,21 11.79,20.81 13,20.46V17.46C11.79,17.81 10.41,18 9,18C4.58,18 1,16.21 1,14M18,14V17H15V19H18V22H20V19H23V17H20V14");
		}

		private static System.Windows.Media.Geometry CreateGeometry(string source)
		{
			var geometry = System.Windows.Media.Geometry.Parse(source);
			geometry.Freeze();
			return geometry;
		}
	}
}