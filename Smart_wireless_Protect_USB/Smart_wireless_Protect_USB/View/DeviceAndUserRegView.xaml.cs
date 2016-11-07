using Smart_wireless_Protect_USB.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Smart_wireless_Protect_USB.View
{
    /// <summary>
    /// DeviceAndUserRegView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DeviceAndUserRegView : UserControl
    {
        ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        public DeviceAndUserRegView()
        {
            InitializeComponent();
            DataContext = new DialogViewModel();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (client.JoinMember(IDTextBox.Text, PasswordBox.Password, NameTextBox.Text, EmailTextBox.Text, PhoneTextBox.Text))
            {
                MessageBox.Show("성공");return;
            }
            MessageBox.Show("실패");
        }
    }
}
