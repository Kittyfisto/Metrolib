using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using FluentAssertions;
using Metrolib.Controls;
using Moq;
using NUnit.Framework;

namespace Metrolib.Test.Tree
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public sealed class FlatTreeViewItemTest
	{
		private ITreeViewItemViewModel _model;

		sealed class TreeViewItemViewModel
			: ITreeViewItemViewModel
		{
			private bool _isSelected;
			private bool _isExpanded;
			public event PropertyChangedEventHandler PropertyChanged;

			public bool IsSelected
			{
				get { return _isSelected; }
				set
				{
					if (value == _isSelected)
						return;

					_isSelected = value;
					EmitPropertyChanged();
				}
			}

			public bool IsExpanded
			{
				get { return _isExpanded; }
				set
				{
					if (value == _isExpanded)
						return;

					_isExpanded = value;
					EmitPropertyChanged();
				}
			}

			private void EmitPropertyChanged([CallerMemberName] string propertyName = null)
			{
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		[SetUp]
		public void Setup()
		{
			_model = new TreeViewItemViewModel();
		}

		[Test]
		public void TestCtor()
		{
			var item = new FlatTreeViewItem();
			item.IsExpandable.Should().BeTrue();
		}

		[Test]
		[NUnit.Framework.Description("Verifies that the IFlatTreeViewItemViewModel is optional and properties can be changed without it")]
		public void TestExpand1()
		{
			var item = new FlatTreeViewItem();
			new Action(() => item.IsExpanded = true).ShouldNotThrow();
			item.IsExpanded.Should().BeTrue();
			new Action(() => item.IsExpanded = false).ShouldNotThrow();
			item.IsExpanded.Should().BeFalse();
		}

		[Test]
		public void TestExpand2()
		{
			var item = new FlatTreeViewItem
			{
				Header = _model
			};
			item.IsExpanded = true;
			item.IsExpanded.Should().BeTrue();
			_model.IsExpanded.Should().BeTrue();

			item.IsExpanded = false;
			item.IsExpanded.Should().BeFalse();
			_model.IsExpanded.Should().BeFalse();
		}

		[Test]
		public void TestExpand3([Values(true, false)]bool isExpanded)
		{
			var item = new FlatTreeViewItem();
			_model.IsExpanded = isExpanded;
			item.Header = _model;
			item.IsExpanded.Should().Be(isExpanded);
			_model.IsExpanded.Should().Be(isExpanded);
		}

		[Test]
		[NUnit.Framework.Description("Verifies that the item reacts to property changes of the view model")]
		public void TestExpand4([Values(true, false)]bool isExpanded)
		{
			var item = new FlatTreeViewItem();
			item.Header = _model;

			_model.IsExpanded = isExpanded;
			item.IsExpanded.Should().Be(isExpanded);
			_model.IsExpanded.Should().Be(isExpanded);
		}
		
		[Test]
		[NUnit.Framework.Description("Verifies that the IFlatTreeViewItemViewModel is optional and properties can be changed without it")]
		public void TestSelect1()
		{
			var item = new FlatTreeViewItem();
			new Action(() => item.IsSelected = true).ShouldNotThrow();
			item.IsSelected.Should().BeTrue();
			new Action(() => item.IsSelected = false).ShouldNotThrow();
			item.IsSelected.Should().BeFalse();
		}

		[Test]
		public void TestSelect2()
		{
			var item = new FlatTreeViewItem
			{
				Header = _model
			};
			item.IsSelected = true;
			item.IsSelected.Should().BeTrue();
			_model.IsSelected.Should().BeTrue();

			item.IsSelected = false;
			item.IsSelected.Should().BeFalse();
			_model.IsSelected.Should().BeFalse();
		}

		[Test]
		public void TestSelect3([Values(true, false)]bool isSelected)
		{
			var item = new FlatTreeViewItem();
			_model.IsSelected = isSelected;
			item.Header = _model;
			item.IsSelected.Should().Be(isSelected);
			_model.IsSelected.Should().Be(isSelected);
		}

		[Test]
		[NUnit.Framework.Description("Verifies that the item reacts to property changes of the view model")]
		public void TestSelect4([Values(true, false)]bool isSelected)
		{
			var item = new FlatTreeViewItem();
			item.Header = _model;

			_model.IsSelected = isSelected;
			item.IsSelected.Should().Be(isSelected);
			_model.IsSelected.Should().Be(isSelected);
		}
	}
}