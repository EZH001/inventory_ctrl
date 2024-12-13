using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string serverName = GetServerNameFromConnectionString(connectionString);

            IPAddress[] addresses = Dns.GetHostAddresses(serverName);
            IPAddress ipAddressToUse = null;

            if (addresses != null && addresses.Length > 0)
            {
                ipAddressToUse = addresses[0]; // Используем первый IP-адрес
                Console.WriteLine($"Using IP address: {ipAddressToUse}");
            }
            else
            {
                Console.WriteLine($"Could not resolve IP address for {serverName}");
                return; // Выходим, если не удалось определить IP-адрес
            }


            var server = new WarehouseServer(53551, ipAddressToUse);
            await Task.Run(() => server.Start());
            Console.ReadKey();
        }

        private static string GetServerNameFromConnectionString(string connectionString)
        {
            string serverName = "";
            string[] parts = connectionString.Split(';');
            foreach (string part in parts)
            {
                if (part.StartsWith("Server=", StringComparison.OrdinalIgnoreCase) ||
                    part.StartsWith("Data Source=", StringComparison.OrdinalIgnoreCase))
                {
                    serverName = part.Substring(part.IndexOf('=') + 1);
                    break;
                }
            }
            return serverName;
        }
    }
}
