using System;
using System.Text;
using System.Collections.Generic;

namespace mxCross
{
	public static class mxParent
	{
		//Title Must Be Unique, Must Not Be The Same Of Child Title
		public static string title { get { return wAPI.title + " Server"; } }
		public static IntPtr lastContactChild { get; set; }
		private static IntPtr parentHandle
		{
			get
			{
				return wAPI.MainForm.Handle;
			}
		}

		public static void Initialize()
		{
		}

		public static void Send(String s, IntPtr child)
		{
			s += "\r\n";
			unsafe
			{
				wAPI.PostMessage(child, (uint)mxMessage.ChildExtraPipe_strLen, (int)parentHandle, (int)s.Length);
				fixed (void* ps = s)
				{
					wAPI.PostMessage(child, (uint)mxMessage.ChildExtraPipe_strPointer, (int)parentHandle, (int)ps);
				}
			}
		}

		
		public static void SendToLastChild(String s)
		{
			if (lastContactChild != IntPtr.Zero)
				Send(s, lastContactChild);
		}
		

		public static void Close(IEnumerable<IntPtr> children)
		{
			foreach (IntPtr child in children)
			{
				wAPI.PostMessage(child, (uint)mxMessage.ChildReceivedAction, (int)wAPI.MainForm.Handle, (int)mxAction.CloseConnection);
			}
		}
	}

}