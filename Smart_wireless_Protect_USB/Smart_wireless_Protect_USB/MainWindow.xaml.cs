using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Smart_wireless_Protect_USB
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow:Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = MainWindowViewModel.SingleMainViewModel();
            
        }
        public void LockCtrlAltDel()
        {
            RegistryKey reg = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            reg.SetValue("DisableTaskMgr", 1, RegistryValueKind.DWord);
        }
        

        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = true;
            if (e.Key == Key.LeftAlt ||
                e.Key == Key.RightAlt ||
                e.Key == Key.LeftCtrl ||
                e.Key == Key.RightCtrl ||
                e.Key == Key.F4||
                e.Key == Key.Delete)
            {
                return;
            }
            base.OnKeyDown(e);
            if(e.Key==Key.Enter)
            {
                this.Close();
            }

        }
    }
}
