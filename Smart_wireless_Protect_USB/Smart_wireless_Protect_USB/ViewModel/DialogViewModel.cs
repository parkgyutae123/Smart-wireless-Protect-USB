using MahMaterialDragablzMashUp;
using Smart_wireless_Protect_USB.View;
using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Smart_wireless_Protect_USB.ViewModel
{
    public class DialogViewModel : INotifyPropertyChanged
    {
        public ICommand UserRegCommand { get; set;}
        SmartService.Service1Client client = new SmartService.Service1Client();


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

            string email;
            
            DialogContent = inputEmailDialog;
            
            System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(3))
                .ContinueWith((t, _) => IsDialogOpen = false, null,
                    TaskScheduler.FromCurrentSynchronizationContext());

            if (client.CheckNameEmail(inputEmailDialog.userName.Text, inputEmailDialog.emailNumber.Text) == false)
            {
                MessageBox.Show("등록되지않는 사용자 정보 입니다.");
                return;
            }
            
            email = inputEmailDialog.emailNumber.Text;

            SendMail S = new SendMail(email);
            S.SendGmail();



        }

        InputEmailDialog inputEmailDialog = new InputEmailDialog();
        private void OpenEmailDialog(object obj)
        {
            DialogContent = inputEmailDialog;
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

        InputPhoneNumberDialog inputPhoneDialog = new InputPhoneNumberDialog();
        private void AcceptPhoneDialog(object obj)
        {
            DialogContent = inputPhoneDialog;

            string pnum;
            
            string url = @"https://api.bluehouselab.com/smscenter/v1.0/sendsms";
            string appid = "usblock";
            string apikey = "d748be0697a511e6871f0cc47a1fcfae";

            string AcceptNum = RandomNum();
            if (client.CheckNamePhone(inputPhoneDialog.userName.Text, inputPhoneDialog.phoneNumber.Text) == false)
            {
                MessageBox.Show("등록되지않는 사용자 정보 입니다.");
                return;
            }

            pnum = inputPhoneDialog.phoneNumber.Text;


            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"sender\":\"01091617111\",\"receivers\":[\"");
            jsonBuilder.Append(pnum);
            jsonBuilder.Append("\"],\"content\":\"");
            jsonBuilder.Append(AcceptNum);
            jsonBuilder.Append("\"}");
            //string json = "{\"sender\":\"01091617111\",\"receivers\":[\"01091617111\"],\"content\":\"TEST\"}"; // 회원번호 받고, 텍스트 추가
            string json = jsonBuilder.ToString();

            WebClient webclient = new WebClient();
           
            NetworkCredential creds = new NetworkCredential(appid, apikey);
            webclient.Credentials = creds;
            webclient.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";


            try
            {
                string response = webclient.UploadString(url, json);
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


            DialogContent = inputPhoneDialog;
            IsDialogOpen = true;
        }
        /// <summary>
        /// 인증번호 생성
        /// </summary>
        /// <returns>인증번호</returns>
        private String RandomNum()
        {
            StringBuilder ran = new StringBuilder();
            Random r = new Random();
            for (int i = 0; i < 8; i++)
            {
                int n = (int)r.Next(1, 10);
                ran.Append(n.ToString());
            }
            return ran.ToString();
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
