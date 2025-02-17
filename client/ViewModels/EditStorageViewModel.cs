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
    public class EditStorageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SaveEdit { get; }
        private WarehouseClient _warehouseClient;
        private Storage _selectedStorage;

        private List<Category> _categories;

        public Storage Storage
        {
            get => _selectedStorage;
            set => SetProperty(ref _selectedStorage, value);

        }
        public EditStorageViewModel(WarehouseClient warehouseClient, Storage storage)
        {
            _warehouseClient = warehouseClient;
            Storage = storage;
            LoadDataForComboBoxesAsync();
            SaveEdit = new RelayCommand(SaveStorageEditAsync);
        }

        private async Task SaveStorageEditAsync(object arg)
        {
            try
            {
                var result = await _warehouseClient.UpdateStorage(Storage);
                if (result)
                {
                    if (Application.Current.Windows.OfType<Views.EditStorageWindow>().FirstOrDefault() is Views.EditStorageWindow window)
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

