using System;

namespace CSGOExternal.Classes
{
    internal class Entity
    {
        internal IntPtr Offset;

        internal int GetCrosshairID()
        {
            return Memory.ReadInt(Offset + Offsets.m_iCrosshairId);
        }

        internal int GetHealth()
        {
            return Memory.ReadInt(Offset + Offsets.m_iHealth);
        }

        internal int GetTeam()
        {
            return Memory.ReadInt(Offset + Offsets.m_iTeamNum);
        }

        internal bool IsInGame()
        {
            return Memory.ReadInt(Offsets.clientState + Offsets.dwClientState_State) == 6 ? true : false;
        }

        internal int GetMaxPlayers()
        {
            return Memory.ReadInt(Offsets.clientState + Offsets.dwClientState_MaxPlayer);
        }

        internal Vector3 GetVectorVelocity()
        {
            return Memory.ReadVector3(Offset + Offsets.m_vecVelocity);
        }

        internal Vector3 GetVectorOrigin()
        {
            return Memory.ReadVector3(Offset + Offsets.m_vecOrigin);
        }

        internal Vector3 GetVectorViewOffset()
        {
            return Memory.ReadVector3(Offset + Offsets.m_vecViewOffset);
        }

        internal bool IsAlive()
        {
            int lifestate = Memory.ReadInt(Offset + Offsets.m_lifeState);

            return GetHealth() > 0 && lifestate == 0;
        }

        internal bool IsDormant()
        {
            return Convert.ToBoolean(Memory.ReadInt(Offset + Offsets.m_bDormant));
        }

        internal bool IsSpottedByMask() {
            return Convert.ToBoolean(Memory.ReadInt(Offset + Offsets.m_bSpottedByMask));
        }

        internal bool IsSpotted()
        {
            return Convert.ToBoolean(Memory.ReadInt(Offset + Offsets.m_bSpotted));
        }

        internal bool IsEnemy()
        {
            return GetLocalplayerTeam() != GetTeam() ? true : false;
        }

        internal int GetWeaponInHand()
        {
            int currentWeaponID = Memory.ReadInt(Offset + Offsets.m_hActiveWeapon);
            currentWeaponID &= 0xFFF;

            IntPtr weaponentity = Memory.ReadIntPtr(Offsets.clientDllBaseAddress + Offsets.dwEntityList + (currentWeaponID - 1) * 16);
            int weaponindex = Memory.ReadInt(weaponentity + Offsets.m_iItemDefinitionIndex);

            if(weaponindex == WeaponID.WEAPON_DECOY 
               || weaponindex == WeaponID.WEAPON_FLASHBANG
               || weaponindex == WeaponID.WEAPON_SMOKEGRENADE
               || weaponindex == WeaponID.WEAPON_MOLOTOV
               || weaponindex == WeaponID.WEAPON_HEGRENADE
               || weaponindex == WeaponID.WEAPON_INCGRENADE)
            {
                return WeaponID.IS_GRENADE;
            }
            else if(weaponindex == WeaponID.WEAPON_M4A1
                || weaponindex == WeaponID.WEAPON_FAMAS
                || weaponindex == WeaponID.WEAPON_GALILAR
                || weaponindex == WeaponID.WEAPON_AK47)
            {
                return WeaponID.IS_RIFLE;
            }
            else if(weaponindex == WeaponID.WEAPON_USP_SILENCER
                || weaponindex == WeaponID.WEAPON_HKP2000
                || weaponindex == WeaponID.WEAPON_P250
                || weaponindex == WeaponID.WEAPON_GLOCK
                || weaponindex == WeaponID.WEAPON_FIVESEVEN
                || weaponindex == WeaponID.WEAPON_DEAGLE
                || weaponindex == WeaponID.WEAPON_ELITE
                || weaponindex == WeaponID.WEAPON_TEC9
                || weaponindex == WeaponID.WEAPON_CZ75A)
            {
                return WeaponID.IS_PISTOL;
            }
            else if(weaponindex == WeaponID.WEAPON_AWP || weaponindex == WeaponID.WEAPON_SSG08)
            {
                return WeaponID.IS_RIFLE;
            }
            else if(weaponindex == WeaponID.WEAPON_XM1014 || weaponindex == WeaponID.WEAPON_MAG7 || weaponindex == WeaponID.WEAPON_SAWEDOFF || weaponindex == WeaponID.WEAPON_NOVA)
            {
                return WeaponID.IS_SHOTGUN;
            }
            else
            {
                return WeaponID.WEAPON_NONE;
            }


        }

        internal int GetFlag()
        {
            return Memory.ReadInt(Offset + Offsets.m_fFlags);
        }

        internal int GetLocalplayerTeam()
        {
            IntPtr localplayer = Memory.ReadIntPtr(Offsets.clientDllBaseAddress + Offsets.dwLocalPlayer);
            return Memory.ReadInt(localplayer + Offsets.m_iTeamNum);
        }

        internal float GetFlashDuration()
        {
            return Memory.ReadFloat(Offset + Offsets.m_flFlashDuration);
        }

        internal int GetClassID()
        {
            IntPtr IClientNetworkable = Memory.ReadIntPtr(Offset + 0x8);
            IntPtr GetClientClass = Memory.ReadIntPtr(IClientNetworkable + 0x8);
            IntPtr pClientClass = Memory.ReadIntPtr(GetClientClass + 0x1);
            int classID = Memory.ReadInt(pClientClass + 0x14);

            return classID;
        }

        internal int GetCompetitiveRank(int i)
        {
            IntPtr playerresource = Memory.ReadIntPtr(Offsets.clientDllBaseAddress + Offsets.dwPlayerResource);

            return Memory.ReadInt(playerresource + Offsets.m_iCompetitiveRanking + i * 4);
        }

        internal int GetCompetitiveWins(int i)
        {
            IntPtr playerresource = Memory.ReadIntPtr(Offsets.clientDllBaseAddress + Offsets.dwPlayerResource);

            return Memory.ReadInt(playerresource + Offsets.m_iCompetitiveWins + i * 4);
        }
    }
}
