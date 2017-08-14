using System;
using System.Security.Cryptography;

namespace ed
{
	public static class RSA
	{
		public static byte[] Encrypt(byte[] publicKeyBlob, byte[] data)
		{
			byte[] result = null;
			using (RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider())
			{
				rSACryptoServiceProvider.ImportCspBlob(publicKeyBlob);
				result = rSACryptoServiceProvider.Encrypt(data, false);
			}
			return result;
		}

		public static byte[] Decrypt(byte[] privateKeyBlob, byte[] data)
		{
			byte[] result = null;
			using (RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider())
			{
				rSACryptoServiceProvider.ImportCspBlob(privateKeyBlob);
				result = rSACryptoServiceProvider.Decrypt(data, false);
			}
			return result;
		}
	}
}
