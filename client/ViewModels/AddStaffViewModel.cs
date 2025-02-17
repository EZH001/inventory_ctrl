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
    public class AddStaffViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AddCommand { get; }
        private WarehouseClient _warehouseClient;
        private Staff _staffs;

        public Staff Staff
        {
            get => _staffs;
            set => SetProperty(ref _staffs, value);
        }
        public AddStaffViewModel(WarehouseClient warehouseClient)
        {
            _warehouseClient = warehouseClient;
            Staff = new Staff();
            AddCommand = new RelayCommand(AddStaffAsync);
        }

        private async Task AddStaffAsync(object arg)
        {
            try
            {
                if (!String.IsNullOrEmpty(Staff.first_name) && !String.IsNullOrEmpty(Staff.last_name) && !String.IsNullOrEmpty(Staff.post))
                {
                    var result = await _warehouseClient.AddStaff(Staff);
                    if (result)
                    {
                        if (result)
                        {
                            if (Application.Current.Windows.OfType<Views.AddStaffWindow>().FirstOrDefault() is Views.AddStaffWindow window)
                            {
                                window.DialogResult = true;
                                window.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Не удалось сохранить сотрудника");
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
