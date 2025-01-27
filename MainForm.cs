﻿using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
using MicMute.Properties;
using Microsoft.Win32;
using Shortcut;
using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Reactive;
using System.Windows.Forms;


namespace MicMute
{
    public partial class MainForm : Form
    {
        const string DEFAULT_RECORDING_DEVICE = "Default recording device";
        public CoreAudioController AudioController = new CoreAudioController();
        private readonly HotkeyBinder hotkeyBinder = new HotkeyBinder();
        private readonly RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MicMute");
        
        // toggle
        private readonly string registryKeyName = "Hotkey";
        private Hotkey hotkey;

        // mute
        private readonly string registryKeyMute = "HotkeyMute";
        private Hotkey muteHotkey;

        // unmute
        private readonly string registryKeyUnmute = "HotkeyUnmute";
        private Hotkey unMuteHotkey;

        private readonly string registryDeviceId = "DeviceId";
        private readonly string registryDeviceName = "DeviceName";

        private string selectedDeviceId;
        private string selectedDeviceName;
        private MicSelectorForm micSelectorForm;
        private MicStatusForm micStatusForm;

        RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        enum MicStatus
        {
            Initial, On, Off, Error
        }
        private MicStatus currentStatus;

        private bool myVisible; 
        public bool MyVisible
        {
            get { return myVisible; }
            set { myVisible = value; Visible = value; }
        }

        public MainForm()
        {
            InitializeComponent();

            if (rkApp.GetValue("MyApp") == null)
            {
                // The value doesn't exist, the application is not set to run at startup
                chkRunAtLogin.Checked = false;
            }
            else
            {
                // The value exists, the application is set to run at startup
                chkRunAtLogin.Checked = true;
            }
        }

        private void OnNextDevice(DeviceChangedArgs next)
        {
            UpdateSelectedDevice();
        }

        private void MyHide()
        {
            ShowInTaskbar = false;
            Location = new Point(-10000, -10000);
            MyVisible = false;
        }

        private void MyShow()
        {
            MyVisible = true;
            ShowInTaskbar = true;
            CenterToScreen();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            MyHide();
            selectedDeviceId = (string)registryKey.GetValue(registryDeviceId) ?? "";
            selectedDeviceName = (string)registryKey.GetValue(registryDeviceName) ?? DEFAULT_RECORDING_DEVICE;

            UpdateSelectedDevice();
            AudioController.AudioDeviceChanged.Subscribe(OnNextDevice);
            
            // toggle
            var hotkeyValue = registryKey.GetValue(registryKeyName);
            if (hotkeyValue != null)
            {
                var converter = new Shortcut.Forms.HotkeyConverter();
                hotkey = (Hotkey)converter.ConvertFromString(hotkeyValue.ToString());
                if (!hotkeyBinder.IsHotkeyAlreadyBound(hotkey)) hotkeyBinder.Bind(hotkey).To(ToggleMicStatus);
            }

            // mute 
            hotkeyValue = registryKey.GetValue(registryKeyMute);
            if (hotkeyValue != null)
            {
                var converter = new Shortcut.Forms.HotkeyConverter();
                muteHotkey = (Hotkey)converter.ConvertFromString(hotkeyValue.ToString());
                if (!hotkeyBinder.IsHotkeyAlreadyBound(muteHotkey)) hotkeyBinder.Bind(muteHotkey).To(MuteMicStatus);
            }

            // unmute 
            hotkeyValue = registryKey.GetValue(registryKeyUnmute);
            if (hotkeyValue != null)
            {
                var converter = new Shortcut.Forms.HotkeyConverter();
                unMuteHotkey = (Hotkey)converter.ConvertFromString(hotkeyValue.ToString());
                if (!hotkeyBinder.IsHotkeyAlreadyBound(unMuteHotkey)) hotkeyBinder.Bind(unMuteHotkey).To(UnMuteMicStatus);
            }

            //AudioController.AudioDeviceChanged.Subscribe(x =>
            //{
            //    Debug.WriteLine("{0} - {1}", x.Device.Name, x.ChangedType.ToString());
            //});
            
            if(Properties.Settings.Default.EnableMicStatusOverlay)
            {
                this.chkShowMicStatus.Checked = Properties.Settings.Default.EnableMicStatusOverlay;
                this.mnuItemMicStatusOverlay.Checked = true;
                this.trackBarTransparency.Value = Properties.Settings.Default.MicStatusFormTranparency;
                this.labelBackgroundColor.BackColor = Properties.Settings.Default.MicStatusFormBackground;
                this.colorDialog1.Color = Properties.Settings.Default.MicStatusFormBackground;
                ShowMicStatusOverlay();
            }
        }

        private void CloseMicStatusOverlay()
        {
            if (micStatusForm != null)
            {
                micStatusForm.Close();
                micStatusForm.Dispose();
                micStatusForm = null;
            }
        }

