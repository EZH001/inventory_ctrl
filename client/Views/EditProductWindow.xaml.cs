using client.Models;
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
using System.Windows.Shapes;

namespace client.Views
{
    /// <summary>
    /// Логика взаимодействия для EditProductWindowxaml.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        private Product product;
        private WarehouseClient Client;
        public EditProductWindow(Product _product, WarehouseClient client)
        {
            this.Client = client;
            this.product = _product;
            InitializeComponent();
            DataContext = new EditProductViewModel(Client, product);
            productTitleTB.Text = product.Title;
            productDescriptionTB.Text = product.Description;
            productQuantityTB.Text = product.QuantityInStock.ToString();
        }
    }
}
