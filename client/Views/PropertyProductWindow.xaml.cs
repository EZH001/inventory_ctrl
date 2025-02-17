using client.Models;
using Newtonsoft.Json;
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
    /// Логика взаимодействия для PropertyProductWindow.xaml
    /// </summary>
    public partial class PropertyProductWindow : Window
    {
        private Product product;
        private WarehouseClient Client;
        
         public PropertyProductWindow(Product _product, WarehouseClient client)
        {   this.Client = client;
            this.product = _product;            
            InitializeComponent();
            UpdateUserData(product);
        }
        public async void UpdateUserData(Product product)
        {
            string userName = await Client.GetUsername(product.User_id);
            if (!string.IsNullOrEmpty(userName))
            {
                PropertyProductTB.Text = $"Наименование: {product.Title}\n" +
                                         $"Описание: {product.Description}\n" +
                                         $"Категория: {product.Category.Title}\n" +
                                         $"Кол-во на складе: {product.QuantityInStock}\n" +
                                         $"Единицы измерения: {product.StorageUnit.Title}\n" +
                                         $"Создан: {product.CreatedAt}\n" +
                                         $"Изменён: {product.UpdateAt}\n" +
                                         $"Стеллаж и ячейка: {product.Storage.Title}\n" +
                                         $"Добавлен: {userName}\n" +
                                         $"Поставщик: {product.Supplier.Title}";
            }
        }
    }
}
