using System;

namespace USBStateProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando o serviço....");

            // Inicializar objetos
            var usbControlService = new USBControlService();
            var monitorService = new MonitorService();   // Adicionar esta linha
            var httpServer = new HTTPServer(usbControlService, monitorService); // Modificar esta linha

            // Iniciar o servidor HTTP
            httpServer.Start();

            // Manter o programa rodando.
            while (true) {}
        }
    }
}