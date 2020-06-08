﻿using System;
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
    /// Interaction logic for ucCustomerCard.xaml
    /// </summary>
    public partial class ucCustomerCard : UserControl
    {
        public ucCustomerCard()
        {
            InitializeComponent();
        }


        private void BtnShowImageOfCustomer_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            GridLength currentGridLength = new GridLength(0, GridUnitType.Pixel);

            if (ColumnOfImageCustomer.Width == currentGridLength)
                ColumnOfImageCustomer.Width = new GridLength(350, GridUnitType.Pixel);
            else
                ColumnOfImageCustomer.Width = new GridLength(0, GridUnitType.Pixel);
        }
    }
}