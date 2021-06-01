using MicMute.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicMute
{
    public partial class MicStatusForm : Form
    {
        private const byte _opaque = 255;

        /// <summary>
        /// 0: the window is completely transparent ... 255: the window is opaque
        /// </summary>
        private byte _alpha;
        private int _defaultStyle;
        private bool _muted = false;
        private enum GetWindowLong
        {
            /// <summary>
            /// Sets a new extended window style.
            /// </summary>
            GWL_EXSTYLE = -20
        }

        private enum ExtendedWindowStyles
        {
            /// <summary>
            /// Transparent window.
            /// </summary>
            WS_EX_TRANSPARENT = 0x20,
            /// <summary>
            /// Layered window. http://msdn.microsoft.com/en-us/library/windows/desktop/ms632599%28v=vs.85%29.aspx#layered
            /// </summary>
            WS_EX_LAYERED = 0x80000
        }

        private enum LayeredWindowAttributes
        {
            /// <summary>
            /// Use bAlpha to determine the opacity of the layered window.
            /// </summary>
            LWA_COLORKEY = 0x1,
            /// <summary>
            /// Use crKey as the transparency color.
            /// </summary>
            LWA_ALPHA = 0x2
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern int User32_GetWindowLong(IntPtr hWnd, GetWindowLong nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int User32_SetWindowLong(IntPtr hWnd, GetWindowLong nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
        private static extern bool User32_SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte bAlpha, LayeredWindowAttributes dwFlags);


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            //Click through
            int wl = User32_GetWindowLong(this.Handle, GetWindowLong.GWL_EXSTYLE);
            User32_SetWindowLong(this.Handle, GetWindowLong.GWL_EXSTYLE, wl | (int)ExtendedWindowStyles.WS_EX_LAYERED | (int)ExtendedWindowStyles.WS_EX_TRANSPARENT);
            //Change alpha
            //User32_SetLayeredWindowAttributes(this.Handle, (TransparencyKey.B << 16) + (TransparencyKey.G << 8) + TransparencyKey.R, _alpha, LayeredWindowAttributes.LWA_COLORKEY | LayeredWindowAttributes.LWA_ALPHA);
            User32_SetLayeredWindowAttributes(this.Handle, 0, _alpha, LayeredWindowAttributes.LWA_ALPHA);
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            //Click through
            //int wl = User32_GetWindowLong(this.Handle, GetWindowLong.GWL_EXSTYLE);
            User32_SetWindowLong(this.Handle, GetWindowLong.GWL_EXSTYLE, _defaultStyle);
            //Change alpha
            //User32_SetLayeredWindowAttributes(this.Handle, (TransparencyKey.B << 16) + (TransparencyKey.G << 8) + TransparencyKey.R, _alpha, LayeredWindowAttributes.LWA_COLORKEY | LayeredWindowAttributes.LWA_ALPHA);
            User32_SetLayeredWindowAttributes(this.Handle, 0, _opaque, LayeredWindowAttributes.LWA_ALPHA);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.FormBorderStyle = FormBorderStyle.None;
            //Click through
            int wl = User32_GetWindowLong(this.Handle, GetWindowLong.GWL_EXSTYLE);
            User32_SetWindowLong(this.Handle, GetWindowLong.GWL_EXSTYLE, wl | (int)ExtendedWindowStyles.WS_EX_LAYERED | (int)ExtendedWindowStyles.WS_EX_TRANSPARENT);
            //Change alpha
            //User32_SetLayeredWindowAttributes(this.Handle, (TransparencyKey.B << 16) + (TransparencyKey.G << 8) + TransparencyKey.R, _alpha, LayeredWindowAttributes.LWA_COLORKEY | LayeredWindowAttributes.LWA_ALPHA);
            User32_SetLayeredWindowAttributes(this.Handle, 0, _alpha, LayeredWindowAttributes.LWA_ALPHA);
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 50, 50));
        }

        public MicStatusForm()
        {
            InitializeComponent();
            _defaultStyle = User32_GetWindowLong(this.Handle, GetWindowLong.GWL_EXSTYLE);
            TopMost = true;
            BackColor = Properties.Settings.Default.MicStatusFormBackground;
            _alpha = Properties.Settings.Default.MicStatusFormTranparency;
        }

        public void SetMicState(Image image)
        {
            if (this.InvokeRequired)
            {
                SetMicStateDelegate setMicStateDelegate = new SetMicStateDelegate(SetMicState);
                this.pic.Invoke(setMicStateDelegate, image);
            }
            else
            {
                this.pic.Image = image;
            }
        }

        delegate void SetMicStateDelegate(Image image);

        private void OnLoad(object sender, EventArgs e)
        {
            this.Location = Settings.Default.MicStatusFormPosition;
        }

        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.MicStatusFormPosition = this.Location;
            Settings.Default.Save();
        }
    }
}
