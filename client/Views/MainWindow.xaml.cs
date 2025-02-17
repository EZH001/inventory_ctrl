using client.pages;
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

namespace client.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WarehouseClient Client;
        private dynamic userData;
        private ProductsPage _productsPage;
        private StoragePage _storagePage;
        private AcceptencePage _acceptencePage;
        private SupplierPage _supplierPage;
        private StaffPage _staffPage;
        private ExtraditionPage _extraditionPage;
        public MainWindow(WarehouseClient client, dynamic response)
        {
            this.Client = client;
            this.userData = response;            
            InitializeComponent();
            usernameTB.Text = userData.last_name + " " + userData.first_name;

        }

        private void productsBtn_Click(object sender, RoutedEventArgs e)
        {
            productsBtn.Style = (Style)this.FindResource("ButtonStyle2");
            acceptanceBtn.Style = (Style)this.FindResource("ButtonStyle1");     
            extraditionBtn.Style = (Style)this.FindResource("ButtonStyle1");
            storageBtn.Style = (Style)this.FindResource("ButtonStyle1");
            supplierBtn.Style = (Style)this.FindResource("ButtonStyle1");
            staffBtn.Style = (Style)this.FindResource("ButtonStyle1");
            _productsPage = new ProductsPage(Client);
            Frame.NavigationService.Navigate(_productsPage);
            this.Title = "Товары";
        }

        private void storageBtn_Click(object sender, RoutedEventArgs e)
        {
            productsBtn.Style = (Style)this.FindResource("ButtonStyle1");
            acceptanceBtn.Style = (Style)this.FindResource("ButtonStyle1");
            extraditionBtn.Style = (Style)this.FindResource("ButtonStyle1");  
            storageBtn.Style = (Style)this.FindResource("ButtonStyle2");
            supplierBtn.Style = (Style)this.FindResource("ButtonStyle1");
            staffBtn.Style = (Style)this.FindResource("ButtonStyle1");
            _storagePage = new StoragePage(Client);
            Frame.NavigationService.Navigate(_storagePage);
            this.Title = "Места хранения";
        }

        private void acceptanceBtn_Click(object sender, RoutedEventArgs e)
        {
            productsBtn.Style = (Style)this.FindResource("ButtonStyle1");
            acceptanceBtn.Style = (Style)this.FindResource("ButtonStyle2");
            extraditionBtn.Style = (Style)this.FindResource("ButtonStyle1");
            storageBtn.Style = (Style)this.FindResource("ButtonStyle1");
            supplierBtn.Style = (Style)this.FindResource("ButtonStyle1");
            staffBtn.Style = (Style)this.FindResource("ButtonStyle1");
            _acceptencePage = new AcceptencePage(Client, (int)userData.user_id);
            Frame.NavigationService.Navigate(_acceptencePage);
            this.Title = "Приёмка товаров";
        }

        private void extraditionBtn_Click(object sender, RoutedEventArgs e)
        {
            productsBtn.Style = (Style)this.FindResource("ButtonStyle1");
            acceptanceBtn.Style = (Style)this.FindResource("ButtonStyle1");
            extraditionBtn.Style = (Style)this.FindResource("ButtonStyle2");
            storageBtn.Style = (Style)this.FindResource("ButtonStyle1");
            supplierBtn.Style = (Style)this.FindResource("ButtonStyle1");
            staffBtn.Style = (Style)this.FindResource("ButtonStyle1");
            _extraditionPage = new ExtraditionPage(Client);
            Frame.NavigationService.Navigate(_extraditionPage);
            this.Title = "Выдача товаров";
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow(Client);
            loginWindow.Show();
            this.Close();
        }

        private void supplierBtn_Click(object sender, RoutedEventArgs e)
        {
            productsBtn.Style = (Style)this.FindResource("ButtonStyle1");
            acceptanceBtn.Style = (Style)this.FindResource("ButtonStyle1");
            extraditionBtn.Style = (Style)this.FindResource("ButtonStyle1");
            storageBtn.Style = (Style)this.FindResource("ButtonStyle1");
            supplierBtn.Style = (Style)this.FindResource("ButtonStyle2");
            staffBtn.Style = (Style)this.FindResource("ButtonStyle1");
            _supplierPage = new SupplierPage(Client);
            Frame.NavigationService.Navigate(_supplierPage);
            this.Title = "Поставщики";
        }

        private void staffBtn_Click(object sender, RoutedEventArgs e)
        {
            productsBtn.Style = (Style)this.FindResource("ButtonStyle1");
            acceptanceBtn.Style = (Style)this.FindResource("ButtonStyle1");
            extraditionBtn.Style = (Style)this.FindResource("ButtonStyle1");
            storageBtn.Style = (Style)this.FindResource("ButtonStyle1");
            supplierBtn.Style = (Style)this.FindResource("ButtonStyle1");
            staffBtn.Style = (Style)this.FindResource("ButtonStyle2");
            _staffPage = new StaffPage(Client);
            Frame.NavigationService.Navigate(_staffPage);
            this.Title = "Сотрудники";
        }
    }
}
