﻿using System.Threading;
using System.Windows.Input;

namespace CSGOExternal.Classes
{
    internal static class Bunnyhop
    {
        internal static Entity entity = new Entity();

        internal static void Run()
        {
            Entity localplayer = entity;
            localplayer.Offset = Memory.ReadIntPtr(Offsets.clientDllBaseAddress + Offsets.dwLocalPlayer);

            if(localplayer.IsInGame())
            {
                if((NativeMethods.GetAsyncKeyState(KeyInterop.VirtualKeyFromKey(Key.Space)) & 0x8000) != 0)
                {
                    bool isAlive = localplayer.IsAlive();

                    if (localplayer.GetFlag() == 257 && isAlive)
                    {
                        Memory.WriteInt(localplayer.Offset + Offsets.dwForceJump, 6);
                        Thread.Sleep(3);
                        Memory.WriteInt(localplayer.Offset + Offsets.dwForceJump, 5);
                    }
                }
            }
        }
    }
}