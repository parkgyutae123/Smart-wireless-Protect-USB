using Smart_wireless_Protect_USB.ViewModel;
using System.Windows.Controls;

namespace Smart_wireless_Protect_USB.View
{
    /// <summary>
    /// DeviceAndUserRegView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DeviceAndUserRegView : UserControl
    {
        public DeviceAndUserRegView()
        {
            InitializeComponent();
            DataContext = new DialogViewModel();
        }
    }
}