        private void ShowMicStatusOverlay()
        {
            CloseMicStatusOverlay();
            micStatusForm = new MicStatusForm();
            micStatusForm.Show();
            micStatusForm.Activate();
            micStatusForm.FormClosing += (s, e) =>
            {
                mnuItemMicStatusOverlay.Checked = false;
            };
            UpdateSelectedDevice();
        }

        private void OnMuteChanged(DeviceMuteChangedArgs next)
        {
            UpdateStatus(next.Device);
        }

        IDisposable muteChangedSubscription;
        public void UpdateDevice(IDevice device)
        {
            muteChangedSubscription?.Dispose();
            muteChangedSubscription = device?.MuteChanged.Subscribe(OnMuteChanged);
            UpdateStatus(device);
        }
        public IDevice getSelectedDevice()
        {
            return selectedDeviceId == "" ? AudioController.DefaultCaptureDevice : AudioController.GetDevice(new Guid(selectedDeviceId), DeviceState.Active);
        }

        public void UpdateSelectedDevice()
        {
            UpdateDevice(getSelectedDevice());
        }

        Icon iconOff = Properties.Resources.off;
        Icon iconOn = Properties.Resources.on;
        Icon iconError = Properties.Resources.error;

        public void PlaySound(string relativePath)
        {
            string path = Path.Combine(Application.StartupPath, relativePath);
            if (File.Exists(path))
            {
                SoundPlayer simpleSound = new SoundPlayer(path);
                simpleSound.Play();
            }
        }

        public void UpdateStatus(IDevice device)
        {
            MicStatus newStatus = (device != null) ? (device.IsMuted ? MicStatus.Off : MicStatus.On) : MicStatus.Error;
            //if(micStatusForm == null)
            //{
            //    micStatusForm = new MicStatusForm();
            //    micStatusForm.Show(this);
            //}
            bool playSound = currentStatus != MicStatus.Initial && currentStatus != newStatus;
            currentStatus = newStatus;
            switch (currentStatus)
            {
                case MicStatus.On:
                    UpdateIcon(iconOn, device.FullName);
                    if (micStatusForm != null)
                    {
                        //micStatusForm.Invoke()
                        micStatusForm.SetMicState(Resources.micunmuted);
                    }
                    if (playSound) PlaySound("on.wav");
                    break;
                case MicStatus.Off:
                    UpdateIcon(iconOff, device.FullName);
                    if (micStatusForm != null)
                    {
                        micStatusForm.SetMicState(Resources.micmuted);
                    }
                    if (playSound) PlaySound("off.wav");
                    break;
                case MicStatus.Error:
                    UpdateIcon(iconError, "< No device >");
                    if (playSound) PlaySound("error.wav");
                    break;
            }
        }
        private void UpdateIcon(Icon icon, string tooltipText)
        {
            this.icon.Icon = icon;
            this.icon.Text = tooltipText;
        }

        public async void ToggleMicStatus()
        {
            await getSelectedDevice()?.ToggleMuteAsync();
        }

        public async void MuteMicStatus()
        {
            await getSelectedDevice()?.SetMuteAsync(true);
        }

        public async void UnMuteMicStatus()
        {
            await getSelectedDevice()?.SetMuteAsync(false);
        }

