﻿using client.ViewModels;
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
using System.Windows.Shapes;

namespace client.Views
{
    /// <summary>
    /// Логика взаимодействия для ExtraditionWindow.xaml
    /// </summary>
    public partial class ExtraditionWindow : Window
    {   
        WarehouseClient client;
        public ExtraditionWindow(WarehouseClient client)
        {
            InitializeComponent();
            this.client = client;
            DataContext = new ExtraditionWindowViewModel(client);
        }
    }
}
