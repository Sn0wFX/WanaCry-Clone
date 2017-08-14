using System;
using System.Security.Cryptography;

namespace ed
{
	public class RSAKeyPair
	{
		private byte[] _publicKey;

		private byte[] _privateKey;

		public byte[] PublicKey
		{
			get
			{
				return this._publicKey;
			}
		}

		public byte[] PrivateKey
		{
			get
			{
				return this._privateKey;
			}
		}

		public RSAKeyPair()
		{
			using (RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider(2048))
			{
				this._publicKey = rSACryptoServiceProvider.ExportCspBlob(false);
				this._privateKey = rSACryptoServiceProvider.ExportCspBlob(true);
			}
		}

		public void ClearPrivateKey()
		{
			for (int i = 0; i < this._privateKey.Length; i++)
			{
				this._privateKey[i] = 0;
			}
		}
	}
}
