using client.Commands;
using client.Models;
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
    public class EditProductViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SaveEdit { get; }
        private WarehouseClient _warehouseClient;
        private Product _selectedProduct;

        private List<StorageUnit> _storageUnits;
        private List<Category> _categories;
        private List<Storage> _storages;
        private List<Supplier> _suppliers;

        public Product Product
        {
            get => _selectedProduct;
            set => SetProperty(ref _selectedProduct, value);

        }
        public EditProductViewModel(WarehouseClient warehouseClient, Product product)
        {
            _warehouseClient = warehouseClient;
            Product = product;
            LoadDataForComboBoxesAsync();
            SaveEdit = new RelayCommand(SaveProductEditAsync);
        }

        private async Task SaveProductEditAsync(object arg)
        {
            try
            {
                var result = await _warehouseClient.UpdateProduct(Product);
                if (result)
                {
                    if (Application.Current.Windows.OfType<Views.EditProductWindow>().FirstOrDefault() is Views.EditProductWindow window)
                    {
                        window.DialogResult = true;
                        window.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось сохранить изменения");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}");
            }
        }

        public List<StorageUnit> StorageUnits
        {
            get => _storageUnits;
            set => SetProperty(ref _storageUnits, value);
        }
        public List<Category> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }
        public List<Storage> Storages
        {
            get => _storages;
            set => SetProperty(ref _storages, value);
        }
        public List<Supplier> Suppliers{
            get => _suppliers;
            set => SetProperty(ref _suppliers, value);
        }
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        private async Task LoadCategoriesAsync()
        {
            Categories = await _warehouseClient.GetCategories();
        }
        private async Task LoadStorageUnitsAsync()
        {
            StorageUnits = await _warehouseClient.GetStorageUnits();
        }
        private async Task LoadStoragesAsync()
        {
            Storages = await _warehouseClient.GetStorages();
        }

        private async Task LoadSuppliersAsync()
        {
            Suppliers = await _warehouseClient.GetSuppliers();
        }
        private async Task LoadDataForComboBoxesAsync()
        {
            await LoadStorageUnitsAsync();
            await LoadCategoriesAsync();
            await LoadStoragesAsync();
            await LoadSuppliersAsync(); 
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
