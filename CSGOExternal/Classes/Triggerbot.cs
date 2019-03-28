using System.Threading;
using System.Windows.Input;

namespace CSGOExternal.Classes
{
    internal static class Triggerbot
    {
        internal static Entity entity = new Entity();

        internal static void Run()
        {
            if ((NativeMethods.GetAsyncKeyState(KeyInterop.VirtualKeyFromKey(Settings.GetTriggerKey())) & 0x8000 ) != 0 || Settings.GetTriggerAuto())
            {
                Entity localplayer = Triggerbot.entity;
                localplayer.Offset = Memory.ReadIntPtr(Offsets.clientDllBaseAddress + Offsets.dwLocalPlayer);
                int crosshairid = localplayer.GetCrosshairID();

                Entity entity = Triggerbot.entity;
                entity.Offset = Memory.ReadIntPtr(Offsets.clientDllBaseAddress + Offsets.dwEntityList + (crosshairid - 1) * 0x10);

                if (entity.IsEnemy() && entity.GetHealth() > 0 || Settings.GetTriggerTeam() && entity.GetHealth() > 0)
                {
                    int delay = Settings.GetTriggerDelay() == 0 ? 1 : Settings.GetTriggerDelay();

                    Memory.WriteBool(localplayer.Offset + Offsets.dwForceAttack, true);
                    Thread.Sleep(delay);
                    Memory.WriteBool(localplayer.Offset + Offsets.dwForceAttack, false);
                }
            }
        }
    }
}
