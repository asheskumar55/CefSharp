using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;

namespace CefSharp
{
    public partial class CefSharp : Form
    {
        public CefSharp()
        {
            InitializeComponent();
            //For resize property 
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            //End
        }

        //The all codes are for chromium property
        ChromiumWebBrowser chrome;

        private void CefSharp_Load(object sender, EventArgs e)
        {
            CefSettings settings = new CefSettings();
            //Initialize
            Cef.Initialize(settings);
            txtUrl.Text = "https://www.google.com";
            chrome = new ChromiumWebBrowser(txtUrl.Text);
            this.pContainer.Controls.Add(chrome);
            chrome.Dock = DockStyle.Fill;
            chrome.AddressChanged += Chrome_AddressChanged;
        }

        private void Chrome_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                txtUrl.Text = e.Address;
            }));
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            chrome.Load(txtUrl.Text);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            chrome.Refresh();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (chrome.CanGoForward)
                chrome.Forward();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (chrome.CanGoBack)
                chrome.Back();
        }

        private void CefSharp_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //End


        //For resize Property 
        private const int cGrip = 16;
        private const int cCaption = 32;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0X84)
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17;
                    return;
                }
            }
            base.WndProc(ref m);
        }
        //End
        private void MaximizeButton_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }

        }

        private void MinimizeButton_Click_1(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
                this.WindowState = FormWindowState.Minimized;
        }

        private void TopPanel_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

    }
}
