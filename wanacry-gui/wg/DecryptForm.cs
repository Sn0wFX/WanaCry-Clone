using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace wg
{
	public class DecryptForm : Form
	{
		private IContainer components;

		private Button DecryptDemo;

		private Button DecryptAll;

		public DecryptForm()
		{
			this.InitializeComponent();
			if (Utils.IsPaymentReceived())
			{
				this.DecryptAll.Text = "Decrypt all files";
				this.DecryptAll.Enabled = true;
			}
		}

		private void DecryptDemo_Click(object sender, EventArgs e)
		{
			Utils.Decrypt(true);
			MessageBox.Show(this, "Images decrypted!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			base.Close();
		}

		private void DecryptAll_Click(object sender, EventArgs e)
		{
			Utils.Decrypt(false);
			MessageBox.Show(this, "Files decrypted!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			base.Close();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.DecryptDemo = new Button();
			this.DecryptAll = new Button();
			base.SuspendLayout();
			this.DecryptDemo.Location = new Point(12, 30);
			this.DecryptDemo.Name = "DecryptDemo";
			this.DecryptDemo.Size = new Size(155, 48);
			this.DecryptDemo.TabIndex = 0;
			this.DecryptDemo.Text = "Decrypt images (Free)";
			this.DecryptDemo.UseVisualStyleBackColor = true;
			this.DecryptDemo.Click += new EventHandler(this.DecryptDemo_Click);
			this.DecryptAll.Enabled = false;
			this.DecryptAll.Location = new Point(173, 30);
			this.DecryptAll.Name = "DecryptAll";
			this.DecryptAll.Size = new Size(155, 48);
			this.DecryptAll.TabIndex = 1;
			this.DecryptAll.Text = "Decrypt all files \r\n(Will available after payment)";
			this.DecryptAll.UseVisualStyleBackColor = true;
			this.DecryptAll.Click += new EventHandler(this.DecryptAll_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(340, 119);
			base.Controls.Add(this.DecryptAll);
			base.Controls.Add(this.DecryptDemo);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DecryptForm";
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Decrypt files";
			base.ResumeLayout(false);
		}
	}
}
