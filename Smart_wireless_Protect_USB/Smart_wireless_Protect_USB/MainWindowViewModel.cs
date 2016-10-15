using MahMaterialDragablzMashUp;
using System;
using System.Windows;
using System.Windows.Input;

namespace Smart_wireless_Protect_USB
{
    /// <summary>
    /// MainWindow와의 상호작용
    /// </summary>
    class MainWindowViewModel
    {
        public ICommand ShowRegFlyoutCommand { get; }//사용자등록 컨트롤을 띄우는 커맨드 프로퍼티
        public ICommand ShowLossFlyoutCommand { get; set; }//분실경우 마스터키를 입력하는 페이지를 띄우는 커맨드 프로퍼티
        public ICommand ShowHelpFlyoutCommand { get; set; }//도움말의 컨트롤을 띄우는 커맨드 프로퍼티

        /// <summary>
        /// ViewModel 생성자
        /// </summary>
        public MainWindowViewModel()
        {
            ShowRegFlyoutCommand = new AnotherCommandImplementation(_ => ShowRigthRegFlyout());
            ShowLossFlyoutCommand = new AnotherCommandImplementation(_ => ShowRigthLossFlyout());
            ShowHelpFlyoutCommand = new AnotherCommandImplementation(_ => ShowHelpFlyout());
        }

        /// <summary>
        /// 트래이아이콘에서 윈도우 창을 띄우기 위한 커맨드 프로퍼티
        /// </summary>
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

        /// <summary>
        /// 잠금화면 닫는 커맨드 프로퍼티
        /// </summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand { CommandAction = () => Application.Current.Shutdown() };
            }
        }

        
        /// <summary>
        /// 사용자등록 컨트롤을 띄우기위한 메서드
        /// </summary>
        private void ShowRigthRegFlyout()
        {
            ((MainWindow)Application.Current.MainWindow).RightRegFlyout.IsOpen = !((MainWindow)Application.Current.MainWindow).RightRegFlyout.IsOpen;
        }
        /// <summary>
        /// USB 분실시 키입력 컨트롤을 띄우기위한 메서드
        /// </summary>
        private void ShowRigthLossFlyout()
        {
            ((MainWindow)Application.Current.MainWindow).RightLossFlyout.IsOpen = !((MainWindow)Application.Current.MainWindow).RightLossFlyout.IsOpen;
        }
        /// <summary>
        /// 도움말 컨트롤 띄우기위한 메서드
        /// </summary>
        private void ShowHelpFlyout()
        {
            ((MainWindow)Application.Current.MainWindow).CenterHelpFlyout.IsOpen = !((MainWindow)Application.Current.MainWindow).CenterHelpFlyout.IsOpen;
        }
    }

    /// <summary>
    /// 중복 커맨드를 방지하기위해 정의한 클래스
    /// </summary>
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
