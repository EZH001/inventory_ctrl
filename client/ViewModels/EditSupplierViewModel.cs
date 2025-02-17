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
    public class EditSupplierViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SaveEdit { get; }
        private WarehouseClient _warehouseClient;
        private Supplier _selectedSupplier;

        public Supplier Supplier
        {
            get => _selectedSupplier;
            set => SetProperty(ref _selectedSupplier, value);

        }
        public EditSupplierViewModel(WarehouseClient warehouseClient, Supplier supplier)
        {
            _warehouseClient = warehouseClient;
            Supplier = supplier;
            SaveEdit = new RelayCommand(SaveSupplierEditAsync);
        }

        private async Task SaveSupplierEditAsync(object arg)
        {
            try
            {
                var result = await _warehouseClient.UpdateSupplier(Supplier);
                if (result)
                {
                    if (Application.Current.Windows.OfType<Views.EditSupplierWindow>().FirstOrDefault() is Views.EditSupplierWindow window)
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
