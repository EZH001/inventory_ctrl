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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace client.pages
{
    /// <summary>
    /// Логика взаимодействия для ExtraditionPage.xaml
    /// </summary>
    public partial class ExtraditionPage : Page
    {
        private WarehouseClient _client;
        public ExtraditionPage(WarehouseClient client)
        {   
            _client = client;
            InitializeComponent();
            DataContext = new ExtraditionPageViewModel(_client);
        }
    }
}
