﻿namespace MicMute
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.iconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.hotkeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItemMicStatusOverlay = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hotkeyTextBox = new Shortcut.Forms.HotkeyTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.muteTextBox = new Shortcut.Forms.HotkeyTextBox();
            this.muteReset = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.unmuteReset = new System.Windows.Forms.Button();
            this.unmuteTextBox = new Shortcut.Forms.HotkeyTextBox();
            this.chkShowMicStatus = new System.Windows.Forms.CheckBox();
            this.panelTransparentOverlaySettings = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.labelBackgroundColor = new System.Windows.Forms.Label();
            this.trackBarTransparency = new System.Windows.Forms.TrackBar();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.chkRunAtLogin = new System.Windows.Forms.CheckBox();
            this.iconContextMenu.SuspendLayout();
            this.panelTransparentOverlaySettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTransparency)).BeginInit();
            this.SuspendLayout();
            // 
            // icon
            // 
            this.icon.ContextMenuStrip = this.iconContextMenu;
            this.icon.Text = "<Initializing>";
            this.icon.Visible = true;
            this.icon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Icon_MouseClick);
            // 
            // iconContextMenu
            // 
            this.iconContextMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.iconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.hotkeyToolStripMenuItem,
            this.mnuItemMicStatusOverlay,
            this.toolStripMenuItem1});
            this.iconContextMenu.Name = "iconContextMenu";
            this.iconContextMenu.Size = new System.Drawing.Size(173, 92);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(172, 22);
            this.toolStripMenuItem2.Text = "Select mic";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // hotkeyToolStripMenuItem
            // 
            this.hotkeyToolStripMenuItem.Name = "hotkeyToolStripMenuItem";
            this.hotkeyToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.hotkeyToolStripMenuItem.Text = "Settings";
            this.hotkeyToolStripMenuItem.Click += new System.EventHandler(this.HotkeyToolStripMenuItem_Click);
            // 
            // mnuItemMicStatusOverlay
            // 
            this.mnuItemMicStatusOverlay.Name = "mnuItemMicStatusOverlay";
            this.mnuItemMicStatusOverlay.Size = new System.Drawing.Size(172, 22);
            this.mnuItemMicStatusOverlay.Text = "Mic Status Overlay";
            this.mnuItemMicStatusOverlay.Click += new System.EventHandler(this.OnToggleMicStatusOverlay);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
            this.toolStripMenuItem1.Text = "Exit";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // hotkeyTextBox
            // 
            this.hotkeyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hotkeyTextBox.Hotkey = null;
            this.hotkeyTextBox.Location = new System.Drawing.Point(12, 70);
            this.hotkeyTextBox.Name = "hotkeyTextBox";
            this.hotkeyTextBox.Size = new System.Drawing.Size(249, 26);
            this.hotkeyTextBox.TabIndex = 1;
            this.hotkeyTextBox.Text = "None";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(323, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Register toggle hotkey (auto saved on close)";
            // 
            // buttonReset
            // 
            this.buttonReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonReset.Location = new System.Drawing.Point(265, 70);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(77, 26);
            this.buttonReset.TabIndex = 3;
            this.buttonReset.Text = "reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(315, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Register mute hotkey (auto saved on close)";
            // 
            // muteTextBox
            // 
            this.muteTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.muteTextBox.Hotkey = null;
            this.muteTextBox.Location = new System.Drawing.Point(12, 130);
            this.muteTextBox.Name = "muteTextBox";
            this.muteTextBox.Size = new System.Drawing.Size(249, 26);
            this.muteTextBox.TabIndex = 5;
            this.muteTextBox.Text = "None";
            // 
            // muteReset
            // 
            this.muteReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.muteReset.Location = new System.Drawing.Point(265, 130);
            this.muteReset.Name = "muteReset";
            this.muteReset.Size = new System.Drawing.Size(77, 26);
            this.muteReset.TabIndex = 6;
            this.muteReset.Text = "reset";
            this.muteReset.UseVisualStyleBackColor = true;
            this.muteReset.Click += new System.EventHandler(this.muteReset_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(333, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Register unmute hotkey (auto saved on close)";
            // 
            // unmuteReset
            // 
            this.unmuteReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.unmuteReset.Location = new System.Drawing.Point(265, 190);
            this.unmuteReset.Name = "unmuteReset";
            this.unmuteReset.Size = new System.Drawing.Size(77, 26);
            this.unmuteReset.TabIndex = 9;
            this.unmuteReset.Text = "reset";
            this.unmuteReset.UseVisualStyleBackColor = true;
            this.unmuteReset.Click += new System.EventHandler(this.unmuteReset_Click);
            // 
            // unmuteTextBox
            // 
            this.unmuteTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.unmuteTextBox.Hotkey = null;
            this.unmuteTextBox.Location = new System.Drawing.Point(12, 190);
            this.unmuteTextBox.Name = "unmuteTextBox";
            this.unmuteTextBox.Size = new System.Drawing.Size(249, 26);
            this.unmuteTextBox.TabIndex = 8;
            this.unmuteTextBox.Text = "None";
            // 
            // chkShowMicStatus
            // 
            this.chkShowMicStatus.AutoSize = true;
            this.chkShowMicStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowMicStatus.Location = new System.Drawing.Point(12, 223);
            this.chkShowMicStatus.Name = "chkShowMicStatus";
            this.chkShowMicStatus.Size = new System.Drawing.Size(242, 24);
            this.chkShowMicStatus.TabIndex = 10;
            this.chkShowMicStatus.Text = "Show Transparent Mic Overlay";
            this.chkShowMicStatus.UseVisualStyleBackColor = true;
            this.chkShowMicStatus.CheckedChanged += new System.EventHandler(this.chkShowMicStatus_CheckedChanged);
            // 
            // panelTransparentOverlaySettings
            // 
            this.panelTransparentOverlaySettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTransparentOverlaySettings.Controls.Add(this.label4);
            this.panelTransparentOverlaySettings.Controls.Add(this.labelBackgroundColor);
            this.panelTransparentOverlaySettings.Controls.Add(this.trackBarTransparency);
            this.panelTransparentOverlaySettings.Location = new System.Drawing.Point(12, 254);
            this.panelTransparentOverlaySettings.Name = "panelTransparentOverlaySettings";
            this.panelTransparentOverlaySettings.Size = new System.Drawing.Size(330, 138);
            this.panelTransparentOverlaySettings.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Transparency (Alpha)";
            // 
            // labelBackgroundColor
            // 
            this.labelBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelBackgroundColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.labelBackgroundColor.Location = new System.Drawing.Point(15, 79);
            this.labelBackgroundColor.Name = "labelBackgroundColor";
            this.labelBackgroundColor.Size = new System.Drawing.Size(301, 23);
            this.labelBackgroundColor.TabIndex = 1;
            this.labelBackgroundColor.Text = "Background Color";
            this.labelBackgroundColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelBackgroundColor.Click += new System.EventHandler(this.labelBackgroundColor_Click);
            // 
            // trackBarTransparency
            // 
            this.trackBarTransparency.LargeChange = 10;
            this.trackBarTransparency.Location = new System.Drawing.Point(6, 31);
            this.trackBarTransparency.Maximum = 255;
            this.trackBarTransparency.Minimum = 10;
            this.trackBarTransparency.Name = "trackBarTransparency";
            this.trackBarTransparency.Size = new System.Drawing.Size(319, 45);
            this.trackBarTransparency.SmallChange = 5;
            this.trackBarTransparency.TabIndex = 0;
            this.trackBarTransparency.Value = 128;
            // 
            // chkRunAtLogin
            // 
            this.chkRunAtLogin.AutoSize = true;
            this.chkRunAtLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRunAtLogin.Location = new System.Drawing.Point(12, 12);
            this.chkRunAtLogin.Name = "chkRunAtLogin";
            this.chkRunAtLogin.Size = new System.Drawing.Size(183, 24);
            this.chkRunAtLogin.TabIndex = 12;
            this.chkRunAtLogin.Text = "Run MicMute at Login";
            this.chkRunAtLogin.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 407);
            this.Controls.Add(this.chkRunAtLogin);
            this.Controls.Add(this.panelTransparentOverlaySettings);
            this.Controls.Add(this.chkShowMicStatus);
            this.Controls.Add(this.unmuteReset);
            this.Controls.Add(this.unmuteTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.muteReset);
            this.Controls.Add(this.muteTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hotkeyTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MicMute";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.iconContextMenu.ResumeLayout(false);
            this.panelTransparentOverlaySettings.ResumeLayout(false);
            this.panelTransparentOverlaySettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTransparency)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon icon;
        private System.Windows.Forms.ContextMenuStrip iconContextMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private Shortcut.Forms.HotkeyTextBox hotkeyTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem hotkeyToolStripMenuItem;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Label label2;
        private Shortcut.Forms.HotkeyTextBox muteTextBox;
        private System.Windows.Forms.Button muteReset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button unmuteReset;
        private Shortcut.Forms.HotkeyTextBox unmuteTextBox;
        private System.Windows.Forms.CheckBox chkShowMicStatus;
        private System.Windows.Forms.ToolStripMenuItem mnuItemMicStatusOverlay;
        private System.Windows.Forms.Panel panelTransparentOverlaySettings;
        private System.Windows.Forms.TrackBar trackBarTransparency;
        private System.Windows.Forms.Label labelBackgroundColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkRunAtLogin;
    }
}

