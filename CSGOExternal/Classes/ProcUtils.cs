using System.Diagnostics;

namespace CSGOExternal.Classes
{
    public class ProcUtils
    {
        /// <summary>
        /// Returns whether a specific process is running
        /// </summary>
        /// <param name="name">The name of the process</param>
        /// <returns></returns>
        public static bool ProcessIsRunning(string name)
        {
            return Process.GetProcessesByName(name).Length > 0 ? true : false;
        }

    }
}
