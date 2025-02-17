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
    public class SuppliersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private WarehouseClient _warehouseClient;
        private List<Supplier> _suppliers;
        private Supplier _selectedSupplier;
        public ICommand OpenEditSupplierWindowCommand { get; }
        public ICommand DeleteSupplierCommand { get; }
        public ICommand OpenAddSupplierWindowCommand { get; }

        public List<Supplier> Suppliers
        {
            get => _suppliers;
            set => SetProperty(ref _suppliers, value);
        }
        public Supplier SelectedSupplier
        {
            get => _selectedSupplier;
            set => SetProperty(ref _selectedSupplier, value);
        }
        public SuppliersViewModel(WarehouseClient client)
        {
            _warehouseClient = client;
            LoadSuppliersAsync();
            OpenEditSupplierWindowCommand = new RelayCommand(OpenEditSupplierWindowAsync);
            DeleteSupplierCommand = new RelayCommand(DeleteSupplierAsync);
            OpenAddSupplierWindowCommand = new RelayCommand(OpenAddSupplierWindowAsync);
        }

        private async Task OpenAddSupplierWindowAsync(object arg)
        {
            AddSupplierWindow addSupplierWindow = new AddSupplierWindow(_warehouseClient);
            bool? result = addSupplierWindow.ShowDialog();
            if (result == true)
            {
                await LoadSuppliersAsync();
            }
        }

        private async Task DeleteSupplierAsync(object arg)
        {
            if (arg is Supplier selectedSupplier)
            {
                bool result = await _warehouseClient.DeleteSupplier(selectedSupplier.Id);
                if (result) await LoadSuppliersAsync();
                else MessageBox.Show("Не удалось удалить поставщика");
                SelectedSupplier = null;
            }
        }

        private async Task OpenEditSupplierWindowAsync(object arg)
        {
            if (arg is Supplier selectedSupplier)
            {
                EditSupplierWindow editSupplierWindow = new EditSupplierWindow(selectedSupplier, _warehouseClient);
                bool? result = editSupplierWindow.ShowDialog();
                if (result == true)
                {
                    await LoadSuppliersAsync();
                }
            }
            SelectedSupplier = null;
        }

        private async Task LoadSuppliersAsync()
        {
            Suppliers = await _warehouseClient.GetSuppliers();
        }

        protected virtual bool SetProperty<T>(ref T Supplier, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(Supplier, value)) return false;
            Supplier = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
