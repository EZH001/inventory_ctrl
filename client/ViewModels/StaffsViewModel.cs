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
    public class StaffsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private WarehouseClient _warehouseClient;
        private List<Staff> _staffs;
        private Staff _selectedStaff;
        public ICommand OpenEditStaffWindowCommand { get; }
        public ICommand DeleteStaffCommand { get; }
        public ICommand OpenAddStaffWindowCommand { get; }

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
        public StaffsViewModel(WarehouseClient client)
        {
            _warehouseClient = client;
            LoadStaffsAsync();
            OpenEditStaffWindowCommand = new RelayCommand(OpenEditStaffWindowAsync);
            DeleteStaffCommand = new RelayCommand(DeleteStaffAsync);
            OpenAddStaffWindowCommand = new RelayCommand(OpenAddStaffWindowAsync);
        }

        private async Task OpenAddStaffWindowAsync(object arg)
        {
            AddStaffWindow addStaffWindow = new AddStaffWindow(_warehouseClient);
            bool? result = addStaffWindow.ShowDialog();
            if (result == true)
            {
                await LoadStaffsAsync();
            }
        }

        private async Task DeleteStaffAsync(object arg)
        {
            if (arg is Staff selectedStaff)
            {
                bool result = await _warehouseClient.DeleteStaff(selectedStaff.Id);
                if (result) await LoadStaffsAsync();
                else MessageBox.Show("Не удалось удалить сотрудника");
                SelectedStaff = null;
            }
        }

        private async Task OpenEditStaffWindowAsync(object arg)
        {
            if (arg is Staff selectedStaff)
            {
                EditStaffWindow editStaffWindow = new EditStaffWindow(selectedStaff, _warehouseClient);
                bool? result = editStaffWindow.ShowDialog();
                if (result == true)
                {
                    await LoadStaffsAsync();
                }
            }
            SelectedStaff = null;
        }

        private async Task LoadStaffsAsync()
        {
            Staffs = await _warehouseClient.GetStaffs();
        }

        protected virtual bool SetProperty<T>(ref T Staff, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(Staff, value)) return false;
            Staff = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
