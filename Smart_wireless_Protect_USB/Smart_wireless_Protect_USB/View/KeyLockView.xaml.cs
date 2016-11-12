using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Smart_wireless_Protect_USB.View
{
    /// <summary>
    /// KeyLockView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class KeyLockView : UserControl
    {
        public KeyLockView()
        {
            InitializeComponent();

            var thread = new DispatcherTimer(TimeSpan.FromSeconds(2), DispatcherPriority.Normal, Tick, this.Dispatcher);
        }
        /// <summary>
        /// 현제 시간을 표시해줄 타이머
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick(object sender, EventArgs e)
        {
            var dateTime = DateTime.Now;
            TimeTransLabel.Content = new TextBlock { Text = "현제 시간 : " + dateTime, SnapsToDevicePixels = true, HorizontalAlignment= HorizontalAlignment.Center
            , FontSize = 24};
        }
    }
}
