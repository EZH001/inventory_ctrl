using client.Models;
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

namespace client
{
    /// <summary>
    /// Логика взаимодействия для PropertyProductWindow.xaml
    /// </summary>
    public partial class PropertyProductWindow : Window
    {
        private Product product;
        public PropertyProductWindow(Product _product)
        {   
            this.product = _product;    
            InitializeComponent();
            PropertyProductTB.Text = $"Наименование: {product.Title}\n " +
                                     $"Описание: {product.Description}\n " +
                                     $"Категория: {product.Category.Title}\n " +
                                     $"Кол-во на складе: {product.QuantityInStock}\n" +
                                     $"Единицы измерения: {product.StorageUnit.Title}\n" +
                                     $"Создан: {product.CreatedAt}\n" +
                                     $"Изменён: {product.UpdateAt}\n" +
                                     $"Добавлен: \n";
        }
    }
}
