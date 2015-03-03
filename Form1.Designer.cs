namespace mxCross
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tboxtest = new System.Windows.Forms.TextBox();
			this.btnReport = new System.Windows.Forms.Button();
			this.cbxChildList = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// tboxtest
			// 
			this.tboxtest.Location = new System.Drawing.Point(12, 12);
			this.tboxtest.Multiline = true;
			this.tboxtest.Name = "tboxtest";
			this.tboxtest.Size = new System.Drawing.Size(260, 238);
			this.tboxtest.TabIndex = 0;
			// 
			// btnReport
			// 
			this.btnReport.Location = new System.Drawing.Point(195, 256);
			this.btnReport.Name = "btnReport";
			this.btnReport.Size = new System.Drawing.Size(77, 27);
			this.btnReport.TabIndex = 1;
			this.btnReport.Text = "Report";
			this.btnReport.UseVisualStyleBackColor = true;
			this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
			// 
			// cbxChildList
			// 
			this.cbxChildList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxChildList.FormattingEnabled = true;
			this.cbxChildList.Location = new System.Drawing.Point(12, 263);
			this.cbxChildList.Name = "cbxChildList";
			this.cbxChildList.Size = new System.Drawing.Size(177, 20);
			this.cbxChildList.TabIndex = 2;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 295);
			this.Controls.Add(this.cbxChildList);
			this.Controls.Add(this.btnReport);
			this.Controls.Add(this.tboxtest);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		internal System.Windows.Forms.TextBox tboxtest;
		private System.Windows.Forms.Button btnReport;
		internal System.Windows.Forms.ComboBox cbxChildList;

	}
}

