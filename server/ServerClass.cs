using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using server.Db;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace server
{
    public class WarehouseServer : IDisposable
    {
        private readonly int port;
        private readonly IPAddress ipAddress;
        private readonly ApplicationDbContext context;
        private readonly TcpListener server;
        private bool isRunning = false;
        private string logFilePath;

        public WarehouseServer(int port, IPAddress ipAddress)
        {
            this.port = port;
            this.ipAddress = ipAddress;
            this.context = new ApplicationDbContext(); // Создаём контекст без параметров
            this.server = new TcpListener(ipAddress, port);
            this.logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "server.log");
        }

        public void Start()
        {
            try
            {
                server.Start();
                isRunning = true;
                Console.WriteLine($"Сервер запущен на {ipAddress}:{port}");
                Log($"Сервер запущен на {ipAddress}:{port}");
                while (isRunning)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Новое подключение.");
                    Log("Новое подключение.");
                    Task.Run(() => HandleClient(client));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка запуска сервера: {ex.Message}");
                Log($"Ошибка запуска сервера: {ex.Message}");
            }
        }

        public void Stop()
        {
            isRunning = false;
            server.Stop();
            context.Dispose();
        }

        private async Task HandleClient(TcpClient client)
        {
            using (client)
            using (NetworkStream stream = client.GetStream())
            {
                try
                {
                    while (isRunning)
                    {
                        // Используем буфер для чтения данных
                        byte[] buffer = new byte[1024];
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        if (bytesRead == 0) break; // Клиент отключился

                        string requestJson = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim('\0');

                        string responseJson = await ProcessRequest(requestJson);

                        byte[] responseBytes = Encoding.UTF8.GetBytes(responseJson);
                        await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка обработки клиента: {ex.Message}");
                    Log($"Ошибка обработки клиента: {ex.Message}");
                }
            }
        }


        private async Task<string> ProcessRequest(string requestJson)
        {
            try
            {
                // Парсинг JSON
                dynamic request = JsonConvert.DeserializeObject(requestJson);

                if (request.type == "auth")
                {
                    string username = request.username;
                    string password = request.password;

                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                        return JsonConvert.SerializeObject(new { success = false, message = "Не все поля заполнены" });

                    var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
                    if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                    {
                        return JsonConvert.SerializeObject(new { success = true}); //Генерируем токен
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { success = false, message = "Неверный логин или пароль" });
                    }
                }
                else
                {
                    return JsonConvert.SerializeObject(new { success = false, message = "Неверный тип запроса" });
                }
            }
            catch (JsonReaderException ex)
            {
                Log($"Ошибка парсинга JSON: {ex.Message}");
                return JsonConvert.SerializeObject(new { success = false, message = "Ошибка обработки запроса" });
            }
            catch (Exception ex)
            {
                Log($"Ошибка обработки запроса: {ex.Message}");
                return JsonConvert.SerializeObject(new { success = false, message = "Ошибка обработки запроса" });
            }
        }

        // Хэширование пароля с солью
        


        // Проверка пароля


        private void Log(string message)
        {
            string logMessage = $"{DateTime.Now} - {message}\n";
            File.AppendAllText(logFilePath, logMessage);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}

