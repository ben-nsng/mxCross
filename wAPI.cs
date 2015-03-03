using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace mxCross
{
	public static class wAPI
	{
		//Form1 Global Used
		public static Form1 MainForm = null;
		public static bool parent = true;
		public static string title = "mxCross";

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32")]
		public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);

		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenProcess(uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

		public static byte[] ReadMemory(Process process, int address, int numOfBytes, out int bytesRead)
		{
			IntPtr hProc = wAPI.OpenProcess(0x10, false, process.Id);

			byte[] buffer = new byte[numOfBytes];

			wAPI.ReadProcessMemory(hProc, new IntPtr(address), buffer, numOfBytes, out bytesRead);
			return buffer;
		}
	}
}