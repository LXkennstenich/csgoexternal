using System;
using System.Windows.Input;

namespace CSGOExternal.Classes {
    internal static class Aimbot {
        static Entity localplayer = new Entity();
        static Entity entity = new Entity();
        static Vector3 MyEyeLocation = Vector3.Zero;
        static Vector3 MyLocation = Vector3.Zero;

        internal static void Run() {
            if ((NativeMethods.GetAsyncKeyState(KeyInterop.VirtualKeyFromKey(Key.LeftAlt)) & 0x8000) != 0) {
                localplayer.Offset = Memory.ReadIntPtr(Offsets.clientDllBaseAddress + Offsets.dwLocalPlayer);

                if (localplayer.IsInGame()) {
                    MyLocation = localplayer.GetVectorOrigin();
                    Vector3 origin = localplayer.GetVectorViewOffset();
                    MyEyeLocation = MyLocation + origin;

                    Vector3 viewangles = Memory.ReadVector3(Offsets.clientState + Offsets.dwClientState_ViewAngles);
                    int entityindex = FindClosestEntity(localplayer, viewangles);

                    if(entityindex == 0) {
                        return;
                    }

                    entity.Offset = Memory.ReadIntPtr(Offsets.clientDllBaseAddress + Offsets.dwEntityList + (entityindex - 1) * 0x10);

                    IntPtr bonebase = Memory.ReadIntPtr(entity.Offset + Offsets.m_dwBoneMatrix);

                    Vector3 bonesenemy = Vector3.Zero;

                    bonesenemy.X = Memory.ReadFloat(bonebase + 0x30 * Settings.GetAimbotBone() + 0x0C);
                    bonesenemy.Y = Memory.ReadFloat(bonebase + 0x30 * Settings.GetAimbotBone() + 0x1C);
                    bonesenemy.Z = Memory.ReadFloat(bonebase + 0x30 * Settings.GetAimbotBone() + 0x2C);

                    Vector3 punch = Vector3.Zero;

                    if (localplayer.GetWeaponInHand() == WeaponID.IS_PISTOL || localplayer.GetWeaponInHand() == WeaponID.IS_SMG || localplayer.GetWeaponInHand() == WeaponID.IS_SHOTGUN || localplayer.GetWeaponInHand() == WeaponID.IS_SNIPER || localplayer.GetWeaponInHand() == WeaponID.IS_RIFLE) {
                        if (localplayer.GetWeaponInHand() == WeaponID.IS_RIFLE || localplayer.GetWeaponInHand() == WeaponID.IS_SMG) {
                            punch = Memory.ReadVector3(Offsets.clientDllBaseAddress + Offsets.m_aimPunchAngle);
                        }

                        Vector3 target = MathUtils.CalcAngle(MyEyeLocation, bonesenemy);
                        Vector3 smooth = Vector3.Zero;

                        target = MathUtils.ClampAngle(target);

                        Vector2 vangles = Vector2.Zero;
                        Vector2 eangles = Vector2.Zero;

                        vangles.X = viewangles.X;
                        vangles.Y = viewangles.Y;

                        eangles.X = target.X;
                        eangles.Y = target.Y;

                        if (MathUtils.PointInCircle(eangles, vangles, Settings.GetAimbotFOV())) {
                            Memory.WriteFloat(Offsets.clientState + Offsets.dwClientState_ViewAngles, target.X);
                            Memory.WriteFloat(Offsets.clientState + Offsets.dwClientState_ViewAngles + 0x4, target.Y);
                        }
                    }
                }
            }
        }

        internal static int FindClosestEntity(Entity localplayer, Vector3 viewangles) {
            float final;
            int closestentity = 0;
            Vector3 calculated = Vector3.Zero;

            float closest = 9999;

            for (int i = 0; i < localplayer.GetMaxPlayers(); i++) {
                Entity possibleEntity = entity;
                possibleEntity.Offset = Memory.ReadIntPtr(Offsets.clientDllBaseAddress + Offsets.dwEntityList + (i - 1) * 0x10);

                bool isEnemy = possibleEntity.IsEnemy();
                bool isAlive = possibleEntity.IsAlive();
                bool isDormant = possibleEntity.IsDormant();
                int classID = possibleEntity.GetClassID();

                if (!isDormant && isEnemy && classID == ClassID.CCSPLayer && isAlive) {
                    Vector3 enemylocation = possibleEntity.GetVectorOrigin();
                    final = MyLocation.DistanceTo(enemylocation);

                    if (final < closest) {
                        closest = final;
                        closestentity = i;
                    }
                }

            }

            return closestentity;
        }
    }
}
