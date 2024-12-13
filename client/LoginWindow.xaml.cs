using client.ViewModels;
using System;
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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private TcpClient client; // Сюда будет храниться подключение к серверу
        public LoginViewModel ViewModel { get; set; }

        public LoginWindow(TcpClient client)
        {
            this.client = client;
            InitializeComponent();
            ViewModel = new LoginViewModel(client); // передаем сокет в ViewModel
            DataContext = ViewModel;
        }

        private void passTB_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.Password = ((PasswordBox)sender).Password.ToString();
        }
    }
}
