
namespace CSGOExternal.Classes
{
    internal struct cclRender
    {
        internal byte r;
        internal byte g;
        internal byte b;
    };

    class Radar
    {
        internal static Entity entity = new Entity();
        internal static cclRender cclRender = new cclRender
        {
            r = 0,
            g = 0,
            b = 0
        };

        internal static void Run()
        {
            Entity localplayer = entity;
            localplayer.Offset = Memory.ReadIntPtr(Offsets.clientDllBaseAddress + Offsets.dwLocalPlayer);

            if (localplayer.IsInGame())
            {
                for (int entityIndex = 0; entityIndex < localplayer.GetMaxPlayers(); entityIndex++)
                {
                    //entity offset setzen
                    entity.Offset = Memory.ReadIntPtr(Offsets.clientDllBaseAddress + Offsets.dwEntityList + (entityIndex - 1) * 0x10);

                    //klassen id des entitys holen
                    int classID = entity.GetClassID();
                    bool isEnemy = entity.IsEnemy();
                    bool isPlayer = classID == ClassID.CCSPLayer ? true : false;
                    bool isAlive = entity.IsAlive();
                    bool isDormant = entity.IsDormant();
                    bool isSpotted = entity.IsSpotted();

                    if (!isPlayer)
                    {
                        continue;
                    }

                    if (isAlive && !isDormant && isEnemy && Settings.GetClRender())
                    {
                        cclRender.r = 255;
                        cclRender.g = 0;
                        cclRender.b = 0;

                        Memory.WriteByte(entity.Offset + Offsets.m_clrRender, cclRender.r);
                        Memory.WriteByte(entity.Offset + Offsets.m_clrRender + 1, cclRender.g);
                        Memory.WriteByte(entity.Offset + Offsets.m_clrRender + 2, cclRender.b);

                    }

                    if (isAlive && isEnemy && !isSpotted)
                    {
                        Memory.WriteBool(entity.Offset + Offsets.m_bSpotted, true);
                    }
                }
            }
        }
    }
}

