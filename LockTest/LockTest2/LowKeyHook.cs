/**
	@file LowKeyHook.cs
	@author HyonTak Lee
	@date 2010.11.04
	@brief Defined KeyHook Class
*/
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Windows;

namespace LockTest2
{
	/**
		@class KeyHook
		@author HyonTak Lee
		@date 2010.11.04
		@brief Keyboard API Wrapper & Hide/Show StartMenu
	*/
	class KeyHook : IDisposable
	{
		/**
			@struct LowKeyHook
			@brief Porting LPARAM
		*/
		[StructLayout(LayoutKind.Sequential)]
		public struct LowKeyHook
		{
			public int vkCode;
			private int scanCode;
			public int flags;
			private int time;
			private int dwExtraInfo;
		}
		private int intLLKey;
		private const int KF_ALTDOWN = 0x2000;
		private const int KF_EXTENDED = 0x100;
		private const int LLKHF_ALTDOWN = 0x20;
		private const int LLKHF_EXTENDED = 1;
		private LowKeyHook lParam;
		private const int VK_ESCAPE = 0x1b;
		private const int VK_LWIN = 0x5b;
		private const int VK_RWIN = 0x5c;
		private const int VK_TAB = 9;
		private const int VK_CONTROL = 0x11;
		private const int VK_MENU = 0x12;
		private const int VK_DELETE = 0x2E;

		private const int WH_KEYBOARD_LL = 13;
		private const int WM_KEYDOWN = 0x100;
		private const int WM_KEYUP = 0x101;
		private const int WM_SYSKEYDOWN = 260;
		private const int WM_SYSKEYUP = 0x105;

		private delegate int Dele_LowKeyProc(int nCode, int wParam, ref KeyHook.LowKeyHook lParam);
		private static Dele_LowKeyProc lowCallBack;

		private string TaskMgrKey = "DisableTaskMgr";
		private string TaskMgrReg = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System";

		[DllImport("user32.dll")]
		private static extern int CallNextHookEx(int hHook, int nCode, int wParam, ref LowKeyHook lParam);
		[DllImport("user32.dll")]
		private static extern int SetWindowsHookEx(int idHook, Dele_LowKeyProc lpfn, int hMod, int dwThreadId);
		[DllImport("user32.dll")]
		private static extern bool UnhookWindowsHookEx(int hHook);
		[DllImport("user32.dll")]
		private static extern int FindWindow(string className, string windowText);
		[DllImport("user32.dll")]
		private static extern int ShowWindow(int hwnd, int command);

		/**
			Initialize & Install Hook
		*/
		public KeyHook()
		{
			lowCallBack = new Dele_LowKeyProc(this.LowLevelKeyboardProc);
			this.intLLKey = SetWindowsHookEx(13, lowCallBack,
				Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]).ToInt32(), 0);

			RegistryKey reg = Registry.CurrentUser.CreateSubKey(TaskMgrReg);
			reg.SetValue(TaskMgrKey, 1, RegistryValueKind.DWord);
			KillStartMenu();
		}

		public void Dispose()
		{
			UnhookWindowsHookEx(this.intLLKey);
			RegistryKey reg = Registry.CurrentUser.CreateSubKey(TaskMgrReg);
			reg.SetValue(TaskMgrKey, 0, RegistryValueKind.DWord);
			ShowStartMenu();
		}

		/**
			Keyboard Control Procedure
		*/
		private int LowLevelKeyboardProc(int nCode, int wParam, ref LowKeyHook lParam)
		{
			bool flag = false;
			try
			{
				switch (wParam)
				{
					case KF_EXTENDED:
					case WM_KEYUP:
					case WM_SYSKEYDOWN:
					case WM_SYSKEYUP:
						if ((((((lParam.vkCode == VK_TAB) && (lParam.flags == LLKHF_ALTDOWN))
						|| ((lParam.vkCode == VK_ESCAPE) && (lParam.flags == LLKHF_ALTDOWN)))
						|| (((lParam.vkCode == VK_ESCAPE) && (lParam.flags == 0))
						|| ((lParam.vkCode == VK_LWIN) && (lParam.flags == 1)))//)
						|| ((lParam.vkCode == VK_RWIN) && (lParam.flags == 1)))
						|| (lParam.flags == 0x20)))
						{
							flag = true;
						}
						break;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
			if (flag)
			{
				return 1;
			}
			else {
				return CallNextHookEx(0, nCode, wParam, ref lParam);
			}
		}

		/**
			Hide StartMenu
		*/
		public void KillStartMenu()
		{
			int hwnd = FindWindow("Shell_TrayWnd", "");
			ShowWindow(hwnd, 0);
		}

		/**
			Show StartMenu
		*/
		public static void ShowStartMenu()
		{
			int hwnd = FindWindow("Shell_TrayWnd", "");
			ShowWindow(hwnd, 1);
		}
	}
}
