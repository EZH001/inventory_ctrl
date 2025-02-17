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
    /// Логика взаимодействия для EditStorageWindow.xaml
    /// </summary>
    public partial class EditStorageWindow : Window
    {
        private WarehouseClient Client;
        private Storage storage;
        public EditStorageWindow(Storage _storage, WarehouseClient client)
        {   this.Client = client;
            this.storage = _storage;    
            InitializeComponent();
            DataContext = new EditStorageViewModel(Client, storage);
            storageTitleTB.Text = storage.Title;
        }
    }
}
