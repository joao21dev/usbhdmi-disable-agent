using System.Net;

namespace USBStateProgram
{
    public class HTTPServer
    {
        private readonly USBManager _usbManager;

        public HTTPServer(USBManager usbManager)
        {
            _usbManager = usbManager;
        }

        public void Run()
        {
            // Código do HttpListener aqui
            // Parte do código presente no método Main que lida com HttpListener
            // A chamadas dos métodos EnableUSB, DisableUSB e GetUSBHUB3Start
            // devem ser substituídas por chamadas aos métodos do _usbManager
        }
    }
}