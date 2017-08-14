using System;
using System.IO;

namespace ed
{
	public static class StreamHelpers
	{
		public static byte[] ReadAllBytes(this BinaryReader reader)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte[] array = new byte[4096];
				int count;
				while ((count = reader.Read(array, 0, array.Length)) != 0)
				{
					memoryStream.Write(array, 0, count);
				}
				result = memoryStream.ToArray();
			}
			return result;
		}
	}
}
