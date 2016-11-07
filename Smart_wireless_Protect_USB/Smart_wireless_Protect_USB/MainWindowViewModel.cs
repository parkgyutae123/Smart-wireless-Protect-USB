using MahMaterialDragablzMashUp;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Smart_wireless_Protect_USB
{
    /// <summary>
    /// MainWindow와의 상호작용
    /// </summary>
    class MainWindowViewModel: INotifyPropertyChanged
    {
        private string _userState="잠금화면 입니다.";
        /// <summary>
        /// 사용자 잠금화면 문구 프로퍼티
        /// </summary>
        public String UserState
        {
            get { return _userState; }
            set
            {
                if (value == _userState)
                {
                    return;
                }
                _userState = value;
                OnPropertyChanged("UserState");
            }
        }

        public void SettingSave()
        {
            OnPropertyChanged("UserState");
        }

      /// <summary>
      /// 설정창 확인버튼 커맨드
      /// </summary>
        public ICommand SettingSaveCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () =>
                    {
                        OnPropertyChanged("UserState");
                        Application.Current.MainWindow.Close();
                    }
                };
            }
        }

        public ICommand ShowRegFlyoutCommand { get; }//사용자등록 컨트롤을 띄우는 커맨드 프로퍼티
        public ICommand ShowLossFlyoutCommand { get; set; }//분실경우 마스터키를 입력하는 페이지를 띄우는 커맨드 프로퍼티
        public ICommand ShowHelpFlyoutCommand { get; set; }//도움말의 컨트롤을 띄우는 커맨드 프로퍼티

        static MainWindowViewModel _SingleMainViewModel;
        /// <summary>
        /// ViewModel 생성자
        /// </summary>
        protected MainWindowViewModel()
        {
            ShowRegFlyoutCommand = new AnotherCommandImplementation(_ => ShowRigthRegFlyout());
            ShowLossFlyoutCommand = new AnotherCommandImplementation(_ => ShowRigthLossFlyout());
            ShowHelpFlyoutCommand = new AnotherCommandImplementation(_ => ShowHelpFlyout());
        }
        /// <summary>
        /// 싱글톤
        /// </summary>
        /// <returns></returns>
        public static MainWindowViewModel SingleMainViewModel()
        {
            if(_SingleMainViewModel == null)
            {
                 _SingleMainViewModel = new MainWindowViewModel();
                
            }
            return _SingleMainViewModel;
        }

       
        //INotifyProperty 구현
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
