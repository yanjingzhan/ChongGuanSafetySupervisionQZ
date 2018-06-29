﻿using System;
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

namespace ChongGuanSafetySupervisionQZ.View.WPF
{
    /// <summary>
    /// HomeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            this.Loaded += HomeWindow_Loaded;
        }

        private void HomeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.TextBlock_Department.Text = GlobalData.CurrentDeparment?.DeparmentName ?? "秦城监狱第一看守所";
            this.TextBlock_UserName.Text = GlobalData.CurrentUser?.UserName ?? "唐志强唐总（爱玩秘书）";

        }

        private void Button_NewTalk(object sender, RoutedEventArgs e)
        {
            this.Grid_MainNavigationButtons.Visibility = Visibility.Collapsed;
            this.Frame_BusinessPage.Visibility = Visibility.Visible;
            this.Frame_BusinessPage.Source = new Uri("Pages/NewTalkPage.xaml", UriKind.Relative);   
        }
    }
}
