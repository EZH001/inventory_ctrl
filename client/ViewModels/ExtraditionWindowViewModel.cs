using client.Commands;
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
    public class ExtraditionWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private WarehouseClient _warehouseClient;
        public event EventHandler<(int ProductId, string ProductName)> ProductSelected;
        public ICommand OpenProductWindow { get; }
        public ICommand OpenStaffWindow { get; }
        public ICommand ExtraditionProductCommand { get; }
        private int _selectedProductId;
        private string _selectedProductName;
        private decimal _productQuantity;
        public decimal ProductQuantity
        {
            get => _productQuantity;
            set => SetProperty(ref _productQuantity, value);
        }

        public string SelectedProductName
        {
            get => _selectedProductName;
            set => SetProperty(ref _selectedProductName, value);
        }
        private int _selectedStaffId;
        private string _selectedStaffName;

        public string SelectedStaffName
        {
            get => _selectedStaffName;
            set => SetProperty(ref _selectedStaffName, value);
        }

        public ExtraditionWindowViewModel(WarehouseClient client)
        {
            _warehouseClient = client;
            OpenProductWindow = new RelayCommand(OpenProductWindowAsync);
            OpenStaffWindow = new RelayCommand(OpenStaffWindowAsync);
            ExtraditionProductCommand = new RelayCommand(ExtraditionProductAsync);
        }

        private async Task ExtraditionProductAsync(object arg)
        {
            bool result = false;
            if (_selectedProductId == 0 || _selectedStaffId == 0) MessageBox.Show("Выберите товар и получателя!");
            else if (ProductQuantity == null) MessageBox.Show("Введите кол-во выдаваемого товара!");
            else if (ProductQuantity <= 0) MessageBox.Show("Введите корректное кол-во выдаваемого товара!");
            else result = await _warehouseClient.ExtraditionProduct(_selectedProductId, _selectedStaffId, ProductQuantity);
            if (result)
            {
                if (Application.Current.Windows.OfType<Views.ExtraditionWindow>().FirstOrDefault() is Views.ExtraditionWindow window)
                {
                    window.DialogResult = true;
                    window.Close();
                }
            }
            else
            {
                MessageBox.Show("Не удалось сохранить выдачу товаров");
            }
        }

        private async Task OpenProductWindowAsync(object arg)
        {
            try
            {
                var productWindow = new ProductWindow(_warehouseClient);
                var viewModel = (ProductsWindowViewModel)productWindow.DataContext;

                viewModel.ProductSelected += OnProductSelected;

                bool? result = productWindow.ShowDialog();

                // Отписка от события после закрытия окна
                viewModel.ProductSelected -= OnProductSelected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task OpenStaffWindowAsync(object arg)
        {
            try
            {
                var staffWindow = new StaffWindow(_warehouseClient);
                var viewModel = (StaffWindowViewModel)staffWindow.DataContext;

                viewModel.StaffSelected += OnStaffSelected;

                bool? result = staffWindow.ShowDialog();

                // Отписка от события после закрытия окна
                viewModel.StaffSelected -= OnStaffSelected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void OnProductSelected(object sender, (int ProductId, string ProductName) e)
        {
            Console.WriteLine($"Product Selected: {e.ProductId}, {e.ProductName}"); // Временное сообщение
            SelectedProductName = e.ProductName;
            _selectedProductId = e.ProductId;
        }

        private void OnStaffSelected(object sender, (int StaffId, string StaffName) e)
        {
            Console.WriteLine($"Product Selected: {e.StaffId}, {e.StaffName}"); // Временное сообщение
            SelectedStaffName = e.StaffName;
            _selectedStaffId = e.StaffId;
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

