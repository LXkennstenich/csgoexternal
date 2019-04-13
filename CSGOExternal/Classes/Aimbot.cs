using System;
using System.Threading;
using System.Windows.Input;

namespace CSGOExternal.Classes {

    internal static class Aimbot {

        private static Entity possibleEntity = new Entity();
        private static Entity localplayer = new Entity();
        private static Entity entity = new Entity();
        private static Vector3 MyEyeLocation = Vector3.Zero;
        private static Vector3 MyLocation = Vector3.Zero;

        internal static void Run() {

            if ((NativeMethods.GetAsyncKeyState(KeyInterop.VirtualKeyFromKey(Settings.GetAimbotKey())) & 0x8000) != 0) {

                localplayer.Offset = Memory.ReadIntPtr(Offsets.clientDllBaseAddress + Offsets.dwLocalPlayer);

                if (localplayer.IsInGame()) {
                    MyLocation = localplayer.GetVectorOrigin();
                    Vector3 origin = localplayer.GetVectorViewOffset();
                    MyEyeLocation = MyLocation + origin;

                    Vector3 viewangles = Memory.ReadVector3(Offsets.clientState + Offsets.dwClientState_ViewAngles);

                    Vector3 punch = Vector3.Zero;

                    if (localplayer.GetWeaponInHand() == WeaponID.IS_PISTOL ||
                        localplayer.GetWeaponInHand() == WeaponID.IS_SMG ||
                        localplayer.GetWeaponInHand() == WeaponID.IS_SHOTGUN ||
                        localplayer.GetWeaponInHand() == WeaponID.IS_SNIPER ||
                        localplayer.GetWeaponInHand() == WeaponID.IS_RIFLE) {

                        Vector3 target = FindClosestEntity(localplayer,viewangles);

                        if(target == Vector3.Zero) {
                            return;
                        }

                        target = MathUtils.CalcAngle(MyEyeLocation, target);

                        target = MathUtils.ClampAngle(target);

                        Vector2 screenSize = Vector2.Zero;
                        Vector2 screenCenter = Vector2.Zero;
                        screenSize.X = 2560;
                        screenSize.Y = 1080;
                        screenCenter.X = screenSize.X / 2;
                        screenCenter.Y = screenSize.Y / 2;

                        Vector2 vangles = Vector2.Zero;
                        Vector2 eangles = Vector2.Zero;

                        vangles.X = viewangles.X;
                        vangles.Y = viewangles.Y;

                        eangles.X = target.X;
                        eangles.Y = target.Y;
                        
                        if (MathUtils.PointInCircle(eangles, screenCenter, Settings.GetAimbotFOV())) {
                            Memory.WriteFloat(Offsets.clientState + Offsets.dwClientState_ViewAngles, target.X);
                            Memory.WriteFloat(Offsets.clientState + Offsets.dwClientState_ViewAngles + 0x4, target.Y);

                            if (Settings.GetTriggerAuto()) {
                                int delay = Settings.GetTriggerDelay() == 0 ? 1 : Settings.GetTriggerDelay();

                                NativeMethods.mouse_event(0x0002, 0, 0, 0, 0);
                                Thread.Sleep(delay);
                                NativeMethods.mouse_event(0x0004, 0, 0, 0, 0);
                            }
                        }
                    }
                }
            }
        }

        private static Vector3 FindClosestEntity(Entity localplayer,Vector3 viewangles) {
            Vector3 target = Vector3.Zero;
            float final = 9999;
            int closestentity = 0;
            float closest = 9999;

            for (int i = 0; i < localplayer.GetMaxPlayers(); i++) {
                possibleEntity.Offset = Memory.ReadIntPtr(Offsets.clientDllBaseAddress + Offsets.dwEntityList + (i - 1) * 0x10);

                bool isEnemy = possibleEntity.IsEnemy();
                bool isAlive = possibleEntity.IsAlive();
                bool isDormant = possibleEntity.IsDormant();
                int classID = possibleEntity.GetClassID();

                if (!isDormant && isEnemy && classID == ClassID.CCSPLayer && isAlive) {

                    IntPtr bonebase = Memory.ReadIntPtr(possibleEntity.Offset + Offsets.m_dwBoneMatrix);

                    if (Settings.GetAimOnClosestToCrosshair()) {
                        Vector3 bones = Vector3.Zero;
                        bones.X = Memory.ReadFloat(bonebase + 0x30 * Settings.GetAimbotBone() + 0x0C);
                        bones.Y = Memory.ReadFloat(bonebase + 0x30 * Settings.GetAimbotBone() + 0x1C);
                        bones.Z = Memory.ReadFloat(bonebase + 0x30 * Settings.GetAimbotBone() + 0x1C);
                        Matrix viewmatrix = new Matrix(4, 4);
                        viewmatrix = Memory.ReadMatrix(Offsets.engineDllBaseAddress + Offsets.dwViewMatrix);
                        Vector2 screenSize = Vector2.Zero;
                        Vector2 screenCenter = Vector2.Zero;

                        screenSize.X = 2560;
                        screenSize.Y = 1080;

                        screenCenter.X = screenSize.X / 2;
                        screenCenter.Y = screenSize.Y / 2;

                        Vector2 enemy = MathUtils.WorldToScreen(viewmatrix, screenSize, bones);

                        final = screenCenter.DistanceTo(enemy);
                    } else if(Settings.GetAimOnClosestPlayer()) {
                        Vector3 enemylocation = possibleEntity.GetVectorOrigin();
                        final = MyLocation.DistanceTo(enemylocation);
                    } 

                    if(Settings.GetVisibilityCheck()) {
                        if (final < closest && possibleEntity.IsSpottedByMask()) {
                            closest = final;
                            closestentity = i;
                            target.X = Memory.ReadFloat(bonebase + 0x30 * Settings.GetAimbotBone() + 0x0C);
                            target.Y = Memory.ReadFloat(bonebase + 0x30 * Settings.GetAimbotBone() + 0x1C);
                            target.Z = Memory.ReadFloat(bonebase + 0x30 * Settings.GetAimbotBone() + 0x2C);
                        }
                    } else {
                        if (final < closest) {
                            closest = final;
                            closestentity = i;
                            target.X = Memory.ReadFloat(bonebase + 0x30 * Settings.GetAimbotBone() + 0x0C);
                            target.Y = Memory.ReadFloat(bonebase + 0x30 * Settings.GetAimbotBone() + 0x1C);
                            target.Z = Memory.ReadFloat(bonebase + 0x30 * Settings.GetAimbotBone() + 0x2C);
                        }
                    } 
                }
            }

            return target;
        }
    }
}
