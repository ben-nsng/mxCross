using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace mxCross
{
	public class mxDataProcesser
	{
		public mxDataProcesser()
		{
			data = new Dictionary<IntPtr, mxData>();
		}

		public void Add(ref Message m)
		{
			mxMessage msg = (mxMessage)Enum.Parse(typeof(mxMessage), m.Msg.ToString());
			mxDataEventArgs e = new mxDataEventArgs(m.WParam, m.LParam);

			switch (msg)
			{
					//System Defined Event
				case mxMessage.ProcessClosed:
					if (wAPI.parent)
						mxParent.Close(data.Keys);
					else
						mxChild.Close();
					break;
					//User Defined Event
					//Child To Parent Event
				case mxMessage.ParentReceivedAction:

					mxAction act = (mxAction)Enum.Parse(typeof(mxAction), m.LParam.ToString());
					if (act == mxAction.OpenConnection)
					{
						mxData datum = new mxData();
						datum.mxOpenConn += new mxData.mxDataHandler(datum_mxOpenConn);
						datum.mxCloseConn += new mxData.mxDataHandler(datum_mxCloseConn);
						datum.mxStrLen += new mxData.mxDataHandler(datum_mxStrLen);
						datum.mxStrPointer += new mxData.mxDataHandler(datum_mxStrPointer);
						data.Add(m.WParam, datum);
					}

					data[m.WParam].Act(act, e);

					break;

				case mxMessage.ParentExtraPipe_strPointer:
					data[m.WParam].Act(mxAction.strPointer, e);
					break;

				case mxMessage.ParentExtraPipe_strLen:
					data[m.WParam].Act(mxAction.strLen, e);
					break;
					//Parent To User Event
				case mxMessage.ChildReceivedAction:

					act = (mxAction)Enum.Parse(typeof(mxAction), m.LParam.ToString());
					if (act == mxAction.CloseConnection)
						Environment.Exit(Environment.ExitCode);
					break;
				case mxMessage.ChildExtraPipe_strPointer:
					IntPtr hwndId = wAPI.FindWindow(null, mxParent.title);
					int processId;
					wAPI.GetWindowThreadProcessId(hwndId, out processId);
					wAPI.MainForm.tboxtest.Text += PointerToString(Process.GetProcessById(processId), (int)m.LParam, clen * 2);
					break;
				case mxMessage.ChildExtraPipe_strLen:
					clen = (int)m.LParam;
					break;
			}
		}

		//Function
		private string PointerToString(Process process, int address, int numOfBytes)
		{
			string s = "";
			int r;
			byte[] b;
			b = wAPI.ReadMemory(process, address, numOfBytes, out r);
			for (int i = 0; i < b.Length; i += 2)
				s += (char)(b[i] | (b[i+1] << 8));
			return s;

		}

		//Event
		private void datum_mxOpenConn(mxData sender, mxDataEventArgs e)
		{
			wAPI.MainForm.tboxtest.Text += "Child Handle: " + e.childHandle + " Enter.\r\n";
			wAPI.MainForm.cbxChildList.Items.Add(e.childHandle.ToString());
		}

		private void datum_mxStrPointer(mxData sender, mxDataEventArgs e)
		{
			if (sender.strLen == 0)
				return;

			Process[] processes = Process.GetProcessesByName(mxChild.title);
			foreach (Process process in processes)
				if (process.MainWindowHandle == e.childHandle)
				{
					wAPI.MainForm.tboxtest.Text += PointerToString(process, (int)e.lParam, (int)sender.strLen * 2);
					break;
				}
			mxParent.lastContactChild = e.childHandle;
			mxParent.Send("Server Response: Data Received OK.", e.childHandle);
			sender.strLen = 0;
		}

		private void datum_mxStrLen(mxData sender, mxDataEventArgs e)
		{
			sender.strLen = (int)e.lParam;
		}

		private void datum_mxCloseConn(mxData sender, mxDataEventArgs e)
		{
			wAPI.MainForm.tboxtest.Text += "Child Handle: " + e.childHandle + " Exit.\r\n";
			wAPI.MainForm.cbxChildList.Items.Remove(e.childHandle.ToString());
			data.Remove(e.childHandle);
		}


		private Dictionary<IntPtr, mxData> data;
		private int clen = 0;
	}

	public class mxData
	{
		public delegate void mxDataHandler(mxData sender, mxDataEventArgs e);
		public event mxDataHandler mxStrPointer;
		public event mxDataHandler mxStrLen;
		public event mxDataHandler mxOpenConn;
		public event mxDataHandler mxCloseConn;

		public void Act(mxAction act, mxDataEventArgs e)
		{
			switch (act)
			{
				case mxAction.OpenConnection:
					mxOpenConn(this, e);
					break;
				case mxAction.CloseConnection:
					mxCloseConn(this, e);
					break;
				case mxAction.strLen:
					mxStrLen(this, e);
					break;
				case mxAction.strPointer:
					mxStrPointer(this, e);
					break;
			}
		}

		public int strLen = 0;
	}

	public class mxDataEventArgs : EventArgs
	{
		public mxDataEventArgs(IntPtr pWParam, IntPtr pLParam)
		{
			this.childHandle = pWParam;
			this.lParam = pLParam;
		}

		public readonly IntPtr childHandle;
		public readonly IntPtr lParam;
	}

	public enum mxAction
	{
		OpenConnection,

		strPointer,
		strLen,

		CloseConnection
	}

	public enum mxMessage
	{
		ProcessClosed = 0x010,

		ParentReceivedAction = 0x410,
		ParentExtraPipe_strPointer = 0x411,
		ParentExtraPipe_strLen = 0x412,

		ChildReceivedAction = 0x510,
		ChildExtraPipe_strPointer = 0x511,
		ChildExtraPipe_strLen = 0x512
	}
}
