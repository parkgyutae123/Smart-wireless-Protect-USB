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
using System.Windows.Shapes;

namespace LockTest2
{
	/// <summary>
	/// LockScreen.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class LockScreen : Window
	{
		private string text;
		KeyHook hook;

		public LockScreen()
		{
			InitializeComponent();
		}

		public LockScreen(string text)
		{
			InitializeComponent();
			InitializeService();
			this.Topmost = true;
			this.text = text;
			this.Closed += LockScreen_Closed;
		}

		private void LockScreen_Closed(object sender, EventArgs e)
		{
			hook.Dispose();
		}

		private void InitializeService()
		{
			hook = new KeyHook();
		}
		private void button_Click(object sender, RoutedEventArgs e)
		{
			if (textBox.Text == text)
			{
				this.Close();
			}
			else
			{
				MessageBox.Show("비밀번호가 일치하지 않습니다.");
			}
		}
	}
}
