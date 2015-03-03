using System;
using System.Text;
using System.Windows.Forms;

namespace mxCross
{
	public static class mxChild
	{
		public static string title { get { return wAPI.title; } }
		public static string myString;
		private static IntPtr parentHandle
		{
			get
			{
				IntPtr t = wAPI.FindWindow(null, mxParent.title);
				if (t == IntPtr.Zero)
				{
					MessageBox.Show("Server Window is closed.\r\nThis Program is ready to close.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					Environment.Exit(Environment.ExitCode);
				}
				return t;
			}
		}
		private static IntPtr childHandle
		{
			get
			{
				return wAPI.MainForm.Handle;
			}
		}

		public static void Initialize()
		{
			wAPI.MainForm.Text = title;

			wAPI.PostMessage(parentHandle, (uint)mxMessage.ParentReceivedAction, (int)childHandle, (int)mxAction.OpenConnection);
		}

		public static void Send(string s)
		{
			s += "\r\n";
			unsafe
			{
				wAPI.PostMessage(parentHandle, (uint)mxMessage.ParentExtraPipe_strLen, (int)childHandle, (int)s.Length);
				fixed (void* ps = s)
				{
					wAPI.PostMessage(parentHandle, (uint)mxMessage.ParentExtraPipe_strPointer, (int)childHandle, (int)ps);
				}
			}
		}

		public static void Close()
		{
			wAPI.PostMessage(parentHandle, (uint)mxMessage.ParentReceivedAction, (int)childHandle, (int)mxAction.CloseConnection);
		}
	}
}