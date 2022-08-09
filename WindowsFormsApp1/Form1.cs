using Gma.System.MouseKeyHook;
using Loamen.KeyMouseHook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int KeyID = -1;

        GamepadToMouse gamepadToMouse = new GamepadToMouse();

        private void Form1_Load(object sender, EventArgs e)
        {
            gamepadToMouse.Start();

            Hotkey hotkey = new Hotkey(this.Handle);
            hotkey.OnHotkey += Hotkey_OnHotkey;

            KeyID = hotkey.RegisterHotkey(Keys.Home, Hotkey.KeyFlags.NONE);
        }

        private void Hotkey_OnHotkey(int HotKeyID)
        {
            if(HotKeyID == KeyID)
            {
                gamepadToMouse.Enabled = !gamepadToMouse.Enabled;
                notifyIcon1.ShowBalloonTip(1000, "警告", $"虚拟鼠标已{(gamepadToMouse.Enabled ? "启用" : "禁用")}", ToolTipIcon.Warning);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(1000, "提示", "作者¿? DIY WIN掌机技术交流群155198255", ToolTipIcon.Warning);
        }
    }
}
