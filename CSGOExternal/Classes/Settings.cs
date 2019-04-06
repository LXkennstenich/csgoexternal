using System;
using System.IO;
using System.Windows.Input;

namespace CSGOExternal.Classes
{
    internal class Settings
    {
        internal static IniConfigUtils configUtils = new IniConfigUtils();

        internal static int GetEnemyColorRed()
        {
            return Convert.ToInt32(configUtils.GetValue("EnemyColorRed"));
        }

        internal static int GetEnemyColorGreen()
        {
            return Convert.ToInt32(configUtils.GetValue("EnemyColorGreen"));
        }

        internal static int GetEnemyColorBlue()
        {
            return Convert.ToInt32(configUtils.GetValue("EnemyColorBlue"));
        }

        internal static int GetTeamColorRed()
        {
            return Convert.ToInt32(configUtils.GetValue("TeamColorRed"));
        }

        internal static int GetTeamColorGreen()
        {
            return Convert.ToInt32(configUtils.GetValue("TeamColorGreen"));
        }

        internal static int GetTeamColorBlue()
        {
            return Convert.ToInt32(configUtils.GetValue("TeamColorBlue"));
        }

        internal static bool GetDrawEnemies()
        {
            return Convert.ToBoolean(configUtils.GetValue("DrawEnemies"));
        }

        internal static bool GetDrawTeammates()
        {
            return Convert.ToBoolean(configUtils.GetValue("DrawTeammates"));
        }

        internal static bool GetDrawBomb()
        {
            return Convert.ToBoolean(configUtils.GetValue("DrawBomb"));
        }

        internal static bool GetDrawChicken()
        {
            return Convert.ToBoolean(configUtils.GetValue("DrawChicken"));
        }

        internal static int GetTriggerDelay()
        {
            return Convert.ToInt32(configUtils.GetValue("TriggerDelay"));
        }

        internal static Key GetTriggerKey()
        {
            return (Key)Convert.ToInt32(configUtils.GetValue("TriggerKey"));
        }

        internal static bool GetTriggerTeam()
        {
            return Convert.ToBoolean(configUtils.GetValue("TriggerTeam"));
        }

        internal static bool GetTriggerAuto()
        {
            return Convert.ToBoolean(configUtils.GetValue("TriggerAuto"));
        }

        internal static int GetAimbotFOV()
        {
            return Convert.ToInt32(configUtils.GetValue("AimbotFOV"));
        }

        internal static int GetAimbotSmooth()
        {
            return Convert.ToInt32(configUtils.GetValue("AimbotSmooth"));
        }

        internal static Key GetAimbotKey()
        {
            return (Key)Convert.ToInt32(configUtils.GetValue("AimbotKey"));
        }

        internal static bool GetAimAuto()
        {
            return Convert.ToBoolean(configUtils.GetValue("AimbotAuto"));
        }

        internal static int GetAimbotBone()
        {
            return Convert.ToInt32(configUtils.GetValue("AimbotBone"));
        }

        internal static bool GetClRender() {
            return Convert.ToBoolean(configUtils.GetValue("ClRender"));
        }

        internal static bool GetAimOnClosestPlayer() {
            return Convert.ToBoolean(configUtils.GetValue("AimOnClosestPlayer"));
        }

        internal static bool GetAimOnClosestToCrosshair() {
            return Convert.ToBoolean(configUtils.GetValue("AimOnClosestToCrosshair"));
        }

        internal static bool GetVisibilityCheck() {
            return Convert.ToBoolean(configUtils.GetValue("VisiblityCheck"));
        }

        internal static Key GetBunnyhopKey() {
            return (Key)configUtils.GetValue("BunnyhopKey");
        }

        internal static bool GetBunnyhopLegit() {
            return Convert.ToBoolean(configUtils.GetValue("BunnyhopLegit"));
        }

        internal static void SetAimbotFOV(int value)
        {
            configUtils.SetValue("AimbotFOV", value);
        }

        internal static void SetAimbotSmooth(int value)
        {
            configUtils.SetValue("AimbotSmooth", value);
        }

        internal static void SetAimbotKey(Key key)
        {
            foreach (int keyValue in Enum.GetValues(typeof(Key)))
            {
                if (Enum.GetName(typeof(Key), key) == Enum.GetName(typeof(Key), keyValue))
                {
                    configUtils.SetValue("AimbotKey", keyValue);
                }
            }
        }

        internal static void SetAimAuto(bool value)
        {
            configUtils.SetValue("AimbotAuto", value);
        }

        internal static void SetAimbotBone(int value)
        {
            configUtils.SetValue("AimbotBone", value);
        }

        internal static void SetEnemyColorRed(int value)
        {
            configUtils.SetValue("EnemyColorRed", value);
        }

        internal static void SetEnemyColorGreen(int value)
        {
            configUtils.SetValue("EnemyColorGreen", value);
        }

