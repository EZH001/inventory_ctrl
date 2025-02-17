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
    public class StaffWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SendStaff { get; }
        private WarehouseClient _warehouseClient;
        private List<Staff> _staffs;
        private Staff _selectedStaff;
        public event EventHandler<(int StaffId, string StaffName)> StaffSelected;

        private void OnStaffSelected(int staffId, string staffName)
        {
            if (SelectedStaff != null)
            {
                StaffSelected?.Invoke(this, (staffId, staffName)); // Передача id выбранного продукта
            }
        }
        public List<Staff> Staffs
        {

            get => _staffs;
            set => SetProperty(ref _staffs, value);
        }
        public Staff SelectedStaff
        {
            get => _selectedStaff;
            set => SetProperty(ref _selectedStaff, value);
        }
        private string _staffTitle;
        public string StaffTitle
        {
            get => _staffTitle;
            set => SetProperty(ref _staffTitle, value);
        }
        public StaffWindowViewModel(WarehouseClient warehouseClient)
        {
            _warehouseClient = warehouseClient;
            LoadStaffAsync();
            SendStaff = new RelayCommand(SendStaffAsync);
        }

        private async Task SendStaffAsync(object arg)
        {
            if (SelectedStaff != null)
            {
                OnStaffSelected(SelectedStaff.Id, SelectedStaff.last_name); // Вызов события
                if (Application.Current.Windows.OfType<Views.StaffWindow>().FirstOrDefault() is Views.StaffWindow window)
                {
                    window.DialogResult = true;
                    window.Close();
                }
            }
        }

        private bool CanOpenPropertyWindow(object obj)
        {
            return SelectedStaff != null;
        }

        private async Task LoadStaffAsync()
        {
            Staffs = await _warehouseClient.GetStaffs();
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
