using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace CommonFiles
{
    public class FormHelper
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int IParam);
        public int WM_SYSCOMMAND = 0x0112;

        //移动窗体
        public int SC_MOVE = 0xF010;
        public int HTCAPTION = 0x0002;

        //改变 小
        public int WMSZ_LEFT = 0xF001;
        public int WMSZ_RIGHT = 0xF002;
        public int WMSZ_TOP = 0xF003;
        public int WMSZ_TOPLEFT = 0xF004;
        public int WMSZ_TOPRIGHT = 0xF005;
        public int WMSZ_BOTTOM = 0xF006;
        public int WMSZ_BOTTOMLEFT = 0xF007;
        public int WMSZ_BOTTOMRIGHT = 0xF008;

        public void BtnHelp(string Url)
        {
            try
            {
                System.Diagnostics.Process.Start(Url);
            }
            catch (Exception)
            {

            }
        }
        public void FormMini()
        {
            mainForm.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }
        public void FormMax()
        {
            var size = System.Windows.Forms.Screen.GetWorkingArea(mainForm);
            mainForm.MaximumSize = size.Size;

            mainForm.WindowState =
                mainForm.WindowState == System.Windows.Forms.FormWindowState.Maximized
                                     ? System.Windows.Forms.FormWindowState.Normal
                                     : System.Windows.Forms.FormWindowState.Maximized;
        }
        public void FormClose()
        {
            mainForm.Close();
        }
        public void FormMove()
        { 
            if (ReleaseCapture()) SendMessage(mainForm.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        public void SizeTopLeft()
        {
            if (mainForm == null || mainForm.WindowState != FormWindowState.Normal)
            {
                return;
            }
            if (ReleaseCapture()) SendMessage(mainForm.Handle, WM_SYSCOMMAND, WMSZ_TOPLEFT, 0);
        }

        public void SizeTOPRIGHT()
        {
            if (mainForm == null || mainForm.WindowState != FormWindowState.Normal)
            {
                return;
            }
            if (ReleaseCapture()) SendMessage(mainForm.Handle, WM_SYSCOMMAND, WMSZ_TOPRIGHT, 0);
        }

        public void SizeBOTTOMLEFT()
        {
            if (mainForm == null || mainForm.WindowState != FormWindowState.Normal)
            {
                return;
            }
            if (ReleaseCapture()) SendMessage(mainForm.Handle, WM_SYSCOMMAND, WMSZ_BOTTOMLEFT, 0);
        }

        public void SizeBOTTOMRIGHT()
        {
            if (mainForm == null || mainForm.WindowState != FormWindowState.Normal)
            {
                return;
            }
            if (ReleaseCapture()) SendMessage(mainForm.Handle, WM_SYSCOMMAND, WMSZ_BOTTOMRIGHT, 0);
        }
        public System.Windows.Forms.Form mainForm;
        public System.Windows.Controls.UserControl Page { get; set; }

        public Form InitMainForm(System.Windows.Controls.UserControl page, string title, int width, int height, bool topMost = true)
        {
            Page = page;
            mainForm = new System.Windows.Forms.Form();

            mainForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            mainForm.Padding = new System.Windows.Forms.Padding(0);


            mainForm.TopMost = topMost;
            mainForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            mainForm.ShowIcon = false;
            mainForm.Text = title;
            SetFormSize(mainForm, width, height);

            mainForm.WindowState = System.Windows.Forms.FormWindowState.Normal;

            var wpfhost = new System.Windows.Forms.Integration.ElementHost();

            wpfhost.Dock = System.Windows.Forms.DockStyle.Fill;
            wpfhost.Margin = new System.Windows.Forms.Padding(0);

            wpfhost.BackColorTransparent = true;
            mainForm.Controls.Add(wpfhost);
            wpfhost.Child = page;
            return mainForm;
        }

        public void ShowForm()
        {
            this.mainForm.Show();
        }
        public DialogResult ShowFormDialog()
        {
           return this.mainForm.ShowDialog();
        }

        public void SetFormSize(Form form, int width, int height)
        {
            var currentGraphics = System.Drawing.Graphics.FromHwnd(mainForm.Handle);
            double dpixRatio = currentGraphics.DpiX / 96;
            mainForm.Size = new System.Drawing.Size((int)(width * dpixRatio), (int)(height * dpixRatio));
        }

        public void BtnTheme(string themeName)
        {
            try
            {
                if (Page == null)
                {
                    return;
                }

                var ass = Page.GetType().Assembly;

                var name = ass.ManifestModule.ScopeName.Replace(".dll", ""); 
                var uri = $"pack://application:,,,/{name};component/{themeName}/{themeName}Theme.xaml";

                var newstyle = new ResourceDictionary();
                newstyle.Source = new Uri(uri);
                if (newstyle != null)
                {
                    Page.Resources.MergedDictionaries[0] = newstyle;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
