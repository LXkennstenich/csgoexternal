using System.Windows.Input;

namespace CSGOExternal.Classes {

    internal static class Bunnyhop {

        internal static Entity entity = new Entity();

        internal static void Run() {

            Entity localplayer = entity;
            localplayer.Offset = Memory.ReadIntPtr(Offsets.clientDllBaseAddress + Offsets.dwLocalPlayer);

            if (localplayer.IsInGame()) {

                if ((NativeMethods.GetAsyncKeyState(KeyInterop.VirtualKeyFromKey(Settings.GetBunnyhopKey())) & 0x8000) != 0) {

                    bool isAlive = localplayer.IsAlive();
                    bool inAir = localplayer.InAir();

                    if (!isAlive) {
                        return;
                    }

                    if (!inAir) {
                        Memory.WriteInt(Offsets.clientDllBaseAddress + Offsets.dwForceJump, 5);
                    } else if (inAir) {
                        Memory.WriteInt(Offsets.clientDllBaseAddress + Offsets.dwForceJump, 4);
                        Memory.WriteInt(Offsets.clientDllBaseAddress + Offsets.dwForceJump, 5);
                        Memory.WriteInt(Offsets.clientDllBaseAddress + Offsets.dwForceJump, 4);
                    } else {
                        Memory.WriteInt(Offsets.clientDllBaseAddress + Offsets.dwForceJump, 4);
                    }

                }

            }

        }

    }

}
