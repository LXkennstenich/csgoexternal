using ProcessMemoryWrapper;
using System;
using System.Diagnostics;

namespace CSGOExternal.Classes
{
    internal static class Memory
    {
        internal static IntPtr numberOfBytesWritten;
        internal static IntPtr Handle;

        internal static void Init()
        {
            Process csgo = Process.GetProcessesByName("csgo")[0];
            Handle = ProcessWrapper.OpenProcess(ProcessAccessFlags.All, csgo.Id);

            foreach(ProcessModule processModule in csgo.Modules)
            {
                if(processModule.FileName.Contains("client_panorama.dll"))
                {
                    Offsets.clientDllBaseAddress = processModule.BaseAddress;
                }

                if (processModule.FileName.Contains("engine.dll"))
                {
                    Offsets.engineDllBaseAddress = processModule.BaseAddress;
                }
            }

            Offsets.clientState = Memory.ReadIntPtr(Offsets.engineDllBaseAddress + Offsets.dwClientState);
        }

        internal static IntPtr ReadIntPtr(IntPtr address)
        {
            return ProcessWrapper.ReadProcessMemory<IntPtr>(Handle, address);
        }

        internal static int ReadInt(IntPtr address)
        {
            return ProcessWrapper.ReadProcessMemory<int>(Handle, address);
        }

        internal static bool ReadBool(IntPtr address)
        {
            return ProcessWrapper.ReadProcessMemory<bool>(Handle, address);
        }

        internal static float ReadFloat(IntPtr address)
        {
            return ProcessWrapper.ReadProcessMemory<float>(Handle, address);
        }

        internal static Vector3 ReadVector3(IntPtr address)
        {
            return ProcessWrapper.ReadProcessMemory<Vector3>(Handle, address);
        }

        internal static void WriteByte(IntPtr address, byte value)
        {
            ProcessWrapper.WriteProcessMemory<byte>(Handle, address, value, ref numberOfBytesWritten);
        }

        internal static void WriteInt(IntPtr address,int value)
        {
            ProcessWrapper.WriteProcessMemory<int>(Handle, address, value, ref numberOfBytesWritten);
        }

        internal static void WriteBool(IntPtr address, bool value)
        {
            ProcessWrapper.WriteProcessMemory<bool>(Handle, address, value, ref numberOfBytesWritten);
        }

        internal static void WriteFloat(IntPtr address, float value)
        {
            ProcessWrapper.WriteProcessMemory<float>(Handle, address, value, ref numberOfBytesWritten);
        }

        internal static void WriteVector3(IntPtr address, Vector3 value)
        {
            ProcessWrapper.WriteProcessMemory<Vector3>(Handle, address, value, ref numberOfBytesWritten);
        }
    }
}