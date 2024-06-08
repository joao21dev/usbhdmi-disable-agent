using System;
using System.Net;

namespace USBStateProgram
{
    public class HTTPServer
    {
        private USBControlService _usbControlService;
        private HttpListener _listener;
        private RestartService _restartService;
        private MonitorService _monitorService;

        public HTTPServer(USBControlService usbControlService, MonitorService monitorService)
        {
            _usbControlService = usbControlService;
            _restartService = new RestartService();
            _monitorService = monitorService;

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
                string responseContent = "OK";
                
                if (request.RawUrl == "/disableUSB" && request.HttpMethod == "POST")
                {
                    _usbControlService.DisableUSB();
                    responseContent = "Dispositivos USB desabilitados";
                }
                else if (request.RawUrl == "/enableUSB" && request.HttpMethod == "POST")
                {
                    _usbControlService.EnableUSB();
                    responseContent = "Dispositivos USB habilitados";
                }
                else if (request.RawUrl == "/isUsbDisable")
                {
                    string startValue = _usbControlService.GetUSBHUB3Start();
                    responseContent = (startValue == "4").ToString();
                }
                else if (request.RawUrl == "/restartPC")
                {
                    _restartService.RestartPC();
                    responseContent = "PC será reiniciado.";
                }
                else if (request.RawUrl == "/isSecondMonitorConnected")
                {
                    bool isSecondMonitorConnected = _monitorService.IsSecondMonitorConnected();
                    responseContent = isSecondMonitorConnected.ToString();
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