using Smart_wireless_Protect_USB.ViewModel;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(client.JoinMember(NameTextBox.Text, PasswordBox.Password, NameTextBox.Text, EmailTextBox.Text, PhoneTextBox.Text))
            {
                MessageBox.Show("성공");return;
            }
            MessageBox.Show("실패");
        }
    }
}
