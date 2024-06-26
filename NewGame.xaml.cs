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

namespace connectFour
{
    /// <summary>
    /// Interaction logic for NewGame.xaml
    /// </summary>
    public partial class NewGame : Window
    {
        public int[] data;

        public NewGame()
        {
            InitializeComponent();
        }

        private void B7x6_Click(object sender, RoutedEventArgs e)
        {
            data = [7, 6];
            DialogResult = true;
        }

        private void B5x4_Click(object sender, RoutedEventArgs e)
        {
            data = [5, 4];
            DialogResult = true;
        }

        private void B6x5_Click(object sender, RoutedEventArgs e)
        {
            data = [6, 5];
            DialogResult = true;
        }

        private void B8x7_Click(object sender, RoutedEventArgs e)
        {
            data = [8, 7];
            DialogResult = true;
        }

        private void B9x7_Click(object sender, RoutedEventArgs e)
        {
            data = [9, 7];
            DialogResult = true;
        }

        private void B10x7_Click(object sender, RoutedEventArgs e)
        {
            data = [10, 7];
            DialogResult = true;
        }

        private void B8x8_Click(object sender, RoutedEventArgs e)
        {
            data = [8, 8];
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}