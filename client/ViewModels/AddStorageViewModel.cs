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
    public class AddStorageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AddStorage { get; }
        private WarehouseClient _warehouseClient;
        private string _storageTitle;
        public string StorageTitle{
            get => _storageTitle;
            set => SetProperty(ref _storageTitle, value);
        }
        private int _categoryId;
        public int CategoryId
        {
            get => _categoryId;
            set => SetProperty(ref _categoryId, value);
        }

        private List<Category> _categories;
        public AddStorageViewModel(WarehouseClient warehouseClient)
        {
            _warehouseClient = warehouseClient;
            LoadDataForComboBoxesAsync();
            AddStorage = new RelayCommand(SaveStorageAddAsync);
        }

        private async Task SaveStorageAddAsync(object arg)
        {
            try
            {
                var result = await _warehouseClient.AddStorage(StorageTitle, CategoryId);
                if (result)
                {
                    if (Application.Current.Windows.OfType<Views.AddStorageWindow>().FirstOrDefault() is Views.AddStorageWindow window)
                    {
                        window.DialogResult = true;
                        window.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось сохранить место хранения");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении места хранения: {ex.Message}");
            }
        }

        public List<Category> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
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

        private async Task LoadDataForComboBoxesAsync()
        {
            await LoadCategoriesAsync();
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}