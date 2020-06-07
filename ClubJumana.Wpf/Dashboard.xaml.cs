using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClubJumana.Wpf.UserControls;

namespace ClubJumana.Wpf
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
            //TestBorder.Child=new ucTest();
        }

        private void BtnChange_OnClick(object sender, RoutedEventArgs e)
        {
           //TestBorder.Child=new ucTest2();
        }
    }
}
