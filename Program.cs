using System;
using System.Diagnostics;

namespace VoiceMeeterPriority
{
    internal class Program
    {
        static void Main()
        {
            Process audiodg = Process.GetProcessesByName("audiodg")[0];
            audiodg.ProcessorAffinity = (IntPtr)(1L << 1);
            audiodg.PriorityClass = ProcessPriorityClass.High;
            foreach (ProcessThread thread in audiodg.Threads)
                thread.ProcessorAffinity = (IntPtr)(1L << 1);

        }
    }
}
