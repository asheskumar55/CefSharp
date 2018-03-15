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
        }

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
    }
}
