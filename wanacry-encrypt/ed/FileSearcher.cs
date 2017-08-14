using System;
using System.Collections.Generic;
using System.IO;

namespace ed
{
	public class FileSearcher
	{
		private List<string> _files;

		public List<string> Files
		{
			get
			{
				return this._files;
			}
		}

		private List<string> FindFiles(string path, string pattern)
		{
			List<string> list = new List<string>();
			try
			{
				list.AddRange(Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly));
				string[] directories = Directory.GetDirectories(path);
				for (int i = 0; i < directories.Length; i++)
				{
					string path2 = directories[i];
					list.AddRange(this.FindFiles(path2, pattern));
				}
			}
			catch
			{
			}
			return list;
		}

		public FileSearcher(string startDirectory)
		{
			this._files = this.FindFiles(startDirectory, "*");
		}

		public static List<string> ExcludeDirectories(List<string> files, string[] directories)
		{
			List<string> list = new List<string>();
			foreach (string current in files)
			{
				bool flag = false;
				for (int i = 0; i < directories.Length; i++)
				{
					string text = directories[i];
					string[] array = Path.GetDirectoryName(current).Split(new char[]
					{
						'\\',
						'/'
					});
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].ToLower() == text.ToLower())
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						break;
					}
				}
				if (!flag)
				{
					list.Add(current);
				}
			}
			return list;
		}

		public static List<string> GetFilesByExtension(List<string> files, string extension)
		{
			List<string> list = new List<string>();
			string str = extension.ToLower();
			foreach (string current in files)
			{
				if (Path.GetExtension(current).ToLower() == "." + str)
				{
					list.Add(current);
				}
			}
			return list;
		}
	}
}
