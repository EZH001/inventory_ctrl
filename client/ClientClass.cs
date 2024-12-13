using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace client
{
    public class WarehouseClient
    {
        private TcpClient client;
        private string serverIp;

        public WarehouseClient(string serverIp)
        {
            this.serverIp = serverIp;
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            try
            {
                client = new TcpClient();
                await client.ConnectAsync(serverIp, 53551);

                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                string authRequest = $"auth|{username}|{password}";
                await writer.WriteLineAsync(authRequest);
                await writer.FlushAsync();

                string response = await reader.ReadLineAsync();
                string[] parts = response.Split('|');
                if (parts[0] == "auth" && parts[1] == "success")
                {
                    return true;
                }
                else
                {
                    MessageBox.Show($"Ошибка авторизации: {(parts.Length > 1 ? parts[1] : "Неизвестная ошибка")}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения или авторизации: {ex.Message}");
                return false;
            }
            finally
            {
                client?.Close();
            }
        }
    }
}
