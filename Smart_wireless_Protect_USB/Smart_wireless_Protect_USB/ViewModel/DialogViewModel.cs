using MahMaterialDragablzMashUp;
using Smart_wireless_Protect_USB.View;
using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Smart_wireless_Protect_USB.ViewModel
{
    public class DialogViewModel : INotifyPropertyChanged
    {
        public ICommand UserRegCommand { get; set;}


        public ICommand RunPhoneDialogCommand { get; }
        public ICommand AcceptPhoneDialogCommand { get; }
        public ICommand CancelPhoneDialogCommand { get; }
        public ICommand RunEmailDialogCommand { get; set; }
        public ICommand AcceptEmailDialogCommand { get; set; }
        public ICommand CancelEmailDialogCommand { get; set; }

        private bool _isDialogOpen;
        private object _DialogContent;

        /// <summary>
        /// 생성자
        /// </summary>
        public DialogViewModel()
        {
            UserRegCommand = new AnotherCommandImplementation(UserRegisterCommand); ;

            RunPhoneDialogCommand = new AnotherCommandImplementation(OpenPhoneDialog);
            AcceptPhoneDialogCommand = new AnotherCommandImplementation(AcceptPhoneDialog);
            CancelPhoneDialogCommand = new AnotherCommandImplementation(CancelPhoneDialog);

            RunEmailDialogCommand = new AnotherCommandImplementation(OpenEmailDialog);
            AcceptEmailDialogCommand = new AnotherCommandImplementation(AcceptEmailDialog);
            CancelEmailDialogCommand = new AnotherCommandImplementation(CancelEmailDialog);
        }
        /// <summary>
        /// 사용자등록
        /// </summary>
        /// <param name="obj"></param>
        private void UserRegisterCommand(object obj)
        {
            BluetoothInfo blutooth = new BluetoothInfo();
            string device = blutooth.GetPeerDeviceName;
            MessageBox.Show(device);
        }

        private void CancelEmailDialog(object obj)
        {
            IsDialogOpen = false;
        }

        private void AcceptEmailDialog(object obj)
        {
            DialogContent = new ProgressDialog();
            System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(3))
                .ContinueWith((t, _) => IsDialogOpen = false, null,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void OpenEmailDialog(object obj)
        {
            DialogContent = new InputEmailDialog();
            IsDialogOpen = true;
        }

        public bool IsDialogOpen
        {
            get { return _isDialogOpen; }
            set
            {
                if (_isDialogOpen == value) return;
                _isDialogOpen = value;
                OnPropertyChanged();
            }
        }

        public object DialogContent
        {
            get { return _DialogContent; }
            set
            {
                if (_DialogContent == value) return;
                _DialogContent = value;
                OnPropertyChanged();
            }
        }


        private void CancelPhoneDialog(object obj)
        {
            IsDialogOpen = false;
        }

        private void AcceptPhoneDialog(object obj)
        {
            string url = @"https://api.bluehouselab.com/smscenter/v1.0/sendsms";
            string appid = "usblock";
            string apikey = "d748be0697a511e6871f0cc47a1fcfae";

            
            string json = "{\"sender\":\"01091617111\",\"receivers\":[\"01091617111\"],\"content\":\"TEST\"}"; // 회원번호 받고, 텍스트 추가

            WebClient client = new WebClient();
           
            NetworkCredential creds = new NetworkCredential(appid, apikey);
            client.Credentials = creds;
            client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";


            try
            {
                string response = client.UploadString(url, json);
                MessageBox.Show(response);
            }
            catch(WebException e)
            {
                HttpStatusCode status = ((HttpWebResponse)e.Response).StatusCode;
                MessageBox.Show(status.ToString());
            }

            DialogContent = new ProgressDialog();
            System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(3))
                .ContinueWith((t, _) => IsDialogOpen = false, null,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void OpenPhoneDialog(object obj)
        {
            
            
            DialogContent = new InputPhoneNumberDialog();
            IsDialogOpen = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 메서드, 메서드 호출자의 속성이름을 업데이트하여 실행하는 메소드
        /// </summary>
        /// <param name="propertyName">메서드호출자의 속성이름</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
