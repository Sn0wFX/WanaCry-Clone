using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace wanna_cry.Properties
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
					Resources.resourceMan = new ResourceManager("wanna_cry.Properties.Resources", typeof(Resources).Assembly);
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

		internal static byte[] ed
		{
			get
			{
				return (byte[])Resources.ResourceManager.GetObject("ed", Resources.resourceCulture);
			}
		}

		internal static byte[] wg
		{
			get
			{
				return (byte[])Resources.ResourceManager.GetObject("wg", Resources.resourceCulture);
			}
		}

		internal Resources()
		{
		}
	}
}
