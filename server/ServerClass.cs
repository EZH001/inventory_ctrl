using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using server.Db;
using server.Db.Tables;


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
                switch ((string)request.type)
                {
                    case "auth":
                        {
                            string login = request.login;
                            string password = request.password;

                            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                                return JsonConvert.SerializeObject(new { success = false, message = "Не все поля заполнены" });

                            var user = await context.Users.FirstOrDefaultAsync(u => u.Login == login);
                            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password_hash))
                            {
                                var username = await context.User_name.FirstOrDefaultAsync(un => un.User_id == user.Id);

                                if (username != null)
                                {
                                    return JsonConvert.SerializeObject(new { success = true, user_id = user.Id, first_name = username.First_name, last_name = username.Last_name, isAdmin = user.IsAdmin });
                                }
                            }
                            else
                            {
                                return JsonConvert.SerializeObject(new { success = false, message = "Неверный логин или пароль" });
                            }
                        }
                        break;
                    case "get_products":
                        {
                            var products = await context.Products
                    .Where(p => p.QuantityInStock > 0)
                    .Include(p => p.StorageUnit)
                    .Include(p => p.Category)
                    .Include(p => p.Storage)
                    .Include(p => p.Supplier)   
                    .ToListAsync();
                            return JsonConvert.SerializeObject(new { success = true, data = products });
                        }
                    case "get_username_by_id":
                        {
                            int user_id = request.user_id;
                            if (user_id != null)
                            {
                                var username = await context.User_name.FirstOrDefaultAsync(un => un.User_id == user_id);
                                if (username != null)
                                {
                                    return JsonConvert.SerializeObject(new { success = true, first_name = username.First_name, last_name = username.Last_name });
                                }
                            }
                            else
                            {
                                return JsonConvert.SerializeObject(new { success = false, message = "Id пользователя равен null" });
                            }
                        }
                        break;
                    case "get_categories":
                        {
                            var categories = await context.Category.ToListAsync();
                            return JsonConvert.SerializeObject(new { success = true, data = categories });
                        }
                    case "get_storage_units":
                        {
                            var storageUnits = await context.StorageUnits.ToListAsync();
                            return JsonConvert.SerializeObject(new { success = true, data = storageUnits });
                        }
                    case "get_storages":
                        {
                            var storages = await context.Storage
                            .Include(p => p.Category)
                            .ToListAsync();
                            return JsonConvert.SerializeObject(new { success = true, data = storages });
                        }
                    case "get_suppliers":
                        {
                            var suppliers = await context.Suppliers.ToListAsync();
                            return JsonConvert.SerializeObject(new { success = true, data = suppliers });
                        }
                    case "update_product":
                        {
                            dynamic productData = request.product;
                            if (productData != null)
                            {
                                int productId = (int)productData.Id;
                                var product = await context.Products.FirstOrDefaultAsync(x => x.Id == productId);
                                if (product != null)
                                {
                                    product.Id = productData.Id;
                                    product.Title = productData.Title;
                                    product.Description = productData.Description;
                                    product.Category_Id = productData.Category_Id;
                                    product.StorageUnitsId = productData.StorageUnitsId;
                                    product.QuantityInStock = productData.QuantityInStock;
                                    product.CreatedAt = productData.CreatedAt;
                                    product.UpdateAt = DateTime.Now;
                                    product.User_id = productData.User_id;
                                    product.storage_id = productData.storage_id;
                                    product.supplier_id = productData.supplier_id;
                                    context.Entry(product).State = EntityState.Modified;
                                    await context.SaveChangesAsync();
                                    return JsonConvert.SerializeObject(new { success = true });
                                }
                            }
                            return JsonConvert.SerializeObject(new { success = false, message = "Продукт не найден" });
                        }
                    case "delete_product":
                        {
                            int productId = (int)request.productId;
                            var product = await context.Products.FirstOrDefaultAsync(u => u.Id == productId);
                            if (product != null)
                            {
                                context.Products.Remove(product);
                                await context.SaveChangesAsync();
                                return JsonConvert.SerializeObject(new { success = true });
                            }
                            return JsonConvert.SerializeObject(new { success = false, message = "Продукт не найден" });
                        }
                    case "search_products":
                        {
                            string productTitle = (string)request.productTitle;
                            var products = await context.Products
                                                    .Include(p => p.StorageUnit)
                                                    .Include(p => p.Category)
                                                    .Include(p => p.Storage)
                                                    .Include(p => p.Supplier)
                                                    .Where(p => p.Title == productTitle)
                                                    .ToListAsync();
                            return JsonConvert.SerializeObject(new { success = true, data = products });
                        }
                    case "update_storage":
                        {
                            dynamic storageData = request.storage;
                            if (storageData != null)
                            {
                                var storageId = (int)storageData.Id;
                                var storage = await context.Storage.FirstOrDefaultAsync(s => s.Id == storageId);
                                if (storage != null)
                                {
                                    storage.Id = storageData.Id;
                                    storage.Title = storageData.Title;
                                    storage.category_id = storageData.category_id;
                                    context.Entry(storage).State = EntityState.Modified;
                                    await context.SaveChangesAsync();
                                    return JsonConvert.SerializeObject(new { success = true });
                                }
                            }
                            return JsonConvert.SerializeObject(new { success = false, message = "Место хранения не найдено" });
                        }
                    case "delete_storage":
                        {
                            int storageId = (int)request.storageId;
                            var storage = await context.Storage.FirstOrDefaultAsync(u => u.Id == storageId);
                            if (storage != null)
                            {
                                context.Storage.Remove(storage);
                                await context.SaveChangesAsync();
                                return JsonConvert.SerializeObject(new { success = true });
                            }
                            return JsonConvert.SerializeObject(new { success = false, message = "Место хранения не найден" });
                        }
                    case "add_storage":
                        {
                            string storageTitle = (string)request.storageTitle;
                            int categoryId = (int)request.categoryId;
                            if (!String.IsNullOrEmpty(storageTitle) && categoryId != null)
                            {
                                if (!context.Storage.Any(u => u.Title == storageTitle && u.category_id == categoryId))
                                {
                                    Storage storage = new Storage();
                                    storage.Title = storageTitle;
                                    storage.category_id = categoryId;
                                    context.Storage.Add(storage);
                                    await context.SaveChangesAsync();
                                    return JsonConvert.SerializeObject(new { success = true });
                                }
                                else return JsonConvert.SerializeObject(new { success = false, message = "Такое место хранение уже существует" });
                            }
                            return JsonConvert.SerializeObject(new { success = false, message = "Не удалось добавить место хранения" });
                        }
                    case "add_product":
                        {   
                            int categoryId = (int)request.product.Category_Id;
                            int storageUnitsId = (int)request.product.StorageUnitsId;
                            int supplierId = (int)request.product.supplier_id;
                            int storageId = (int)request.product.storage_id;
                            int User_id = (int)request.product.User_id;
                            string Title = (string)request.product.Title;
                            string Description = (string)request.product.Description;
                            decimal QuantityInStock = (decimal)request.product.QuantityInStock;
                            if (storageUnitsId!= null && categoryId != null && supplierId != null && storageId != null)
                            {
                                    Product product = new Product();
                                    product.Title = Title;
                                    product.Description = Description;
                                    product.Category_Id = categoryId;
                                    product.QuantityInStock = QuantityInStock;
                                    product.StorageUnitsId = storageUnitsId;    
                                    product.CreatedAt = DateTime.Now;
                                    product.UpdateAt = DateTime.Now;
                                    product.User_id = User_id;
                                    product.storage_id = storageId;
                                    product.supplier_id = supplierId;   
                                    context.Products.Add(product);
                                    await context.SaveChangesAsync();
                                    return JsonConvert.SerializeObject(new { success = true });
                                }
                                else return JsonConvert.SerializeObject(new { success = false, message = "Такой товар уже существует" });
                        }
                    case "add_supplier":
                        {                          
                            string Title = (string)request.supplier.Title;
                            string contact_person = (string)request.supplier.contact_person;
                            string address = (string)request.supplier.address;
                            string phone_number = (string)request.supplier.phone_number;
                            string email = (string)request.supplier.email;
                            if (!context.Suppliers.Any(u => u.Title == Title &&  u.contact_person == contact_person && u.address == address && u.phone_number == phone_number && u.email == email))
                            {
                                Supplier supplier = new Supplier();
                                supplier.Title = Title;
                                supplier.contact_person = contact_person;
                                supplier.address = address; 
                                supplier.phone_number = phone_number;
                                supplier.email = email;
                                context.Suppliers.Add(supplier);
                                await context.SaveChangesAsync();
                                return JsonConvert.SerializeObject(new { success = true });
                            }
                            else return JsonConvert.SerializeObject(new { success = false, message = "Такой поставщик уже существует" });
                        }
                    case "update_supplier":
                        {
                            dynamic supplierData = request.supplier;
                            if (supplierData != null)
                            {
                                int supplierId = (int)supplierData.Id;
                                var supplier = await context.Suppliers.FirstOrDefaultAsync(x => x.Id == supplierId);
                                if (supplier != null)
                                {
                                    supplier.Id = supplierData.Id;
                                    supplier.Title = supplierData.Title;
                                    supplier.contact_person = supplier.contact_person;
                                    supplier.address = supplierData.address;
                                    supplier.phone_number = supplierData.phone_number;
                                    supplier.email = supplierData.email;
                                    context.Entry(supplier).State = EntityState.Modified;
                                    await context.SaveChangesAsync();
                                    return JsonConvert.SerializeObject(new { success = true });
                                }
                            }
                            return JsonConvert.SerializeObject(new { success = false, message = "Поставщик не найден" });
                        }
                    case "delete_suppliers":
                        {
                            int supplierId = (int)request.supplierId;
                            var supplier = await context.Suppliers.FirstOrDefaultAsync(u => u.Id == supplierId);
                            if (supplier != null)
                            {
                                context.Suppliers.Remove(supplier);
                                await context.SaveChangesAsync();
                                return JsonConvert.SerializeObject(new { success = true });
                            }
                            return JsonConvert.SerializeObject(new { success = false, message = "Поставщик не найден" });
                        }
                    case "get_staffs":
                        {
                            var staffs = await context.Staff.ToListAsync();
                            return JsonConvert.SerializeObject(new { success = true, data = staffs });
                        }
                    case "delete_staff":
                        {
                            int staffId = (int)request.staffId;
                            var staff = await context.Staff.FirstOrDefaultAsync(u => u.Id == staffId);
                            if (staff != null)
                            {
                                context.Staff.Remove(staff);
                                await context.SaveChangesAsync();
                                return JsonConvert.SerializeObject(new { success = true });
                            }
                            return JsonConvert.SerializeObject(new { success = false, message = "Сотрудник не найден" });
                        }
                    case "add_staff":
                        {
                            string last_name = (string)request.staff.last_name;
                            string first_name = (string)request.staff.first_name;
                            string post = (string)request.staff.post;
                            if (!context.Staff.Any(u => u.last_name == last_name && u.first_name == first_name && u.post == post))
                            {
                                Staff staff = new Staff();
                                staff.last_name = last_name;
                                staff.first_name = first_name;
                                staff.post = post;
                                context.Staff.Add(staff);
                                await context.SaveChangesAsync();
                                return JsonConvert.SerializeObject(new { success = true });
                            }
                            else return JsonConvert.SerializeObject(new { success = false, message = "Такой сотрудник уже существует" });
                        }
                    case "update_staff":
                        {
                            dynamic staffData = request.staff;
                            if (staffData != null)
                            {
                                int staffId = (int)staffData.Id;
                                var staff = await context.Staff.FirstOrDefaultAsync(x => x.Id == staffId);
                                if (staff != null)
                                {
                                    staff.Id = staffData.Id;
                                    staff.last_name = staffData.last_name;
                                    staff.first_name = staffData.first_name;
                                    staff.post = staffData.post;
                                    context.Entry(staff).State = EntityState.Modified;
                                    await context.SaveChangesAsync();
                                    return JsonConvert.SerializeObject(new { success = true });
                                }
                            }
                            return JsonConvert.SerializeObject(new { success = false, message = "Сотрудник не найден" });
                        }
                    case "get_extraditions":
                        {
                            var extraditions = await context.Extradition
                            .Include(p => p.Product)
                            .Include(p => p.Staff)
                            .ToListAsync();
                            return JsonConvert.SerializeObject(new { success = true, data = extraditions });
                        }
                    case "extradition_product":
                        {
                            int productId = request.selectedProductId;
                            int staffId = request.selectedStaffId;
                            decimal extraditionQuantity = request.productQuantity;
                                var product = await context.Products.FirstOrDefaultAsync(x => x.Id == productId);
                                if (product != null)
                                {
                                   if (extraditionQuantity > product.QuantityInStock) return JsonConvert.SerializeObject(new { success = false, message = "Невозможно выдать больше, чем есть на складе" });
                                else
                                {
                                    decimal quantityRes = product.QuantityInStock - extraditionQuantity;
                                    if (quantityRes == 0)
                                    {
                                        Extradition extradition = new Extradition();
                                        extradition.product_id = productId;
                                        extradition.staff_id = staffId;
                                        extradition.quantity = extraditionQuantity;
                                        extradition.shipment_date = DateTime.Now;
                                        context.Extradition.Add(extradition);
                                        product.QuantityInStock = 0;
                                        product.UpdateAt = DateTime.Now;
                                        //context.Products.Remove(product);
                                        context.Entry(product).State = EntityState.Modified;
                                        await context.SaveChangesAsync();
                                        return JsonConvert.SerializeObject(new { success = true });
                                    }
                                    else
                                    {
                                        Extradition extradition = new Extradition();
                                        extradition.product_id = productId;
                                        extradition.staff_id = staffId;
                                        extradition.quantity = extraditionQuantity;
                                        extradition.shipment_date = DateTime.Now;
                                        context.Extradition.Add(extradition);
                                        product.QuantityInStock = quantityRes;
                                        product.UpdateAt = DateTime.Now;
                                        context.Entry(product).State = EntityState.Modified;
                                        await context.SaveChangesAsync();
                                        return JsonConvert.SerializeObject(new { success = true });
                                    }
                                }
                                }
                            else return JsonConvert.SerializeObject(new { success = false, message = "Продукт не найден" });
                        }
                    default:
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
            return JsonConvert.SerializeObject(new { success = false, message = "Ошибка" });
        }

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

