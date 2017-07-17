using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Professional.Tools.FileViewLogic;

namespace Professional.Tools
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var fileLogic = new FileViewLogc();
			fileLogic.GetRootStructure().ForEach(item =>
				FileView.Items.Add(item)
			);
		}

		private void OnItemExpanded(object sender, RoutedEventArgs e)
		{
			var treeViewItemSender = e.OriginalSource as TreeViewItem;
			if (treeViewItemSender != null)
			{
				var fileTreeItem = 	treeViewItemSender.DataContext as FileTreeItem;
				var fileLogic = new FileViewLogc(fileTreeItem.Path);

				fileLogic.GetFolders().ForEach(treeItem => 
					treeViewItemSender.Items.Add(treeItem));
			}
		}

		private void OnItemSelected(object sender, RoutedEventArgs e)
		{
			
		}

		private void RemoveByExtensions(object sender, RoutedEventArgs e)
		{
			var patterns = FileExtensions.Text.Split(',');
			var fileTreeItem = FileView.SelectedItem as FileTreeItem;
			if (fileTreeItem != null)
			{
				var fileLogic = new FileViewLogc(fileTreeItem.Path);
				fileLogic.RemoveRecurcivelyByPatterns(patterns);
			}
		}
	}
}
