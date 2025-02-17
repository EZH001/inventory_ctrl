using client.Commands;
using client.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace client.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Window LoginWindow { get; set; }

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private bool _isLoggingIn;
        public bool IsLoggingIn
        {
            get => _isLoggingIn;
            set => SetProperty(ref _isLoggingIn, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand LoginCommand { get; }
        private WarehouseClient _warehouseClient;

        public LoginViewModel(WarehouseClient warehouseClient)
        {
            this._warehouseClient = warehouseClient;
            LoginCommand = new RelayCommand(LoginAsync, CanLogin);
        }

        public LoginViewModel() : this(null) { } 


        private bool CanLogin(object obj)
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && !IsLoggingIn;
        }

        private async Task LoginAsync(object obj)
        {
            IsLoggingIn = true;
            try
            {
               
                var result = await _warehouseClient.Authenticate(Username, Password);
                if (result != null) {
                    dynamic response = JsonConvert.DeserializeObject(result);
                    if ((bool)response.success)
                    {
                        MainWindow mainWindow = new MainWindow(_warehouseClient, response);
                        mainWindow.Show();
                        
                        LoginWindow?.Close();
                    }
                }  
            }

            catch (JsonReaderException ex)
            {
                MessageBox.Show($"Ошибка разбора JSON ответа: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка авторизации: {ex.Message}");
            }
            finally
            {
                IsLoggingIn = false;
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
    }}
