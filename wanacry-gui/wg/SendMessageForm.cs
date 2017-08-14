using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace wg
{
	public class SendMessageForm : Form
	{
		private IContainer components;

		private TextBox MessageText;

		private Button Send;

		private TextBox Email;

		private Label EmailLabel;

		public SendMessageForm()
		{
			this.InitializeComponent();
		}

		private void Send_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.MessageText.Text))
			{
				MessageBox.Show(this, "Message can not be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			Utils.CheckInternetConnection();
			Utils.SendMessage(this.Email.Text + " : " + this.MessageText, Utils.useProxy);
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
			this.MessageText = new TextBox();
			this.Send = new Button();
			this.Email = new TextBox();
			this.EmailLabel = new Label();
			base.SuspendLayout();
			this.MessageText.Location = new Point(12, 12);
			this.MessageText.Multiline = true;
			this.MessageText.Name = "MessageText";
			this.MessageText.Size = new Size(301, 153);
			this.MessageText.TabIndex = 0;
			this.Send.Location = new Point(196, 171);
			this.Send.Name = "Send";
			this.Send.Size = new Size(117, 29);
			this.Send.TabIndex = 1;
			this.Send.Text = "Send message";
			this.Send.UseVisualStyleBackColor = true;
			this.Send.Click += new EventHandler(this.Send_Click);
			this.Email.Location = new Point(50, 176);
			this.Email.Name = "Email";
			this.Email.Size = new Size(140, 20);
			this.Email.TabIndex = 2;
			this.EmailLabel.AutoSize = true;
			this.EmailLabel.Location = new Point(9, 179);
			this.EmailLabel.Name = "EmailLabel";
			this.EmailLabel.Size = new Size(35, 13);
			this.EmailLabel.TabIndex = 3;
			this.EmailLabel.Text = "Email:";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(325, 210);
			base.Controls.Add(this.EmailLabel);
			base.Controls.Add(this.Email);
			base.Controls.Add(this.Send);
			base.Controls.Add(this.MessageText);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SendMessageForm";
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Support";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
