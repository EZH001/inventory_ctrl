using client.Models;
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
        public async Task<string> Authenticate(string login, string password)
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Соединение с сервером не установлено");
                return JsonConvert.SerializeObject(new { success = false, message = "Соединение с сервером не установлено" });
            }
            try
            {
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                string requestJson = JsonConvert.SerializeObject(new { type = "auth", login = login, password = password });
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
                        string userJson = JsonConvert.SerializeObject(new { success = true, user_id = response.user_id, first_name = response.first_name, last_name = response.last_name, isAdmin = response.isAdmin });
                        return userJson;
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка авторизации: {(string)response.message}");
                        string userJson = JsonConvert.SerializeObject(new { success = response.success, message = response.message });
                        return userJson;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка аутентификации: {ex.Message}");
                return JsonConvert.SerializeObject(new { success = false, message = $"Ошибка аутентификации: {ex.Message}" });
            }
            return JsonConvert.SerializeObject(new { success = false, message = "Ошибка" });
        }

        public async Task<List<Product>> GetProducts(){
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Соединение с сервером не установлено");
                return null;
            }
            try
            {
                NetworkStream stream = client.GetStream();

                var productsRequest = new { type = "get_products" };
                string requestJson = JsonConvert.SerializeObject(productsRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);

                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

                List<byte> responseBytes = new List<byte>();
                byte[] buffer = new byte[8192];
                int bytesRead;
                StringBuilder responseBuilder = new StringBuilder();

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    responseBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

                    // Проверяем, содержит ли строка допустимый JSON
                    try
                    {
                        dynamic response = JsonConvert.DeserializeObject(responseBuilder.ToString());
                        if ((bool)response.success)
                        {
                            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(JsonConvert.SerializeObject(response.data));
                            return products;
                        }
                    }
                    catch (JsonReaderException)
                    {
                        // Continue reading
                    }
                }

                MessageBox.Show("Не удалось прочитать данные от сервера");
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения продуктов: {ex.Message}");
                return null;
            }
        }

       public async Task<string> GetUsername(int user_id)
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Соединение с сервером не установлено");
                return null;
            }
            try
            {

                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                var productsRequest = new { type = "get_username_by_id", user_id = user_id };
                string requestJson = JsonConvert.SerializeObject(productsRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                byte[] responseBytes = new byte[1024];
                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
                if (bytesRead > 0)
                {
                    string responseJson = Encoding.UTF8.GetString(responseBytes, 0, bytesRead).Trim('\0');
                    dynamic response = JsonConvert.DeserializeObject(responseJson);
                    if ((bool)response.success)
                    {
                        string username = $"{response.last_name} {response.first_name}";
                        return username;
                    }
                    else
                    {
                        MessageBox.Show((string)response.message);
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения имени пользователя: {ex.Message}");
                return null;
            }
        }
        public void Disconnect()
        {
            client?.Close();
        }

        public async Task<List<Category>> GetCategories()
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Соединение с сервером не установлено");
                return null;
            }
            try
            {
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                var categoriesRequest = new { type = "get_categories" };
                string requestJson = JsonConvert.SerializeObject(categoriesRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                byte[] responseBytes = new byte[1024];
                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
                if (bytesRead > 0)
                {
                    string responseJson = Encoding.UTF8.GetString(responseBytes, 0, bytesRead).Trim('\0');
                    dynamic response = JsonConvert.DeserializeObject(responseJson);
                    if ((bool)response.success)
                    {
                        List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(JsonConvert.SerializeObject(response.data));
                        return categories;
                    }
                    else
                    {
                        MessageBox.Show((string)response.message);
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения категорий: {ex.Message}");
                return null;
            }
        }

        public async Task<List<StorageUnit>> GetStorageUnits()
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Соединение с сервером не установлено");
                return null;
            }
            try
            {
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                var storageUnitsRequest = new { type = "get_storage_units" };
                string requestJson = JsonConvert.SerializeObject(storageUnitsRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                byte[] responseBytes = new byte[1024];
                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
                if (bytesRead > 0)
                {
                    string responseJson = Encoding.UTF8.GetString(responseBytes, 0, bytesRead).Trim('\0');
                    dynamic response = JsonConvert.DeserializeObject(responseJson);
                    if ((bool)response.success)
                    {
                        List<StorageUnit> storageUnits = JsonConvert.DeserializeObject<List<StorageUnit>>(JsonConvert.SerializeObject(response.data));
                        return storageUnits;
                    }
                    else
                    {
                        MessageBox.Show((string)response.message);
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения единиц хранения: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Storage>> GetStorages()
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Соединение с сервером не установлено");
                return null;
            }
            try
            {
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                var storagesRequest = new { type = "get_storages" };
                string requestJson = JsonConvert.SerializeObject(storagesRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                byte[] responseBytes = new byte[1024];
                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
                if (bytesRead > 0)
                {
                    string responseJson = Encoding.UTF8.GetString(responseBytes, 0, bytesRead).Trim('\0');
                    dynamic response = JsonConvert.DeserializeObject(responseJson);
                    if ((bool)response.success)
                    {
                        List<Storage> storages = JsonConvert.DeserializeObject<List<Storage>>(JsonConvert.SerializeObject(response.data));
                        return storages;
                    }
                    else
                    {
                        MessageBox.Show((string)response.message);
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения мест хранения: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> UpdateProduct(Product product)
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

                var updateRequest = new { type = "update_product", product = product };
                string requestJson = JsonConvert.SerializeObject(updateRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                byte[] responseBytes = new byte[1024];
                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
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
                        MessageBox.Show((string)response.message);
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка изменения товара: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Соединение с сервером не установлено");
                return false;
            }
            try
            {
                NetworkStream stream = client.GetStream();

                var deleteRequest = new { type = "delete_product", productId = productId };
                string requestJson = JsonConvert.SerializeObject(deleteRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                byte[] responseBytes = new byte[1024];
                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
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
                        MessageBox.Show((string)response.message);
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления товара: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Product>> SearchProduct(string productTitle)
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Соединение с сервером не установлено");
                return null;
            }
            try
            {
                NetworkStream stream = client.GetStream();

                var productsRequest = new { type = "search_products", productTitle = productTitle };
                string requestJson = JsonConvert.SerializeObject(productsRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);

                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

                List<byte> responseBytes = new List<byte>();
                byte[] buffer = new byte[8192];
                int bytesRead;
                StringBuilder responseBuilder = new StringBuilder();

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    responseBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

                    // Проверяем, содержит ли строка допустимый JSON
                    try
                    {
                        dynamic response = JsonConvert.DeserializeObject(responseBuilder.ToString());
                        if ((bool)response.success)
                        {
                            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(JsonConvert.SerializeObject(response.data));
                            return products;
                        }
                    }
                    catch (JsonReaderException)
                    {
                        // Continue reading
                    }
                }

                MessageBox.Show("Не удалось прочитать данные от сервера");
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения результатов поиска: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateStorage(Storage storage)
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

                var updateRequest = new { type = "update_storage", storage = storage };
                string requestJson = JsonConvert.SerializeObject(updateRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                byte[] responseBytes = new byte[8024];
                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
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
                        MessageBox.Show((string)response.message);
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка изменения товара: {ex.Message}");
                return false;
            }
        }

        internal async Task<bool> DeleteStorage(int storageId)
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Соединение с сервером не установлено");
                return false;
            }
            try
            {
                NetworkStream stream = client.GetStream();

                var deleteRequest = new { type = "delete_storage", storageId = storageId };
                string requestJson = JsonConvert.SerializeObject(deleteRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                byte[] responseBytes = new byte[1024];
                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
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
                        MessageBox.Show((string)response.message);
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления места хранения: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AddStorage(string storageTitle, int categoryId)
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Соединение с сервером не установлено");
                return false;
            }
            try
            {
                NetworkStream stream = client.GetStream();

                var addRequest = new { type = "add_storage", storageTitle = storageTitle, categoryId = categoryId };
                string requestJson = JsonConvert.SerializeObject(addRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                byte[] responseBytes = new byte[1024];
                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
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
                        MessageBox.Show((string)response.message);
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления места хранения: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Supplier>> GetSuppliers()
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Соединение с сервером не установлено");
                return null;
            }
            try
            {
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                var storagesRequest = new { type = "get_suppliers" };
                string requestJson = JsonConvert.SerializeObject(storagesRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                byte[] responseBytes = new byte[1024];
                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
                if (bytesRead > 0)
                {
                    string responseJson = Encoding.UTF8.GetString(responseBytes, 0, bytesRead).Trim('\0');
                    dynamic response = JsonConvert.DeserializeObject(responseJson);
                    if ((bool)response.success)
                    {
                        List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(JsonConvert.SerializeObject(response.data));
                        return suppliers;
                    }
                    else
                    {
                        MessageBox.Show((string)response.message);
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения поставщиков: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AddProduct(Product product)
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Соединение с сервером не установлено");
                return false;
            }
            try
            {
                NetworkStream stream = client.GetStream();

                var addRequest = new { type = "add_product", product = product };
                string requestJson = JsonConvert.SerializeObject(addRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                byte[] responseBytes = new byte[1024];
                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
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
                        MessageBox.Show((string)response.message);
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления места хранения: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AddSupplier(Supplier supplier)
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Соединение с сервером не установлено");
                return false;
            }
            try
            {
                NetworkStream stream = client.GetStream();

                var addRequest = new { type = "add_supplier", supplier = supplier };
                string requestJson = JsonConvert.SerializeObject(addRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                byte[] responseBytes = new byte[1024];
                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
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
                        MessageBox.Show((string)response.message);
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления поставщика: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateSupplier(Supplier supplier)
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

                var updateRequest = new { type = "update_supplier", supplier = supplier };
                string requestJson = JsonConvert.SerializeObject(updateRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                byte[] responseBytes = new byte[8024];
                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
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
                        MessageBox.Show((string)response.message);
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка изменения поставщика: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteSupplier(int id)
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Соединение с сервером не установлено");
                return false;
            }
            try
            {
                NetworkStream stream = client.GetStream();

                var deleteRequest = new { type = "delete_suppliers", supplierId = id };
                string requestJson = JsonConvert.SerializeObject(deleteRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                byte[] responseBytes = new byte[1024];
                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
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
                        MessageBox.Show((string)response.message);
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления поставщика: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Staff>> GetStaffs()
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Соединение с сервером не установлено");
                return null;
            }
            try
            {
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                var storagesRequest = new { type = "get_staffs" };
                string requestJson = JsonConvert.SerializeObject(storagesRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                byte[] responseBytes = new byte[1024];
                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
                if (bytesRead > 0)
                {
                    string responseJson = Encoding.UTF8.GetString(responseBytes, 0, bytesRead).Trim('\0');
                    dynamic response = JsonConvert.DeserializeObject(responseJson);
                    if ((bool)response.success)
                    {
                        List<Staff> staffs = JsonConvert.DeserializeObject<List<Staff>>(JsonConvert.SerializeObject(response.data));
                        return staffs;
                    }
                    else
                    {
                        MessageBox.Show((string)response.message);
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения сотрудников: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteStaff(int id)
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Соединение с сервером не установлено");
                return false;
            }
            try
            {
                NetworkStream stream = client.GetStream();

                var deleteRequest = new { type = "delete_staff", staffId = id };
                string requestJson = JsonConvert.SerializeObject(deleteRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                byte[] responseBytes = new byte[1024];
                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
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
                        MessageBox.Show((string)response.message);
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления сотрудника: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AddStaff(Staff staff)
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Соединение с сервером не установлено");
                return false;
            }
            try
            {
                NetworkStream stream = client.GetStream();

                var addRequest = new { type = "add_staff", staff = staff };
                string requestJson = JsonConvert.SerializeObject(addRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                byte[] responseBytes = new byte[1024];
                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
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
                        MessageBox.Show((string)response.message);
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления сотрудника: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateStaff(Staff staff)
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

                var updateRequest = new { type = "update_staff", staff = staff };
                string requestJson = JsonConvert.SerializeObject(updateRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                byte[] responseBytes = new byte[8024];
                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
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
                        MessageBox.Show((string)response.message);
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка изменения сотрудника: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Extradition>> GetExtraditions()
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Соединение с сервером не установлено");
                return null;
            }
            try
            {
                NetworkStream stream = client.GetStream();

                var productsRequest = new { type = "get_extraditions" };
                string requestJson = JsonConvert.SerializeObject(productsRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);

                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

                List<byte> responseBytes = new List<byte>();
                byte[] buffer = new byte[8192];
                int bytesRead;
                StringBuilder responseBuilder = new StringBuilder();

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    responseBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

                    // Проверяем, содержит ли строка допустимый JSON
                    try
                    {
                        dynamic response = JsonConvert.DeserializeObject(responseBuilder.ToString());
                        if ((bool)response.success)
                        {
                            List<Extradition> extraditions = JsonConvert.DeserializeObject<List<Extradition>>(JsonConvert.SerializeObject(response.data));
                            return extraditions;
                        }
                    }
                    catch (JsonReaderException)
                    {
                        // Continue reading
                    }
                }

                MessageBox.Show("Не удалось прочитать данные от сервера");
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения продуктов: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> ExtraditionProduct(int selectedProductId, int selectedStaffId, decimal? productQuantity)
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

                var updateRequest = new { type = "extradition_product", selectedProductId = selectedProductId, selectedStaffId = selectedStaffId, productQuantity = productQuantity };
                string requestJson = JsonConvert.SerializeObject(updateRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                byte[] responseBytes = new byte[8024];
                // Отправка данных
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
                int bytesRead = await stream.ReadAsync(responseBytes, 0, responseBytes.Length);
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
                        MessageBox.Show((string)response.message);
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка изменения сотрудника: {ex.Message}");
                return false;
            }
        }
    }
}

