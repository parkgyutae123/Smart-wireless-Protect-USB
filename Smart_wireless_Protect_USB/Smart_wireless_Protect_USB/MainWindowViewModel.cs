using MahApps.Metro.Controls;
using MahMaterialDragablzMashUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Smart_wireless_Protect_USB
{
    /// <summary>
    /// MainWindow와의 상호작용
    /// </summary>
    class MainWindowViewModel
    {
        public ICommand ShowRegFlyoutCommand { get; }

        public MainWindowViewModel()
        {
            ShowRegFlyoutCommand = new AnotherCommandImplementation(_ => ShowRigthFlyout());
        }

        public ICommand ShowWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => Application.Current.MainWindow == null,
                    CommandAction = () =>
                    {
                        Application.Current.MainWindow = new MainWindow();
                        Application.Current.MainWindow.Show();
                    }
                };
            }
        }


        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand { CommandAction = () => Application.Current.Shutdown() };
            }
        }

        public Flyout RightFlyout { get; set; }

        private void ShowRigthFlyout()
        {
            ((MainWindow)Application.Current.MainWindow).RightFlyout.IsOpen = !((MainWindow)Application.Current.MainWindow).RightFlyout.IsOpen;
        }
    }

    public class DelegateCommand : ICommand
    {
        public Action CommandAction { get; set; }
        public Func<bool> CanExecuteFunc { get; set; }

        public void Execute(object parameter)
        {
            CommandAction();
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null || CanExecuteFunc();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
