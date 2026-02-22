using System;
using System.ServiceModel;
using Chat_Library;

namespace Chat_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Запускаємо хост
            var uris = new Uri[1];
            string address = "net.tcp://localhost:8302/";
            uris[0] = new Uri(address);

            IServiceChat service = new ServiceChat();
            ServiceHost host = new ServiceHost(service, uris);

            // Використовуємо NetTcpBinding (швидкий, для локальної мережі)
            var binding = new NetTcpBinding(SecurityMode.None);
            host.AddServiceEndpoint(typeof(IServiceChat), binding, "");

            host.Open();
            Console.WriteLine("Сервер запущено!");
            Console.WriteLine($"Адреса: {address}");
            Console.WriteLine("Натисніть Enter щоб зупинити...");
            Console.ReadLine();
            host.Close();
        }
    }
}