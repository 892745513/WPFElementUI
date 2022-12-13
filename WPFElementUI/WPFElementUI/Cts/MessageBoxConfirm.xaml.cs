using CommonFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Windows.Foundation;
//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace Styles.Cts
{
    public sealed partial class MessageBoxConfirm : UserControl
    {
        #region 公共属性

        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {
            FormHelper.BtnHelp(" ");
            e.Handled = true;
        }

        private void BtnMini_Click(object sender, RoutedEventArgs e)
        {
            FormHelper.FormMini();
            e.Handled = true;

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            FormHelper.FormClose();
        }

        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as FrameworkElement;
            if (btn != null)
            {
                FormHelper.FormMax();
                if (mainForm.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                {
                    btn.ToolTip = "还原";
                    HideBorder();
                }
                else
                {
                    btn.ToolTip = "最大化";
                    ShowBorder();

                }

            }
        }

        private void Move_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FormHelper.FormMove();
            e.Handled = true;
        }

        private void SizeTopLeft_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FormHelper.SizeTopLeft();
            e.Handled = true;
        }

        private void SizeTOPRIGHT_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FormHelper.SizeTOPRIGHT();
            e.Handled = true;
        }

        private void SizeBOTTOMLEFT_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FormHelper.SizeBOTTOMLEFT();
            e.Handled = true;
        }

        private void SizeBOTTOMRIGHT_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FormHelper.SizeBOTTOMRIGHT();
            e.Handled = true;
        }

        private System.Windows.Forms.Form mainForm;

        public FormHelper FormHelper { get; }
        private void HideBorder()
        {
            try
            {
                borderLeft.Visibility =
                borderTop.Visibility =
                borderBottom.Visibility =
                borderRight.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {

            }
        }
        private void ShowBorder()
        {
            try
            {
                borderLeft.Visibility =
                borderTop.Visibility =
                borderBottom.Visibility =
                borderRight.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {

            }
        }
        #endregion

        public string Message { get; set; }
        public string Title { get; set; }
         
        public string Text => tbxMsg.Text;
        private MessageBoxConfirm()
        {
            this.InitializeComponent();
            FormHelper = new FormHelper();
            this.DataContext = this;
        }
        private MessageBoxConfirm(string title, string msg, int width = 400, int height = 160) : this()
        {
            mainForm = FormHelper.InitMainForm(this, title, width, height, false);
            this.Message = msg;
            this.Title = title;
        }
        private MessageBoxConfirm(string title, string value, string placeHolder, int width = 400, int height = 160) : this()
        {
            mainForm = FormHelper.InitMainForm(this, title, width, height, false);
            mainForm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Title = title;

            this.ui1.Visibility = Visibility.Collapsed;
            this.ui2.Visibility = Visibility.Visible;
            this.tbxMsg.Text = value;
            dps.TextBoxEx.SetPlaceHolder(tbxMsg, placeHolder);
        }


        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainForm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                mainForm.Close();
            }
            catch (Exception)
            {

            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainForm.DialogResult = System.Windows.Forms.DialogResult.OK;
                mainForm.Close();
            }
            catch (Exception)
            {

            }
        }

        public  System.Windows.Forms.DialogResult Show()
        {
            return mainForm.ShowDialog();
        }

        public static System.Windows.Forms.DialogResult Show(string title, string msg, int width = 400, int height = 160)
        {
            var box = new MessageBoxConfirm(title, msg, width, height);
            return box.mainForm.ShowDialog();
        }
        public static MessageBoxConfirm CreateInputBox(string title, string value, string placeHolder, int width = 400, int height = 160)
        {
           return new MessageBoxConfirm(title, value, placeHolder, width, height); 
        } 
    }
}
