using CSGOExternal.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace CSGOExternal {

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        readonly Dictionary<string, int> keys = new Dictionary<string, int>();
        DispatcherTimer _updateUIThread = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
        Thread _espThread = new Thread(ESPThread);
        Thread _triggerbotThread = new Thread(TriggerbotThread);
        Thread _aimbotThread = new Thread(AimbotThread);
        Thread _noFlashThread = new Thread(NoFlashThread);
        Thread _bunnyhopThread = new Thread(BunnyhopThread);
        Thread _radarThread = new Thread(RadarThread);

        #region Konstruktor

        public MainWindow() {
            InitializeComponent();
        }

        #endregion

        #region Init

        private void Window_Initialized(object sender, EventArgs e) {
            Closing += MainWindow_Closing;

            if (ProcUtils.ProcessIsRunning("csgo")) {
                Settings.Init();
                Memory.Init();
                Init();
                InitUI();
            } else {
                Application.Current.Shutdown();
            }
        }

        private void Init() {
            _updateUIThread.Tick += _updateUIThread_Tick;
            _updateUIThread.Start();
        }

        private void _updateUIThread_Tick(object sender, EventArgs e) {
            UpdateInfoLabels();

            if (!ProcUtils.ProcessIsRunning("csgo")) {
                Application.Current.Shutdown();
            }
        }

        private void UpdateInfoLabels() {

            if (_espThread.IsAlive) {
                ESPStatusLabel.Content = "ESP Aktiv";
                ESPStatusLabel.Foreground = Brushes.Green;
            } else {
                ESPStatusLabel.Content = "ESP Inaktiv";
                ESPStatusLabel.Foreground = Brushes.Red;
            }

            if (_triggerbotThread.IsAlive) {
                TriggerstatusLabel.Content = "Triggerbot Aktiv";
                TriggerstatusLabel.Foreground = Brushes.Green;
            } else {
                TriggerstatusLabel.Content = "Triggerbot Inaktiv";
                TriggerstatusLabel.Foreground = Brushes.Red;
            }

            if (_aimbotThread.IsAlive) {
                AimbotStatuslabel.Content = "Aimbot Aktiv";
                AimbotStatuslabel.Foreground = Brushes.Green;
            } else {
                AimbotStatuslabel.Content = "Aimbot Inaktiv";
                AimbotStatuslabel.Foreground = Brushes.Red;
            }

            if (_noFlashThread.IsAlive) {
                NoFlashStatuslabel.Content = "NoFlash Aktiv";
                NoFlashStatuslabel.Foreground = Brushes.Green;
            } else {
                NoFlashStatuslabel.Content = "NoFlash Inaktiv";
                NoFlashStatuslabel.Foreground = Brushes.Red;
            }

            if (_bunnyhopThread.IsAlive) {
                BunnyhopStatuslabel.Content = "Bunnyhop Aktiv";
                BunnyhopStatuslabel.Foreground = Brushes.Green;
            } else {
                BunnyhopStatuslabel.Content = "Bunnyhop Inaktiv";
                BunnyhopStatuslabel.Foreground = Brushes.Red;
            }
        }

        private void InitUI() {
            Settings.ReadSettings();

            // glow/esp
            EnemySliderRot.Value = Convert.ToDouble(Settings.GetEnemyColorRed());
            EnemySliderGrün.Value = Convert.ToDouble(Settings.GetEnemyColorGreen());
            EnemySliderBlau.Value = Convert.ToDouble(Settings.GetEnemyColorBlue());
            EnemySliderRotTextbox.Text = Convert.ToString(Settings.GetEnemyColorRed());
            EnemySliderGrünTextbox.Text = Convert.ToString(Settings.GetEnemyColorGreen());
            EnemySliderBlauTextbox.Text = Convert.ToString(Settings.GetEnemyColorBlue());
            TeamSliderRot.Value = Convert.ToDouble(Settings.GetTeamColorRed());
            TeamSliderGrün.Value = Convert.ToDouble(Settings.GetTeamColorGreen());
            TeamSliderBlau.Value = Convert.ToDouble(Settings.GetTeamColorBlue());
            TeamSliderRotTextbox.Text = Convert.ToString(Settings.GetTeamColorRed());
            TeamSliderGrünTextbox.Text = Convert.ToString(Settings.GetTeamColorGreen());
            TeamSliderBlauTextbox.Text = Convert.ToString(Settings.GetTeamColorBlue());
            DrawEnemiesCheckbox.IsChecked = Settings.GetDrawEnemies();
            DrawBombCheckbox.IsChecked = Settings.GetDrawBomb();
            DrawTeammatesCheckbox.IsChecked = Settings.GetDrawTeammates();

            // trigger
            TriggerbotDelaySlider.Value = Convert.ToDouble(Settings.GetTriggerDelay());
            TriggerbotDelayTextbox.Text = Convert.ToString(Settings.GetTriggerDelay());
            TriggerteamCheckbox.IsChecked = Settings.GetTriggerTeam();
            TriggerAutoshootCheckbox.IsChecked = Settings.GetTriggerAuto();


            foreach (int keyValue in Enum.GetValues(typeof(Key))) {
                if (!keys.ContainsKey(Enum.GetName(typeof(Key), keyValue) ?? throw new InvalidOperationException("Der Keyboard Key war nicht im Dictionary enthalten"))) {
                    keys.Add(Enum.GetName(typeof(Key), keyValue) ?? throw new InvalidOperationException(), keyValue);
                }
            }

            foreach (KeyValuePair<string, int> keyValuePair in keys) {
                ComboBoxItem comboBoxItem = new ComboBoxItem { Content = keyValuePair.Key };

                Key key = (Key)keyValuePair.Value;

                comboBoxItem.Tag = key;

                if (key == Settings.GetTriggerKey()) {
                    comboBoxItem.IsSelected = true;
                }

                ComboBoxKeys.Items.Add(comboBoxItem);
            }

            //Aimbot
            AimbotFOVSlider.Value = Convert.ToDouble(Settings.GetAimbotFOV());
            AimbotFOVTextbox.Text = Convert.ToString(Settings.GetAimbotFOV());
            AimbotSmoothSlider.Value = Convert.ToDouble(Settings.GetAimbotSmooth());
            AimbotSmoothTextbox.Text = Convert.ToString(Settings.GetAimbotSmooth());

            foreach (KeyValuePair<string, int> keyValuePair in keys) {
                ComboBoxItem comboBoxItem = new ComboBoxItem { Content = keyValuePair.Key };

                Key key = (Key)keyValuePair.Value;

                comboBoxItem.Tag = key;

                if (key == Settings.GetAimbotKey()) {
                    comboBoxItem.IsSelected = true;
                }

                AimbotKeyCombobox.Items.Add(comboBoxItem);
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e) {
            Settings.UpdateSettings();

            if (_espThread != null) {
                if (_espThread.IsAlive) {
                    _espThread.Abort();
                }
            }

            if (_radarThread != null) {
                if (_radarThread.IsAlive) {
                    _radarThread.Abort();
                }
            }

            if (_triggerbotThread != null) {
                if (_triggerbotThread.IsAlive) {
                    _triggerbotThread.Abort();
                }
            }

            if (_aimbotThread != null) {
                if (_aimbotThread.IsAlive) {
                    _aimbotThread.Abort();
                }
            }

            if (_noFlashThread != null) {
                if (_noFlashThread.IsAlive) {
                    _noFlashThread.Abort();
                }
            }

            if (_bunnyhopThread != null) {
                if (_bunnyhopThread.IsAlive) {
                    _bunnyhopThread.Abort();
                }
            }

        }

        #endregion

        #region Threads

        private static void ESPThread() {
            while (true) {
                Glow.Render();
                Thread.Sleep(1);
            }

        }

        [STAThread]
        private static void TriggerbotThread() {
            while (true) {
                Triggerbot.Run();
                Thread.Sleep(1);
            }

        }

        [STAThread]
        private static void AimbotThread() {
            while (true) {
                Aimbot.Run();
                Thread.Sleep(1);
            }

        }

        private static void NoFlashThread() {
            while (true) {
                NoFlash.Run();
                Thread.Sleep(1);
            }

        }

        [STAThread]
        private static void BunnyhopThread() {
            while (true) {
                Bunnyhop.Run();
                Thread.Sleep(1);
            }

        }

        private static void RadarThread() {
            while (true) {
                Radar.Run();
                Thread.Sleep(1);
            }
        }

        #endregion

        #region UI Logik

        #region Glow

        private void ESPButton_Click_1(object sender, RoutedEventArgs e) {
            if (_espThread != null) {
                if (_espThread.IsAlive) {
                    _espThread.Abort();
                } else {
                    _espThread = new Thread(ESPThread);
                    _espThread.Start();
                }
            }

        }

        private void TeamSliderRot_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (e.OldValue <= 0) {
                return;
            }

            string value = Convert.ToInt32(e.NewValue).ToString();

            TeamSliderRotTextbox.Text = value;
            Settings.SetTeamColorRed(Convert.ToInt32(value));
        }

        private void TeamSliderGrün_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (e.OldValue <= 0) {
                return;
            }

            string value = Convert.ToInt32(e.NewValue).ToString();

            TeamSliderGrünTextbox.Text = value;
            Settings.SetTeamColorGreen(Convert.ToInt32(value));
        }

        private void TeamSliderBlau_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (e.OldValue <= 0) {
                return;
            }

            string value = Convert.ToInt32(e.NewValue).ToString();

            TeamSliderBlauTextbox.Text = value;
            Settings.SetTeamColorBlue(Convert.ToInt32(value));
        }

        private void EnemySliderRot_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (e.OldValue <= 0) {
                return;
            }

            string value = Convert.ToInt32(e.NewValue).ToString();

            EnemySliderRotTextbox.Text = value;
            Settings.SetEnemyColorRed(Convert.ToInt32(value));
        }

        private void EnemySliderGrün_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (e.OldValue <= 0) {
                return;
            }

            string value = Convert.ToInt32(e.NewValue).ToString();

            EnemySliderGrünTextbox.Text = value;
            Settings.SetEnemyColorGreen(Convert.ToInt32(value));
        }

        private void EnemySliderBlau_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (e.OldValue <= 0) {
                return;
            }

            string value = Convert.ToInt32(e.NewValue).ToString();

            EnemySliderBlauTextbox.Text = value;
            Settings.SetEnemyColorBlue(Convert.ToInt32(value));
        }

        private void TeamSliderRotTextbox_TextChanged(object sender, TextChangedEventArgs e) {
            int value = Convert.ToInt32(TeamSliderRotTextbox.Text);

            if (value > 0 && value <= 255) {
                TeamSliderRot.Value = value;
            }

            Settings.SetTeamColorRed(value);
        }

        private void TeamSliderGrünTextbox_TextChanged(object sender, TextChangedEventArgs e) {

            int value = Convert.ToInt32(TeamSliderGrünTextbox.Text);

            if (value > 0 && value <= 255) {
                TeamSliderGrün.Value = value;
            }

            Settings.SetTeamColorGreen(value);
        }

        private void TeamSliderBlauTextbox_TextChanged(object sender, TextChangedEventArgs e) {

            int value = Convert.ToInt32(TeamSliderBlauTextbox.Text);

            if (value > 0 && value <= 255) {
                TeamSliderBlau.Value = value;
            }

            Settings.SetTeamColorBlue(value);
        }

        private void EnemySliderRotTextbox_TextChanged(object sender, TextChangedEventArgs e) {

            int value = Convert.ToInt32(EnemySliderRotTextbox.Text);

            if (value > 0 && value <= 255) {
                EnemySliderRot.Value = value;
            }

            Settings.SetEnemyColorRed(value);

        }

        private void EnemySliderGrünTextbox_TextChanged(object sender, TextChangedEventArgs e) {
            int value = Convert.ToInt32(EnemySliderGrünTextbox.Text);

            if (value > 0 && value <= 255) {
                EnemySliderGrün.Value = value;
            }

            Settings.SetEnemyColorGreen(value);
        }

        private void EnemySliderBlauTextbox_TextChanged(object sender, TextChangedEventArgs e) {
            int value = Convert.ToInt32(EnemySliderBlauTextbox.Text);

            if (value > 0 && value <= 255) {
                EnemySliderBlau.Value = value;
            }

            Settings.SetEnemyColorBlue(value);
        }

        private void DrawTeammatesCheckbox_Checked(object sender, RoutedEventArgs e) {
            Settings.SetDrawTeammates(true);
        }

        private void DrawEnemiesCheckbox_Checked(object sender, RoutedEventArgs e) {
            Settings.SetDrawEnemies(true);
        }

        private void DrawBombCheckbox_Checked(object sender, RoutedEventArgs e) {
            Settings.SetDrawBomb(true);
        }

        private void DrawChickenCheckbox_Checked(object sender, RoutedEventArgs e) {
            Settings.SetDrawChicken(true);
        }

        private void DrawTeammatesCheckbox_Unchecked(object sender, RoutedEventArgs e) {
            Settings.SetDrawTeammates(false);
        }

        private void DrawEnemiesCheckbox_Unchecked(object sender, RoutedEventArgs e) {
            Settings.SetDrawEnemies(false);
        }

        private void DrawBombCheckbox_Unchecked(object sender, RoutedEventArgs e) {
            Settings.SetDrawBomb(false);
        }

        private void DrawChickenCheckbox_Unchecked(object sender, RoutedEventArgs e) {
            Settings.SetDrawChicken(false);
        }

        #endregion

        #region Triggerbot

        private void TriggerButton_Click(object sender, RoutedEventArgs e) {
            if (_triggerbotThread != null) {
                if (_triggerbotThread.IsAlive) {
                    _triggerbotThread.Abort();
                } else {
                    _triggerbotThread = new Thread(TriggerbotThread);
                    _triggerbotThread.Start();
                }
            }
        }

        private void TriggerbotDelaySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (e.OldValue <= 0) {
                return;
            }

            string value = Convert.ToInt32(e.NewValue).ToString();
            TriggerbotDelayTextbox.Text = value;
            Settings.SetTriggerDelay(Convert.ToInt32(value));
        }

        private void TriggerbotDelayTextbox_TextChanged(object sender, TextChangedEventArgs e) {
            int value = Convert.ToInt32(TriggerbotDelayTextbox.Text);

            if (value > 0 && value <= 255) {
                TriggerbotDelaySlider.Value = value;
            }

            Settings.SetTriggerDelay(value);
        }

        private void ComboBoxKeys_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var item = ComboBoxKeys.SelectedItem as ComboBoxItem;

            Settings.SetTriggerKey((Key)item.Tag);
        }

        private void TriggerAutoshootCheckbox_Checked(object sender, RoutedEventArgs e) {
            Settings.SetTriggerAuto(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriggerAutoshootCheckbox_Unchecked(object sender, RoutedEventArgs e) {
            Settings.SetTriggerAuto(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriggerteamCheckbox_Checked(object sender, RoutedEventArgs e) {
            Settings.SetTriggerTeam(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TriggerteamCheckbox_Unchecked(object sender, RoutedEventArgs e) {
            Settings.SetTriggerTeam(false);
        }

        #endregion

        #region Aimbot

        private void AimbotFOVSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (e.OldValue <= 0) {
                return;
            }

            string value = Convert.ToInt32(e.NewValue).ToString();

            AimbotFOVTextbox.Text = value;
            Settings.SetAimbotFOV(Convert.ToInt32(value));
        }

        private void AimbotFOVTextbox_TextChanged(object sender, TextChangedEventArgs e) {
            int value = Convert.ToInt32(AimbotFOVTextbox.Text);

            if (value > 0 && value <= 255) {
                AimbotFOVSlider.Value = value;
            }

            Settings.SetAimbotFOV(value);
        }

        private void AimbotSmoothSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (e.OldValue <= 0) {
                return;
            }

            string value = Convert.ToInt32(e.NewValue).ToString();

            AimbotSmoothTextbox.Text = value;
            Settings.SetAimbotSmooth(Convert.ToInt32(value));
        }

        private void AimbotSmoothTextbox_TextChanged(object sender, TextChangedEventArgs e) {
            int value = Convert.ToInt32(AimbotSmoothTextbox.Text);

            if (value > 0 && value <= 255) {
                AimbotSmoothSlider.Value = value;
            }

            Settings.SetAimbotSmooth(value);
        }

        private void AimbotHitboxCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var item = AimbotHitboxCombobox.SelectedItem as ComboBoxItem;

            Settings.SetAimbotBone((int)item.Tag);
        }

        private void AimbotButton_Click(object sender, RoutedEventArgs e) {
            if (_aimbotThread != null) {
                if (_aimbotThread.IsAlive) {
                    _aimbotThread.Abort();
                } else {
                    _aimbotThread = new Thread(AimbotThread);
                    _aimbotThread.Start();
                }
            }
        }

        private void AimbotKeyCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var item = AimbotKeyCombobox.SelectedItem as ComboBoxItem;

            Settings.SetAimbotKey((Key)item.Tag);
        }

        private void AimbotAutoCheckbox_Checked(object sender, RoutedEventArgs e) {
            Settings.SetAimAuto(true);
        }

        private void AimbotAutoCheckbox_Unchecked(object sender, RoutedEventArgs e) {
            Settings.SetAimAuto(false);
        }

        #endregion

        #region NoFlash

        private void NoFlashButton_Click(object sender, RoutedEventArgs e) {
            if (_noFlashThread != null) {
                if (_noFlashThread.IsAlive) {
                    _noFlashThread.Abort();
                } else {
                    _noFlashThread = new Thread(NoFlashThread);
                    _noFlashThread.Start();
                }
            }
        }

        #endregion

        #region Bunnyhop

        private void BunnyhopButton_Click(object sender, RoutedEventArgs e) {
            if (_bunnyhopThread != null) {
                if (_bunnyhopThread.IsAlive) {
                    _bunnyhopThread.Abort();
                } else {
                    _bunnyhopThread = new Thread(BunnyhopThread);
                    _bunnyhopThread.Start();
                }
            }
        }


        #endregion

        #region Radar

        private void RadarButton_Click(object sender, RoutedEventArgs e) {
            if (_radarThread != null) {
                if (_radarThread.IsAlive) {
                    _radarThread.Abort();
                } else {
                    _radarThread = new Thread(RadarThread);
                    _radarThread.Start();
                }
            }
        }

        #endregion

        #endregion
    }
}
