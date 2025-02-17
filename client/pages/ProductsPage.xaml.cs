using client.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace client.pages
{
    /// <summary>
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {private WarehouseClient _client;
        public ProductsPage(WarehouseClient warehouseClient)
        {
            this._client = warehouseClient;
            InitializeComponent();
            DataContext = new ProductsViewModel(_client);
        }
        private void ProductsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var viewModel = DataContext as ProductsViewModel;
            viewModel.OpenPropertyProductWindowCommand.Execute(null);
        }
    }
}
