using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Professional.Tools.FileViewLogic;

namespace Professional.Tools
{
	public class TreeItemImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var itemType = (FileItemType)value;

			return itemType == FileItemType.Drive ? new BitmapImage(new Uri("pack://application:,,,/Image/diskdrive.png")) : 
				new BitmapImage(new Uri("pack://application:,,,/Image/folder.png"));
		} 

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException("Cannot convert back");
		}
	}
}
