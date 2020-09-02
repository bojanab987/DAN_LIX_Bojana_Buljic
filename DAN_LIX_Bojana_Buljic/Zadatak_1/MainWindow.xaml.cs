﻿using System.Windows;
using Zadatak_1.ViewModel;

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new StartMenuViewModel(this);
        }
    }
}
