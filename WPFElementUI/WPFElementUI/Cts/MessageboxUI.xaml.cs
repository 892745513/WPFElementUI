using CommonFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Windows.Controls;
using Windows.Foundation;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace Styles.Cts
{
    public sealed partial class MessageboxUI : UserControl, System.ComponentModel.INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public System.Collections.ObjectModel.ObservableCollection<MsgModel> Msgs { get; set; }
        public MessageboxUI()
        {
            this.InitializeComponent();
            Msgs = new System.Collections.ObjectModel.ObservableCollection<MsgModel>();
            this.DataContext = this;
        }


        public void AddMsg(MsgModel msg)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                Msgs.Add(msg);
                this.Visibility = System.Windows.Visibility.Visible;
                System.Threading.Tasks.Task.Run(() =>
                {
                    Thread.Sleep(3000);
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        Msgs.Remove(msg);
                        if (Msgs.Count <= 0)
                        {
                            this.Visibility = System.Windows.Visibility.Collapsed;
                        }
                    }));
                });
            })); 
        } 
    }


}
