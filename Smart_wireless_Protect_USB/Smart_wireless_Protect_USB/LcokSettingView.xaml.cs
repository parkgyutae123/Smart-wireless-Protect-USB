using System.Windows;

namespace Smart_wireless_Protect_USB
{
    /// <summary>
    /// LcokSettingView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LcokSettingView : Window
    {
        public LcokSettingView()
        {
            InitializeComponent();
            DataContext = MainWindowViewModel.SingleMainViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
