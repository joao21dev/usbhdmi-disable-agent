﻿using System;

namespace USBStateProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando o serviço....");

            // Inicializar objetos
            var usbControlService = new USBControlService();
            var httpServer = new HTTPServer(usbControlService);

            // Iniciar o servidor HTTP
            httpServer.Start();

            // Manter o programa rodando.
            while (true) {}
        }
    }
}