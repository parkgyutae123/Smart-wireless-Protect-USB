﻿using Smart_wireless_Protect_USB.ViewModel;
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
    /// InputMasterKeyView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InputMasterKeyView : UserControl
    {
        public InputMasterKeyView()
        {
            InitializeComponent();
            DataContext = new DialogViewModel();
        }
    }
}
