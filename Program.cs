using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace mxCross
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			//Checking Parent Existence
			bool createdNew, parent = true;
			Mutex m = new Mutex(false, strMutexName, out createdNew);
			if (!createdNew)
				parent = false;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1(parent));
		}

		static String strMutexName = "mxCross";
	}
}
