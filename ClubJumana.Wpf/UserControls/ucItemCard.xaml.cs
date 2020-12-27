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
    /// Interaction logic for ItemCard.xaml
    /// </summary>
    public partial class ucItemCard : UserControl
    {
        public ucItemCard()
        {
            InitializeComponent();
        }

        private void BtnShowImageOfItem_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            GridLength currentGridLength = new GridLength(0, GridUnitType.Pixel);

            if (ColumnOfImageItem.Width == currentGridLength)
                ColumnOfImageItem.Width = new GridLength(350, GridUnitType.Pixel);
            else
                ColumnOfImageItem.Width = new GridLength(0, GridUnitType.Pixel);
        }

        public event EventHandler<EventArgs> BtnSaveOnClick;
        private void BtnSaveItem_OnClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (BtnSaveOnClick != null)
                BtnSaveOnClick(sender, e);
        }

        public event EventHandler<EventArgs> BtnCloseSubPage;
        private void BtnCloseSubPage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (BtnCloseSubPage != null)
                BtnCloseSubPage(sender, e);
        }

        private void TxtDescription_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(txtDescription.Text);
        }

        public event EventHandler<EventArgs> BtnCreateInner;
        private void BtnCreateInner_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (BtnCreateInner != null)
                BtnCreateInner(sender, e);
        }
    }
}
