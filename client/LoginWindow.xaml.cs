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
         // Сюда будет храниться подключение к серверу
        public LoginViewModel ViewModel { get; set; }

        public LoginWindow(WarehouseClient warehouseClient)
        {
            
            InitializeComponent();
            ViewModel = new LoginViewModel(warehouseClient); // передаем сокет в ViewModel
            DataContext = ViewModel;
            ViewModel.LoginWindow = this;
        }
       

    private void passTB_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.Password = ((PasswordBox)sender).Password.ToString();
        }
    }
}
