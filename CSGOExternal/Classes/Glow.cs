using System;

namespace CSGOExternal.Classes
{
    internal struct GlowStruct
    {
        public float r;
        public float g;
        public float b;
        public float a;
        public bool rwo;
        public bool rwuo;
    }

    internal static class Glow
    {
        internal static GlowStruct glowstruct = new GlowStruct
        {
            r = 0,
            g = 0,
            b = 0,
            a = 1,
            rwo = true,
            rwuo = false
        };

        internal static Entity entity = new Entity();

        internal static void Render()
        {
            if (Memory.ReadInt(Offsets.clientState + Offsets.dwClientState_State) == 6)
            {
                //glowpointer
                IntPtr glowManager = Memory.ReadIntPtr(Offsets.clientDllBaseAddress + Offsets.dwGlowObjectManager);

                //objekte die mit dem glow beschrieben werden können
                int glowObjectCount = Memory.ReadInt(Offsets.clientDllBaseAddress + Offsets.dwGlowObjectManager + 0x4);

                for (int entityIndex = 0; entityIndex < glowObjectCount; entityIndex++)
                {
                    //entity offset setzen
                    entity.Offset = Memory.ReadIntPtr(glowManager + (entityIndex * 0x38));

                    int classID = entity.GetClassID();
                    bool isEnemy = entity.IsEnemy();
                    bool isAlive = entity.IsAlive();
                    bool drawTeammates = Settings.GetDrawTeammates();
                    bool drawEnemies = Settings.GetDrawEnemies();
                    bool drawBomb = Settings.GetDrawBomb();

                    // teammates
                    if (!entity.IsDormant() && !isEnemy && classID == ClassID.CCSPLayer && isAlive && drawTeammates)
                    {
                        //eventuell könnte man hier für das leben der spieler nochmal extra farben definieren ?!

                        glowstruct.r = Settings.GetTeamColorRed();
                        glowstruct.g = Settings.GetTeamColorGreen();
                        glowstruct.b = Settings.GetTeamColorBlue();
                        DrawGlow(glowManager, entityIndex, glowstruct);
                    }
                    // gegner
                    else if (!entity.IsDormant() && isEnemy && classID == ClassID.CCSPLayer && isAlive && drawEnemies)
                    {
                        glowstruct.r = Settings.GetEnemyColorRed();
                        glowstruct.g = Settings.GetEnemyColorGreen();
                        glowstruct.b = Settings.GetEnemyColorBlue();
                        DrawGlow(glowManager, entityIndex, glowstruct);
                    }
                    // bombe
                    else if ((classID == ClassID.Bomb || classID == ClassID.BombPlanted) && drawBomb)
                    {
                        glowstruct.r = 0;
                        glowstruct.g = 0;
                        glowstruct.b = 255;
                        DrawGlow(glowManager, entityIndex, glowstruct);
                    }
                }
            }
        }

        static void DrawGlow(IntPtr glowpointer, int glowindex, GlowStruct glowstruct)
        {
            Memory.WriteFloat(glowpointer + (glowindex * 0x38) + 4, glowstruct.r);
            Memory.WriteFloat(glowpointer + (glowindex * 0x38) + 8, glowstruct.g);
            Memory.WriteFloat(glowpointer + (glowindex * 0x38) + 12, glowstruct.b);
            Memory.WriteFloat(glowpointer + (glowindex * 0x38) + 0x10, glowstruct.a);
            Memory.WriteBool(glowpointer + (glowindex * 0x38) + 0x24, glowstruct.rwo);
            Memory.WriteBool(glowpointer + (glowindex * 0x38) + 0x25, glowstruct.rwuo);
        }
    }
}

