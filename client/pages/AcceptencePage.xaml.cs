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
    /// Логика взаимодействия для AcceptencePage.xaml
    /// </summary>
    public partial class AcceptencePage : Page
    {
        private WarehouseClient client;
        public AcceptencePage(WarehouseClient client, int user_id)
        {
            this.client = client;
            InitializeComponent();
            DataContext = new AddProductViewModel(client, user_id);
        }


    }
}
