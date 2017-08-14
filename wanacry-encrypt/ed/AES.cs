using System;
using System.IO;
using System.Security.Cryptography;

namespace ed
{
	public static class AES
	{
		public static byte[] Encrypt(byte[] key, byte[] iv, byte[] data)
		{
			byte[] result;
			using (AesManaged aesManaged = new AesManaged())
			{
				aesManaged.Mode = CipherMode.CBC;
				aesManaged.KeySize = 256;
				aesManaged.Key = key;
				aesManaged.IV = iv;
				aesManaged.Padding = PaddingMode.None;
				ICryptoTransform transform = aesManaged.CreateEncryptor(aesManaged.Key, aesManaged.IV);
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
					{
						using (BinaryWriter binaryWriter = new BinaryWriter(cryptoStream))
						{
							binaryWriter.Write(data);
						}
						result = memoryStream.ToArray();
					}
				}
			}
			return result;
		}

		public static byte[] Decrypt(byte[] key, byte[] iv, byte[] data)
		{
			byte[] result = null;
			using (AesManaged aesManaged = new AesManaged())
			{
				aesManaged.Mode = CipherMode.CBC;
				aesManaged.KeySize = 256;
				aesManaged.Key = key;
				aesManaged.IV = iv;
				aesManaged.Padding = PaddingMode.None;
				ICryptoTransform transform = aesManaged.CreateDecryptor(aesManaged.Key, aesManaged.IV);
				using (MemoryStream memoryStream = new MemoryStream(data))
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read))
					{
						using (BinaryReader binaryReader = new BinaryReader(cryptoStream))
						{
							result = binaryReader.ReadAllBytes();
						}
					}
				}
			}
			return result;
		}
	}
}
