using System.Runtime.InteropServices;

namespace CSGOExternal.Classes
{
    internal class NativeMethods
    {
        private NativeMethods() { }

        [DllImport("User32.dll")]
        internal static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey); // Keys enumeration

        [DllImport("User32.dll")]
        internal static extern short GetAsyncKeyState(System.Int32 vKey);
    }
}
