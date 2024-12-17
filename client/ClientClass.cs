using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace client
{
    public class WarehouseClient
    {
        private TcpClient client;
        private string serverIp;
        public bool IsConnected { get; private set; }

        public WarehouseClient(string serverIp)
        {
            this.serverIp = serverIp;
            IsConnected = false;
        }

        public async Task<bool> Connect()
        {
            try
            {
                if (client == null || !client.Connected)
                {
                    client = new TcpClient();
                    await client.ConnectAsync(serverIp, 53551);
                    IsConnected = true;
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> Authenticate(string username, string password)
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Соединение с сервером не установлено");
                return false;
            }
            try
            {
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                string requestJson = JsonConvert.SerializeObject(new { type = "auth", username = username, password = password });
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
                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка авторизации: {(string)response.message}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка аутентификации: {ex.Message}");
                return false;
            }
            return false;

        }

        public void Disconnect()
        {
            client?.Close();
        }

        //public TcpClient GetClient()
        //{
        //    return client;
        //}
    }
}

