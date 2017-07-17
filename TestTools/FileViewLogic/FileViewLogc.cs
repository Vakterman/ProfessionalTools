using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace Professional.Tools.FileViewLogic
{
	public class FileViewLogc
	{
		private readonly string _rootPath;
		/// <summary>
		/// if rootPath == null => starts from list of available drives
		/// </summary>
		/// <param name="rootPath"></param>
		public FileViewLogc(string rootPath = null)
		{
			_rootPath = rootPath;
		}

		public List<FileTreeItem> GetRootStructure()
		{
			return Directory.GetLogicalDrives().Select(
				localDrive =>
					new FileTreeItem
					{
						Name = localDrive,
						Path = localDrive,
						Type = FileItemType.Drive
					}).ToList();
		}

		public List<FileTreeItem> GetFolders()
		{
			return Directory.GetDirectories(_rootPath).Select(
				dirName => new FileTreeItem
				{
					Name = Path.GetFileName(dirName),
					Path = dirName,
					Type = FileItemType.Directory
				}).ToList();
		}

		public void RemoveRecurcivelyByPatterns(string[] patterns)
		{

		    var regularPatterns =  	patterns.Select(pattern => new WildcardPattern(pattern));

			RemoveRecurcively(_rootPath, regularPatterns);
		}

		private void RemoveRecurcively(string path, IEnumerable<WildcardPattern> patterns)
		{
			bool NameIsMatch(string name) => patterns.Any(expression => expression.IsMatch(name));

			var fileList = Directory.EnumerateFiles(path).ToList();
			fileList.ForEach(
				file =>
				{
					if (NameIsMatch(Path.GetFileName(file))) File.Delete(file);
				});

			Directory.EnumerateDirectories(path).ToList().ForEach(
				directory =>
				{
					RemoveRecurcively(directory, patterns);
				}
			);
		}

	}
}
