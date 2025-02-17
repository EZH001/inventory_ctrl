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
using System.Windows.Input;

namespace client.ViewModels
{
    public class ExtraditionPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private WarehouseClient _warehouseClient;
        private List<Extradition> _extraditions;

        public ICommand OpenExtraditionWindow{  get; }
        public List<Extradition> Extraditions
        {
            get => _extraditions;
            set => SetProperty(ref _extraditions, value);
        }

        public ExtraditionPageViewModel(WarehouseClient warehouseClient) {
            _warehouseClient = warehouseClient;
            LoadExtraditionAsync();
            OpenExtraditionWindow = new RelayCommand(OpenExtraditionWindowAsync);
        }

        private async Task OpenExtraditionWindowAsync(object arg)
        {
           ExtraditionWindow extraditionWindow = new ExtraditionWindow(_warehouseClient);
            bool? result = extraditionWindow.ShowDialog();
            if (result == true)
            {
                await LoadExtraditionAsync();
            }
        }

        private async Task LoadExtraditionAsync()
        {
           Extraditions = await _warehouseClient.GetExtraditions();
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
