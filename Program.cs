using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace VoiceMeeterPriority
{
    internal class Program
    {

        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow([In] IntPtr hWnd, [In] int nCmdShow);
        static void Main()
        {
            IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(handle, 6);
            checkProcess();
        }

        static void checkProcess()
        {
            System.Threading.Thread.Sleep(5000);
            if (Process.GetProcessesByName("audiodg").Length == 0)
                checkProcess();
            else
                setPriorities();
        }

        static void setPriorities()
        {
            Process audiodg = Process.GetProcessesByName("audiodg")[0];
            audiodg.ProcessorAffinity = (IntPtr)(1L << 1);
            audiodg.PriorityClass = ProcessPriorityClass.High;
            foreach (ProcessThread thread in audiodg.Threads)
                thread.ProcessorAffinity = (IntPtr)(1L << 1);
        }
    }
}
