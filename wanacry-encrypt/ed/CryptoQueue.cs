using System;
using System.Collections.Generic;
using System.Threading;

namespace ed
{
	public class CryptoQueue
	{
		private Queue<string> _files;

		private byte[] _publicKey;

		private byte[] _privateKey;

		public bool IsDone
		{
			get
			{
				return this._files.Count == 0;
			}
		}

		public CryptoQueue(List<string> files)
		{
			this._files = new Queue<string>(files);
		}

		public void BeginEncryption(byte[] publicKeyBlob)
		{
			this._publicKey = (byte[])publicKeyBlob.Clone();
			new Thread(new ThreadStart(this.EncryptThreadFunc)).Start();
		}

		public void BeginDecryption(byte[] privateKeyBlob)
		{
			this._privateKey = (byte[])privateKeyBlob.Clone();
			new Thread(new ThreadStart(this.DecryptThreadFunc)).Start();
		}

		private void EncryptThreadFunc()
		{
			while (this._files.Count > 0)
			{
				string text = this._files.Dequeue();
				if (CryptoFile.Encrypt(text, this._publicKey))
				{
					Console.WriteLine(text);
				}
			}
		}

		private void DecryptThreadFunc()
		{
			while (this._files.Count > 0)
			{
				string text = this._files.Dequeue();
				if (CryptoFile.Decrypt(text, this._privateKey))
				{
					Console.WriteLine(text);
				}
			}
		}
	}
}
