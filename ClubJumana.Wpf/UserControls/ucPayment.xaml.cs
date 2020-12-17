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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClubJumana.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for ucPayment.xaml
    /// </summary>
    public partial class ucPayment : UserControl
    {
        public ucPayment()
        {
            InitializeComponent();
        }

        private void UcPayment_OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }

        public event EventHandler<EventArgs> BtnCloseSecendPage;
        private void BtnCloseSecendPage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (BtnCloseSecendPage != null)
                BtnCloseSecendPage(sender, e);
        }
    }
}