        private void Icon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ToggleMicStatus();
            }
        }

        private void HotkeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // toggle
            if (hotkey != null)
            {
                hotkeyTextBox.Hotkey = hotkey;
                if (hotkeyBinder.IsHotkeyAlreadyBound(hotkey)) hotkeyBinder.Unbind(hotkey);
            }

            // mute
            if (muteHotkey != null)
            {
                muteTextBox.Hotkey = muteHotkey;
                if (hotkeyBinder.IsHotkeyAlreadyBound(muteHotkey)) hotkeyBinder.Unbind(muteHotkey);
            }

            // unmute
            if (unMuteHotkey != null)
            {
                unmuteTextBox.Hotkey = unMuteHotkey;
                if (hotkeyBinder.IsHotkeyAlreadyBound(unMuteHotkey)) hotkeyBinder.Unbind(unMuteHotkey);
            }

            MyShow();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MyVisible)
            {
                MyHide();
                e.Cancel = true;

                hotkey = hotkeyTextBox.Hotkey;

                if (hotkey == null)
                {
                    registryKey.DeleteValue(registryKeyName, false);
                }
                else
                {
                    if (!hotkeyBinder.IsHotkeyAlreadyBound(hotkey))
                    {
                        registryKey.SetValue(registryKeyName, hotkey);
                        if (!hotkeyBinder.IsHotkeyAlreadyBound(hotkey)) hotkeyBinder.Bind(hotkey).To(ToggleMicStatus);
                    }
                }

                muteHotkey = muteTextBox.Hotkey;

                if (muteHotkey == null)
                {
                    registryKey.DeleteValue(registryKeyMute, false);
                }
                else
                {
                    if (!hotkeyBinder.IsHotkeyAlreadyBound(muteHotkey))
                    {
                        registryKey.SetValue(registryKeyMute, muteHotkey);
                        if (!hotkeyBinder.IsHotkeyAlreadyBound(muteHotkey)) hotkeyBinder.Bind(muteHotkey).To(MuteMicStatus);
                    }
                }


                unMuteHotkey = unmuteTextBox.Hotkey;

                if (unMuteHotkey == null)
                {
                    registryKey.DeleteValue(registryKeyUnmute, false);
                }
                else
                {
                    if (!hotkeyBinder.IsHotkeyAlreadyBound(unMuteHotkey))
                    {
                        registryKey.SetValue(registryKeyUnmute, unMuteHotkey);
                        if (!hotkeyBinder.IsHotkeyAlreadyBound(unMuteHotkey)) hotkeyBinder.Bind(unMuteHotkey).To(UnMuteMicStatus);
                    }
                }

                Properties.Settings.Default.EnableMicStatusOverlay = chkShowMicStatus.Checked;
                Properties.Settings.Default.MicStatusFormTranparency = (byte)trackBarTransparency.Value;
                Properties.Settings.Default.MicStatusFormBackground = labelBackgroundColor.BackColor;
                Properties.Settings.Default.Save();

                if (Properties.Settings.Default.EnableMicStatusOverlay)
                {
                    ShowMicStatusOverlay();
                }
                else
                {
                    CloseMicStatusOverlay();
                }

                if (chkRunAtLogin.Checked)
                {
                    // Add the value in the registry so that the application runs at startup
                    rkApp.SetValue("MicMute", Application.ExecutablePath);
                }
                else
                {
                    // Remove the value from the registry so that the application doesn't start
                    rkApp.DeleteValue("MicMute", false);
                }
            }
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            hotkeyTextBox.Hotkey = null;
            hotkeyTextBox.Text = "None";
        }
        private void muteReset_Click(object sender, EventArgs e)
        {
            muteTextBox.Hotkey = null;
            muteTextBox.Text = "None";
        }

        private void unmuteReset_Click(object sender, EventArgs e)
        {
            unmuteTextBox.Hotkey = null;
            unmuteTextBox.Text = "None";
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            if (micStatusForm != null)
            {
                micStatusForm.Close();
                micStatusForm.Dispose();
                micStatusForm = null;
            }
            Application.Exit();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            micSelectorForm = new MicSelectorForm();
            ComboBox comboBox = micSelectorForm.cbMics;
            comboBox.Items.Clear();

            bool registryExists = false;

            ComboboxItem defaultItem = new ComboboxItem();
            defaultItem.Text = DEFAULT_RECORDING_DEVICE;
            defaultItem.deviceId = "";
            comboBox.Items.Add(defaultItem);

            if (selectedDeviceId == "")
            {
                registryExists = true;
                comboBox.SelectedIndex = comboBox.Items.Count - 1;
            }

            foreach (CoreAudioDevice device in AudioController.GetCaptureDevices())
            {
                if (device.State == DeviceState.Active)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = device.FullName;
                    item.deviceId = device.Id.ToString();
                    comboBox.Items.Add(item);

                    if (item.deviceId == selectedDeviceId)
                    {
                        registryExists = true;
                        comboBox.SelectedIndex = comboBox.Items.Count - 1;
                    }
                }
            }

            if (!registryExists) {
                ComboboxItem item = new ComboboxItem();
                item.Text = "(unavailable) " + registryDeviceName.ToString();
                item.deviceId = selectedDeviceId.ToString();
                comboBox.Items.Add(item);
                comboBox.SelectedIndex = comboBox.Items.Count - 1;
            }
            DialogResult result = micSelectorForm.ShowDialog();
            Console.WriteLine(result);
            ComboboxItem selectedItem = (ComboboxItem)comboBox.SelectedItem;

            registryKey.SetValue(registryDeviceId, selectedItem.deviceId);
            registryKey.SetValue(registryDeviceName, selectedItem.Text);
            selectedDeviceName = selectedItem.Text;
            selectedDeviceId = selectedItem.deviceId;

            micSelectorForm.Dispose();

            UpdateSelectedDevice();
        }

        private void OnToggleMicStatusOverlay(object sender, EventArgs e)
        {
            this.mnuItemMicStatusOverlay.Checked = !this.mnuItemMicStatusOverlay.Checked;
            Properties.Settings.Default.EnableMicStatusOverlay = this.mnuItemMicStatusOverlay.Checked;
            Properties.Settings.Default.Save();
            if ((micStatusForm == null || micStatusForm.IsDisposed ) && Properties.Settings.Default.EnableMicStatusOverlay)
            {
                this.chkShowMicStatus.Checked = true;
                ShowMicStatusOverlay();
            }
            else if (micStatusForm != null && !micStatusForm.IsDisposed)
            {
                CloseMicStatusOverlay();
            }            
        }

        private void chkShowMicStatus_CheckedChanged(object sender, EventArgs e)
        {
            //panelTransparentOverlaySettings.Visible = chkShowMicStatus.Checked;
            panelTransparentOverlaySettings.Visible = chkShowMicStatus.Checked;
        }

        private void labelBackgroundColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                labelBackgroundColor.BackColor = colorDialog1.Color;
            }
        }
    }
}
