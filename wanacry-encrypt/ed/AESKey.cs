using System;
using System.Security.Cryptography;

namespace ed
{
	public class AESKey
	{
		private byte[] _key;

		private byte[] _iv;

		public byte[] Key
		{
			get
			{
				return this._key;
			}
		}

		public byte[] IV
		{
			get
			{
				return this._iv;
			}
		}

		public AESKey()
		{
			using (AesManaged aesManaged = new AesManaged())
			{
				aesManaged.Mode = CipherMode.CBC;
				aesManaged.KeySize = 256;
				aesManaged.Padding = PaddingMode.None;
				aesManaged.GenerateKey();
				aesManaged.GenerateIV();
				this._key = aesManaged.Key;
				this._iv = aesManaged.IV;
			}
		}
	}
}
