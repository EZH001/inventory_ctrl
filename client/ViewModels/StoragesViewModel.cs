using client.Commands;
using client.Models;
using client.Views;
using Microsoft.EntityFrameworkCore.Internal;
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
    public class StoragesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private WarehouseClient _warehouseClient;
        private List<Storage> _storages;
        private Storage _selectedStorage;
        public ICommand OpenEditStorageWindowCommand { get; }
        public ICommand DeleteStorageCommand { get; }
        public ICommand OpenAddStorageWindowCommand { get; }

        public List<Storage> Storages{
            get => _storages;
            set => SetProperty(ref _storages, value);
        }
        public Storage SelectedStorage{
            get => _selectedStorage;
            set => SetProperty(ref _selectedStorage, value);
        }
        public StoragesViewModel(WarehouseClient client){
            _warehouseClient = client;
            LoadStoragesAsync();
            OpenEditStorageWindowCommand = new RelayCommand(OpenEditStorageWindowAsync);
            DeleteStorageCommand = new RelayCommand(DeleteStorageAsync);
            OpenAddStorageWindowCommand = new RelayCommand(OpenAddStorageWindowAsync);
        }

        private async Task OpenAddStorageWindowAsync(object arg)
        {
            AddStorageWindow addStorageWindow = new AddStorageWindow(_warehouseClient);
            bool? result = addStorageWindow.ShowDialog();
            if (result == true)
            {
                await LoadStoragesAsync();
            }
        }

        private async Task DeleteStorageAsync(object arg)
        {
            if (arg is Storage selectedStorage)
            {
                bool result = await _warehouseClient.DeleteStorage(selectedStorage.Id);
                if (result) await LoadStoragesAsync();
                else MessageBox.Show("Не удалось удалить продукт");
                SelectedStorage = null;
            }
        }

        private async Task OpenEditStorageWindowAsync(object arg)
        {
            if (arg is Storage selectedStorage)
            {
                EditStorageWindow editStorageWindow = new EditStorageWindow(selectedStorage, _warehouseClient);
                bool? result = editStorageWindow.ShowDialog();
                if (result == true)
                {
                    await LoadStoragesAsync();
                }
            }
            SelectedStorage = null;
        }

        private async Task LoadStoragesAsync()
        {
            Storages = await _warehouseClient.GetStorages();
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
