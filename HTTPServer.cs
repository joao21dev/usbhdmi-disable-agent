using System;
using System.Net;

namespace USBStateProgram
{
    public class HTTPServer
    {
        private USBControlService _usbControlService;
        private HttpListener _listener;

        public HTTPServer(USBControlService usbControlService)
        {
            _usbControlService = usbControlService;

            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:8000/");
        }

        public void Start()
        {
            _listener.Start();
            Console.WriteLine("Ouvindo...");

            while (true)
            {
                HttpListenerContext context = _listener.GetContext();
                HttpListenerRequest request = context.Request;
                string responseContent = "<html><body>OK</body></html>";

                if (request.RawUrl == "/disableUSB")
                {
                    _usbControlService.DisableUSB();
                    responseContent = "<html><body>Dispositivos USB desabilitados</body></html>";
                }
                else if (request.RawUrl == "/enableUSB")
                {
                    _usbControlService.EnableUSB();
                    responseContent = "<html><body>Dispositivos USB habilitados</body></html>";
                }
                else if (request.RawUrl == "/getUSBHUB3Start")
                {
                    string startValue = _usbControlService.GetUSBHUB3Start();
                    responseContent = $"<html><body>O valor inicial do USBHUB3 é: {startValue}</body></html>";
                }

                HttpListenerResponse response = context.Response;
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseContent);
                response.ContentLength64 = buffer.Length;

                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
    }
}