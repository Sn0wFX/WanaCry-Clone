using System;
using System.Diagnostics;

namespace wg
{
	public static class PS
	{
		public static bool RedirectOutput
		{
			get;
			set;
		}

		private static string Execute(string path, string arguments, bool wait)
		{
			string result;
			try
			{
				Process process = PS.StartHidden(path, arguments);
				if (wait)
				{
					process.WaitForExit();
					result = process.StandardOutput.ReadToEnd();
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = "";
			}
			return result;
		}

		public static string ExecuteAndWait(string path, string arguments)
		{
			return PS.Execute(path, arguments, true);
		}

		public static void ExecuteAndForget(string path, string arguments)
		{
			PS.Execute(path, arguments, false);
		}

		public static Process StartHidden(string path, string arguments)
		{
			Process expr_05 = new Process();
			expr_05.StartInfo.FileName = path;
			expr_05.StartInfo.Arguments = arguments;
			expr_05.StartInfo.UseShellExecute = false;
			expr_05.StartInfo.RedirectStandardOutput = PS.RedirectOutput;
			expr_05.StartInfo.CreateNoWindow = true;
			expr_05.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			expr_05.Start();
			return expr_05;
		}

		static PS()
		{
			// Note: this type is marked as 'beforefieldinit'.
			//PS.<RedirectOutput>k__BackingField = true;
		}
	}
}
