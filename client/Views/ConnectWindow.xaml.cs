﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для ConnectWindow.xaml
    /// </summary>
    public partial class ConnectWindow : Window, INotifyPropertyChanged
    {
        private string _ipAddress;
        private bool _isConnecting;// Добавлено для индикации процесса
        public WarehouseClient Client { get; private set; }

        public string IpAddress
        {
            get => _ipAddress;
            set
            {
                _ipAddress = value;
                OnPropertyChanged();
            }
        }

        public bool IsConnecting // Свойство для индикации подключения
        {
            get => _isConnecting;
            set
            {
                _isConnecting = value;
                OnPropertyChanged();
            }
        }


        public ConnectWindow()
        {
            InitializeComponent();
            DataContext = this;
            connectTB.SetBinding(TextBox.TextProperty, new Binding("IpAddress"));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void connectBtn_Click(object sender, RoutedEventArgs e)
        {
            IsConnecting = true; // Устанавливаем флаг подключения

            if (string.IsNullOrEmpty(IpAddress))
            {
                MessageBox.Show("Пожалуйста, введите IP-адрес.");
                IsConnecting = false;
                return;
            }

            try
            {

                Client = new WarehouseClient(IpAddress);
                var result = await Client.Connect();
                if (result)
                {
                    var loginWindow = new LoginWindow(Client);
                    //await Сlient.ConnectAsync(IpAddress, 53551);// Замените 8080 на ваш порт
                    // Подключение успешно 
                        //this.DialogResult = true;
                        this.Close();
                    loginWindow.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
                IsConnecting = false; // Снимаем флаг подключения
            }
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
