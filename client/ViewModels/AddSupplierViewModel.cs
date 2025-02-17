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
    public class AddSupplierViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AddCommand { get; }
        private WarehouseClient _warehouseClient;
        private Supplier _suppliers;

        public Supplier Supplier
        {
            get => _suppliers;
            set => SetProperty(ref _suppliers, value);
        }
        public AddSupplierViewModel(WarehouseClient warehouseClient)
        {
            _warehouseClient = warehouseClient;
            Supplier = new Supplier();
            AddCommand = new RelayCommand(AddSupplierAsync);
        }

        private async Task AddSupplierAsync(object arg)
        {
            try
            {
                if (!String.IsNullOrEmpty(Supplier.Title) && !String.IsNullOrEmpty(Supplier.contact_person) && !String.IsNullOrEmpty(Supplier.phone_number) && !String.IsNullOrEmpty(Supplier.email) && !String.IsNullOrEmpty(Supplier.address))
                {
                    var result = await _warehouseClient.AddSupplier(Supplier);
                    if (result)
                    {
                        if (Application.Current.Windows.OfType<Views.AddSupplierWindow>().FirstOrDefault() is Views.AddSupplierWindow window)
                        {
                            window.DialogResult = true;
                            window.Close();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось сохранить поставщика");
                        }
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
