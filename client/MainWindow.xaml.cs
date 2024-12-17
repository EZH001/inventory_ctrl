﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WarehouseClient Client;
        public MainWindow(WarehouseClient client)
        {
            this.Client = client;
            InitializeComponent();
        }

        private void productsBtn_Click(object sender, RoutedEventArgs e)
        {
            productsBtn.Style = (Style)this.FindResource("ButtonStyle2");
            acceptanceBtn.Style = (Style)this.FindResource("ButtonStyle1");            
            extraditionBtn.Style = (Style)this.FindResource("ButtonStyle1");
            storageBtn.Style = (Style)this.FindResource("ButtonStyle1");
            Frame.NavigationService.Navigate(new Uri("pages/ProductsPage.xaml", UriKind.Relative));
            this.Title = "Товары";
        }

        private void storageBtn_Click(object sender, RoutedEventArgs e)
        {
            productsBtn.Style = (Style)this.FindResource("ButtonStyle1");
            acceptanceBtn.Style = (Style)this.FindResource("ButtonStyle1");
            extraditionBtn.Style = (Style)this.FindResource("ButtonStyle1");
            storageBtn.Style = (Style)this.FindResource("ButtonStyle2");
            //Frame.NavigationService.Navigate(new Uri("pages/ProductsPage.xaml", UriKind.Relative));
            this.Title = "Места хранения";
        }

        private void acceptanceBtn_Click(object sender, RoutedEventArgs e)
        {
            productsBtn.Style = (Style)this.FindResource("ButtonStyle1");
            acceptanceBtn.Style = (Style)this.FindResource("ButtonStyle2");
            extraditionBtn.Style = (Style)this.FindResource("ButtonStyle1");
            storageBtn.Style = (Style)this.FindResource("ButtonStyle1");
            //Frame.NavigationService.Navigate(new Uri("pages/ProductsPage.xaml", UriKind.Relative));
            this.Title = "Приёмка товаров";
        }

        private void extraditionBtn_Click(object sender, RoutedEventArgs e)
        {
            productsBtn.Style = (Style)this.FindResource("ButtonStyle1");
            acceptanceBtn.Style = (Style)this.FindResource("ButtonStyle1");
            extraditionBtn.Style = (Style)this.FindResource("ButtonStyle2");
            storageBtn.Style = (Style)this.FindResource("ButtonStyle1");
            //Frame.NavigationService.Navigate(new Uri("pages/ProductsPage.xaml", UriKind.Relative));
            this.Title = "Выдача товаров";
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow(Client);
            loginWindow.Show();
            this.Close();
        }
    }
}
