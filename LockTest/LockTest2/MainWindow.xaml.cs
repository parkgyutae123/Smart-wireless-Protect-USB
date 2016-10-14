using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Management;
using System.ComponentModel;

namespace LockTest2
{
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : Window
	{
		private LockScreen lc;
		private IntPtr windowHandle;
		private string deviceId = "AA04012700012181";

		// SIUTIL_API(HRESULT) USBDebugDevices(DWORD * dwDevices);
		[DllImport("SiUtil.dll")]
		public static extern int USBDebugDevices(ref uint dwDevices);

		// SIUTIL_API(HRESULT) GetUSBDeviceSN(DWORD dwDeviceNum, const char ** psSerialNum);
		[DllImport("SiUtil.dll")]
		public static extern int GetUSBDeviceSN(uint dwDeviceNum, ref string psSerialNum);

		public MainWindow()
		{
			InitializeComponent();

   
			if (deviceIdCheck())
			{
				MessageBox.Show("일치");
			}
			else
			{
				MessageBox.Show("ㅇㅇ");
			}
		}
		//등록 시리얼 체크

		public Boolean deviceIdCheck()
		{
			//Serial 가져오기, drive에는 현재 프로그램이있는 드라이브( c 또는 d 또는 g .....)
			//string drive = System.Environment.CurrentDirectory.Substring(0, 1);
			//USBSerialNumber usb = new USBSerialNumber();
			//string serial = usb.getSerialNumberFromDriveLetter(drive);
			try
			{

				string serial = null;
				ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'");
				
				foreach (ManagementObject queryObj in searcher.Get())
				{
					//MessageBox.Show(string.Format("-----------------------------------\nWin32_USBDevice instance\n-----------------------------------\nCaption: {0}\nDescription: {1}\nDeviceID: {2}\nManufacturer: {3}\nName: {4}\nPNPDeviceID: {5}", queryObj["Caption"], queryObj["Description"], queryObj["DeviceID"], queryObj["Manufacturer"], queryObj["Name"], queryObj["PNPDeviceID"]));

					ManagementObject theSerialNumberObjectQuery = new ManagementObject("Win32_PhysicalMedia.Tag='" + queryObj["DeviceID"] + "'");
					MessageBox.Show(string.Format("SerialNumber : {0}", theSerialNumberObjectQuery["SerialNumber"].ToString()));
					serial = theSerialNumberObjectQuery["SerialNumber"].ToString();
				}
				if (serial == deviceId)
				{
					return true;
				}

				return false;

			}
			catch (ManagementException e)
			{
				MessageBox.Show("An error occurred while querying for WMI data: " + e.Message);
				return false;
			}
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			if (tb_pw.Text != string.Empty)
			{
				if (lc == null)
				{
					lc = new LockScreen(tb_pw.Text);
					lc.Show();
				}
			}
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);

			// Adds the windows message processing hook and registers USB device add/removal notification.
			HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
			if (source != null)
			{
				windowHandle = source.Handle;
				source.AddHook(HwndHandler);
				UsbNotification.RegisterUsbDeviceNotification(windowHandle);
			}
		}

		/// <summary>
		/// Method that receives window messages.
		/// </summary>
		private IntPtr HwndHandler(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
		{
			if (msg == UsbNotification.WmDevicechange)
			{
				switch ((int)wparam)
				{
					case UsbNotification.DbtDeviceremovecomplete:
						MessageBox.Show("Remove"); // this is where you do your magic
						break;
					case UsbNotification.DbtDevicearrival:
						MessageBox.Show("Add");// this is where you do your magic
						break;
				}
			}

			handled = false;
			return IntPtr.Zero;
		}
	}
}
