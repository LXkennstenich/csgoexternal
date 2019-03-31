using System.Runtime.InteropServices;

namespace CSGOExternal.Classes {
    internal class NativeMethods {
        private NativeMethods() { }

        [DllImport("User32.dll")]
        internal static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey); // Keys enumeration

        [DllImport("User32.dll")]
        internal static extern short GetAsyncKeyState(System.Int32 vKey);

        [DllImport("user32.dll")]
        internal static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData,int dwExtraInfo);
    }
}
