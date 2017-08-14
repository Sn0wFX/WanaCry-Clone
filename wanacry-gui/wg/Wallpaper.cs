using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace wg
{
	public class Wallpaper
	{
		public enum Style
		{
			Tiled,
			Centered,
			Stretched
		}

		public static readonly uint SPI_SETDESKWALLPAPER = 20u;

		public static readonly uint SPIF_UPDATEINIFILE = 1u;

		public static readonly uint SPIF_SENDWININICHANGE = 2u;

		[DllImport("user32.dll")]
		public static extern int SystemParametersInfo(uint action, uint uParam, string vParam, uint winIni);

		public static bool Set(string filePath, Wallpaper.Style style)
		{
			bool result = false;
			try
			{
				Wallpaper.Set(Image.FromFile(Path.GetFullPath(filePath)), style);
				result = true;
			}
			catch
			{
			}
			return result;
		}

		public static bool Set(Image image, Wallpaper.Style style)
		{
			bool result = false;
			try
			{
				string text = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
				image.Save(text, ImageFormat.Bmp);
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", true);
				switch (style)
				{
				case Wallpaper.Style.Centered:
					registryKey.SetValue("WallpaperStyle", 1.ToString());
					registryKey.SetValue("TileWallpaper", 0.ToString());
					goto IL_BF;
				case Wallpaper.Style.Stretched:
					registryKey.SetValue("WallpaperStyle", 2.ToString());
					registryKey.SetValue("TileWallpaper", 0.ToString());
					goto IL_BF;
				}
				registryKey.SetValue("WallpaperStyle", 1.ToString());
				registryKey.SetValue("TileWallpaper", 1.ToString());
				IL_BF:
				Wallpaper.SystemParametersInfo(Wallpaper.SPI_SETDESKWALLPAPER, 0u, text, Wallpaper.SPIF_UPDATEINIFILE | Wallpaper.SPIF_SENDWININICHANGE);
				result = true;
			}
			catch
			{
			}
			return result;
		}
	}
}
