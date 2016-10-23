using MahMaterialDragablzMashUp;
using MaterialDesignThemes.Wpf;
using Microsoft.WindowsMobile.PocketOutlook;
using Smart_wireless_Protect_USB.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
        public ICommand RunDialogCommand { get; }
        public ICommand AcceptDialogCommand { get; }
        public ICommand CancelDialogCommand { get; }

        private bool _isDialogOpen;
        private object _DialogContent;

        /// <summary>
        /// 생성자
        /// </summary>
        public DialogViewModel()
        {
            RunDialogCommand = new AnotherCommandImplementation(OpenDialog);
            AcceptDialogCommand = new AnotherCommandImplementation(AcceptDialog);
            CancelDialogCommand = new AnotherCommandImplementation(CancelDialog);
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


        private void CancelDialog(object obj)
        {
            IsDialogOpen = false;
        }

        private void AcceptDialog(object obj)
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

        private void OpenDialog(object obj)
        {
            DialogContent = new InputPhoneNumberDialog();
            IsDialogOpen = true;
        }

        //public ICommand RunDialogCommand => new AnotherCommandImplementation(ExecuteRunExtendedDialog);

        //private async void ExecuteRunExtendedDialog(object obj)
        //{
        //    var view = new InputPhoneNumberDialog();

        //    var result = await DialogHost.Show(view,ExtendedOpenedEventHandler, ExtendedClosingEventHandler);

        //}

        //private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventargs)
        //{
        //    Console.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");
        //}

        //private void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        //{
        //    if ((bool)eventArgs.Parameter == false) return;

        //    //OK, lets cancel the close...
        //    eventArgs.Cancel();

        //    //...now, lets update the "session" with some new content!
        //    eventArgs.Session.UpdateContent(new ProgressDialog());
        //    //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

        //    //lets run a fake operation for 3 seconds then close this baby.
        //    Task.Delay(TimeSpan.FromSeconds(2))
        //        .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
        //            TaskScheduler.FromCurrentSynchronizationContext());
        //}

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
