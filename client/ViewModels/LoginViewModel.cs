using client.Commands;
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
        private readonly TcpClient client;

        public LoginViewModel(TcpClient client)
        {
            this.client = client;
            LoginCommand = new RelayCommand(LoginAsync, CanLogin);
        }

        public LoginViewModel() : this(null) { } //Подумайте, как лучше инициализировать client здесь


        private bool CanLogin(object obj)
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && !IsLoggingIn;
        }

        private async Task LoginAsync(object obj)
        {
            IsLoggingIn = true;
            try
            {
                // Отправка запроса на сервер в формате JSON
                string requestJson = JsonConvert.SerializeObject(new { type = "auth", username = Username, password = Password });
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                await client.GetStream().WriteAsync(requestBytes, 0, requestBytes.Length);

                byte[] responseBytes = new byte[1024];
                int bytesRead = await client.GetStream().ReadAsync(responseBytes, 0, responseBytes.Length);
                if (bytesRead > 0)
                {
                    string responseJson = Encoding.UTF8.GetString(responseBytes, 0, bytesRead).Trim('\0');
                    dynamic response = JsonConvert.DeserializeObject(responseJson);

                    if ((bool)response.success)
                    {
                        MessageBox.Show("Успешная авторизация!");
                        // Переход к следующему окну
                    }
                    else
                    {
                        MessageBox.Show((string)response.message);
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
    }

    
}
