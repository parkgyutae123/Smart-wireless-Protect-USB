﻿using MahMaterialDragablzMashUp;
using Smart_wireless_Protect_USB.View;
using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Smart_wireless_Protect_USB.ViewModel
{
    public class DialogViewModel : INotifyPropertyChanged
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

        public ICommand RunPhoneDialogCommand { get; }
        public ICommand AcceptPhoneDialogCommand { get; }
        public ICommand CancelPhoneDialogCommand { get; }
        public ICommand RunEmailDialogCommand { get; set; }
        public ICommand AcceptEmailDialogCommand { get; set; }
        public ICommand CancelEmailDialogCommand { get; set; }

        public String _memberName;
        public String MemberName
        {
            get
            {
                return _memberName;
            }
            set
            {
                _memberName = value;
            }
        }
        public String _memberPhone;
        public String MemberPhone
        {
            get
            {
                return _memberPhone;
            }
            set
            {
                _memberPhone = value;
            }
        }
        public String _memberEmail;
        public String MemberEmail
        {
            get
            {
                return _memberEmail;
            }
            set
            {
                _memberEmail = value;
            }
        }
        public ICommand _Button_Clicked_JoinMember { get; set; }

        private bool _isDialogOpen;
        private object _DialogContent;

        /// <summary>
        /// 생성자
        /// </summary>
        public DialogViewModel()
        {
            RunPhoneDialogCommand = new AnotherCommandImplementation(OpenPhoneDialog);
            AcceptPhoneDialogCommand = new AnotherCommandImplementation(AcceptPhoneDialog);
            CancelPhoneDialogCommand = new AnotherCommandImplementation(CancelPhoneDialog);
            _Button_Clicked_JoinMember = new AnotherCommandImplementation(JoinMemberDialog);
            RunEmailDialogCommand = new AnotherCommandImplementation(OpenEmailDialog);
            AcceptEmailDialogCommand = new AnotherCommandImplementation(AcceptEmailDialog);
            CancelEmailDialogCommand = new AnotherCommandImplementation(CancelEmailDialog);
        }

        private void JoinMemberDialog(object obj)
        {
            var passwordBox = obj as PasswordBox;
            var password = passwordBox.Password;
            if(JoinMember(MemberName, password, MemberName, MemberEmail, MemberPhone))
            {
                MessageBox.Show("성공");return;
            }
            MessageBox.Show("실패");

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
            string url = @"https://api.bluehouselab.com/smscenter/v1.0.1/sendsms";
            string appid = "usblock";
            string apikey = "d748be0697a511e6871f0cc47a1fcfae";

            
            string json = @"{
                ""sender"":""01091617111"",
                ""receivers"":[""01091617111""],
                ""content"": ""인증 테스트""}";

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


        #region JoinMember(string name, string pw, string email. string pnum)
        public bool JoinMember(string id, string pw, string name, string email, string pnum)
        {
            bool re = client.JoinMember(id,pw,name,email,pnum);
            return re;
        }
        #endregion
    }
}
