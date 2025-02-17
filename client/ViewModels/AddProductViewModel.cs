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
    public class AddProductViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AddCommand { get; }
        private WarehouseClient _warehouseClient;
        private Product _product;

        private List<StorageUnit> _storageUnits;
        private List<Category> _categories;
        private List<Storage> _storages;
        private List<Supplier> _suppliers;

        public Product Product
        {
            get => _product;
            set => SetProperty(ref _product, value);

        }
        private int _user_id;
        public int User_id
        {
            get => _user_id;
            set => SetProperty(ref _user_id, value);

        }
        public AddProductViewModel(WarehouseClient warehouseClient, int user_id)
        {
            _warehouseClient = warehouseClient;
            Product = new Product();
            User_id = user_id;
            LoadDataForComboBoxesAsync();
            AddCommand = new RelayCommand(AddProductAsync);
        }

        private async Task AddProductAsync(object arg)
        {
            try
            {
                if (!String.IsNullOrEmpty(Product.Title) && !String.IsNullOrEmpty(Product.Description) && Product.QuantityInStock != null && Product.QuantityInStock != 0){
                    Product.User_id = User_id;
                    var result = await _warehouseClient.AddProduct(Product);
                    if (result)
                    {
                        MessageBox.Show("Товар принят на склад");
                    }
                    else
                    {
                        MessageBox.Show("Не удалось сохранить изменения");

                    }
                }
                else MessageBox.Show("Введите данные");
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
        public List<Supplier> Suppliers
        {
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

