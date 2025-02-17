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
    public class ProductsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand OpenPropertyProductWindowCommand { get; }
        public ICommand OpenEditProductWindowCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ICommand SearchProductCommand { get; }
        private WarehouseClient _warehouseClient;
        private List<Product> _products;
        private Product _selectedProduct;
        public List<Product> Products{

            get => _products;
            set => SetProperty(ref _products, value);
        }
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set => SetProperty(ref _selectedProduct, value);
        }
        private string _productTitle;
        public string ProductTitle{
            get => _productTitle;
            set => SetProperty(ref _productTitle, value);
        }
        public ProductsViewModel(WarehouseClient warehouseClient)
        {
            _warehouseClient = warehouseClient;
            SearchProductCommand = new RelayCommand(SearchProductAsync);
            LoadProductAsync();
            OpenPropertyProductWindowCommand = new RelayCommand(OpenPropertyProductWindowAsync, CanOpenPropertyWindow);
            OpenEditProductWindowCommand = new RelayCommand(OpenEditProductWindowAsync);
            DeleteProductCommand = new RelayCommand(DeleteProductAsync);
            
        }

        private async Task SearchProductAsync(object arg)
        {
            LoadProductAsync();
        }

        private async Task OpenEditProductWindowAsync(object obj)
        {
            if (obj is Product selectedProduct)
            {
                EditProductWindow editProductWindow = new EditProductWindow(selectedProduct, _warehouseClient);
                bool? result = editProductWindow.ShowDialog();
                if (result == true)
                {
                    await LoadProductAsync();
                }
            }
            SelectedProduct = null;
        }

        private async Task OpenPropertyProductWindowAsync(object obj)
        {
            await Task.Run(() =>
            { 
                if (SelectedProduct != null){
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
            if(String.IsNullOrEmpty(ProductTitle)) Products = await _warehouseClient.GetProducts();
            else Products = await _warehouseClient.SearchProduct(ProductTitle);
        }

        private async Task DeleteProductAsync(object obj){
            if (obj is Product selectedProduct)
            {
                bool result = await _warehouseClient.DeleteProduct(selectedProduct.Id);
                if (result) await LoadProductAsync();
                else MessageBox.Show("Не удалось удалить продукт");
                SelectedProduct = null;
            }
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
