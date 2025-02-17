using client.Commands;
using client.Models;
using client.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace client.ViewModels
{
    public class ProductsWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand OpenPropertyProductWindowCommand { get; }
        public ICommand SendProduct{  get; }
        private WarehouseClient _warehouseClient;
        private List<Product> _products;
        private Product _selectedProduct;
        public event EventHandler<(int ProductId, string ProductName)> ProductSelected;

        private void OnProductSelected(int productId, string productName)
        {
            if (SelectedProduct != null)
            {
                ProductSelected?.Invoke(this, (productId, productName)); // Передача id выбранного продукта
            }
        }
        public List<Product> Products
        {

            get => _products;
            set => SetProperty(ref _products, value);
        }
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set => SetProperty(ref _selectedProduct, value);
        }
        private string _productTitle;
        public string ProductTitle
        {
            get => _productTitle;
            set => SetProperty(ref _productTitle, value);
        }
        public ProductsWindowViewModel(WarehouseClient warehouseClient)
        {
            _warehouseClient = warehouseClient;
            LoadProductAsync();
            OpenPropertyProductWindowCommand = new RelayCommand(OpenPropertyProductWindowAsync, CanOpenPropertyWindow);
            SendProduct = new RelayCommand(SendProductAsync);
        }

        private async Task SendProductAsync(object arg)
        {
            if (SelectedProduct != null)
            {
                OnProductSelected(SelectedProduct.Id, SelectedProduct.Title); // Вызов события
                if (Application.Current.Windows.OfType<Views.ProductWindow>().FirstOrDefault() is Views.ProductWindow window)
                {
                    window.DialogResult = true;
                    window.Close();
                }
            }
        }

        private async Task OpenPropertyProductWindowAsync(object obj)
        {
            await Task.Run(() =>
            {
                if (SelectedProduct != null)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        PropertyProductWindow productWindow = new PropertyProductWindow(SelectedProduct, _warehouseClient);
                        productWindow.ShowDialog();
                    });
                    SelectedProduct = null;
                }
            });
        }

        private bool CanOpenPropertyWindow(object obj)
        {
            return SelectedProduct != null;
        }

        private async Task LoadProductAsync()
        {
            if (String.IsNullOrEmpty(ProductTitle)) Products = await _warehouseClient.GetProducts();
            else Products = await _warehouseClient.SearchProduct(ProductTitle);
        }
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}