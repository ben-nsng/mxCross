using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;



namespace mxCross
{
	public partial class Form1 : Form
	{
		private mxDataProcesser _mxDataProcesser;

		public Form1(bool parent)
		{
			
			InitializeComponent();

			wAPI.MainForm = this;
			wAPI.parent = parent;
			this._mxDataProcesser = new mxDataProcesser();

			//Debug
			tboxtest.Text = "This Handle: " + this.Handle.ToString() + "\r\n";

			if (parent)
			{
				btnReport.Text = "Send";
			}
			else
			{
				cbxChildList.Visible = false;
				mxChild.Initialize();
			}
			
		}

		protected override void WndProc(ref Message m)
		{
			_mxDataProcesser.Add(ref m);

			base.WndProc(ref m);
		}

		//Title Must Be Programmatically Defined!!!
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if (wAPI.parent)
					base.Text = mxParent.title;
				else
					base.Text = mxChild.title;
			}
		}

		private void btnReport_Click(object sender, EventArgs e)
		{
			if (wAPI.parent)
			{
				if(cbxChildList.Text != "")
					mxParent.Send("Parent Send Signal To Child. Parent Handle: " + this.Handle, (IntPtr)int.Parse(cbxChildList.Text));
			}
			else
				mxChild.Send("測試!Reporting to Server. Child Handle:" + this.Handle);
		}

	}
}
