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
    /// Логика взаимодействия для AddStorageWindow.xaml
    /// </summary>
    public partial class AddStorageWindow : Window
    {   
        private WarehouseClient _client;
        public AddStorageWindow(WarehouseClient client)
        {
            InitializeComponent();
            _client = client;
            DataContext = new AddStorageViewModel(_client);
        }
    }
}