        internal static void SetEnemyColorBlue(int value)
        {
            configUtils.SetValue("EnemyColorBlue", value);
        }

        internal static void SetTeamColorRed(int value)
        {
            configUtils.SetValue("TeamColorRed", value);
        }

        internal static void SetTeamColorGreen(int value)
        {
            configUtils.SetValue("TeamColorGreen", value);
        }

        internal static void SetTeamColorBlue(int value)
        {
            configUtils.SetValue("TeamColorBlue", value);
        }

        internal static void SetDrawEnemies(bool value)
        {
            configUtils.SetValue("DrawEnemies", value);
        }

        internal static void SetDrawTeammates(bool value)
        {
            configUtils.SetValue("DrawTeammates", value);
        }

        internal static void SetDrawBomb(bool value)
        {
            configUtils.SetValue("DrawBomb", value);
        }

        internal static void SetDrawChicken(bool value)
        {
            configUtils.SetValue("DrawChicken", value);
        }

        internal static void SetTriggerDelay(int value)
        {
            configUtils.SetValue("TriggerDelay", value);
        }

        internal static void SetTriggerKey(Key key)
        {
            
            foreach (int keyValue in Enum.GetValues(typeof(Key)))
            {
                if(Enum.GetName(typeof(Key),key) == Enum.GetName(typeof(Key),keyValue))
                {
                    configUtils.SetValue("TriggerKey", keyValue);
                }
            }
        }

        internal static void SetTriggerTeam(bool value)
        {
            configUtils.SetValue("TriggerTeam", value);
        }

        internal static void SetTriggerAuto(bool value)
        {
            configUtils.SetValue("TriggerAuto", value);
        }

        internal static void SetClRender(bool value) {
            configUtils.SetValue("ClRender", value);
        }

        internal static void SetAimOnClosestPlayer(bool value) {
            configUtils.SetValue("AimOnClosestPlayer", value);
        }

        internal static void SetAimOnClosestToCrosshair(bool value) {
            configUtils.SetValue("AimOnClosestToCrosshair", value);
        }

        internal static void SetVisibilityCheck(bool value) {
            configUtils.SetValue("VisiblityCheck", value);
        }

        internal static void SetBunnyhopKey(Key key) {
            foreach (int keyValue in Enum.GetValues(typeof(Key))) {
                if (Enum.GetName(typeof(Key), key) == Enum.GetName(typeof(Key), keyValue)) {
                    configUtils.SetValue("BunnyhopKey", keyValue);
                }
            }
        }

        internal static void SetBunnyhopLegit(bool value) {
            configUtils.SetValue("BunnyhopLegit", value);
        }

        internal static void UpdateSettings(bool create = false)
        {
            FileStream fileStream = null;

            try
            {
                if(create)
                {
                    fileStream = File.OpenWrite(Environment.CurrentDirectory + "\\config.dat");
                    SetEnemyColorRed(255);
                    SetEnemyColorGreen(0);
                    SetEnemyColorBlue(0);
                    SetTeamColorRed(0);
                    SetTeamColorGreen(255);
                    SetTeamColorBlue(0);
                    SetDrawEnemies(true);
                    SetDrawTeammates(false);
                    SetDrawChicken(false);
                    SetDrawBomb(false);
                    SetTriggerAuto(false);
                    SetTriggerDelay(10);
                    SetTriggerTeam(false);
                    SetTriggerKey(Key.LeftAlt);
                    SetAimAuto(false);
                    SetAimbotFOV(2);
                    SetAimbotBone(10);
                    SetAimbotKey(Key.E);
                    SetAimbotSmooth(10);
                    SetAimbotBone(10);
                    SetAimOnClosestPlayer(false);
                    SetAimOnClosestToCrosshair(true);
                    SetVisibilityCheck(true);
                    SetBunnyhopLegit(true);
                    SetBunnyhopKey(Key.Space);
                    SetClRender(false);
                }
                else
                {
                    fileStream = File.OpenWrite(Environment.CurrentDirectory + "\\config.dat");
                }

                fileStream.Lock(0, fileStream.Length);
                byte[] data = configUtils.SaveSettings();
                fileStream.Write(data, 0, data.Length);

            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
        }

        internal static void Init()
        {
            if (!File.Exists(Environment.CurrentDirectory + "\\config.dat"))
            {
                UpdateSettings(true);
            }

            ReadSettings();
        }

        internal static void ReadSettings()
        {
            configUtils.ReadingSettingEvent += ConfigUtils_ReadingSettingEvent1;
            configUtils.ReadSettings(File.ReadAllBytes(Environment.CurrentDirectory + "\\config.dat"));
        }

        private static void ConfigUtils_ReadingSettingEvent1(object sender, IniConfigUtils.ReadingSettingEventArgs e)
        {
            configUtils.SetValue(e.Name, e.Value);
        }
    }
}
