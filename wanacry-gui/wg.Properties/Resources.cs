using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace wg.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
	internal class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("wg.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		internal static string readme
		{
			get
			{
				return Resources.ResourceManager.GetString("readme", Resources.resourceCulture);
			}
		}

		internal static byte[] tg
		{
			get
			{
				return (byte[])Resources.ResourceManager.GetObject("tg", Resources.resourceCulture);
			}
		}

		internal static Bitmap wannacry
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("wannacry", Resources.resourceCulture);
			}
		}

		internal Resources()
		{
		}
	}
}
