using System;
using System.Net;
using Microsoft.Win32;

namespace USBStateProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            using var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8000/");
            listener.Start();
            Console.WriteLine("Ouvindo...");

            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                string responseContent = "<html><body>OK</body></html>";

                if (request.RawUrl == "/disableUSB")
                {
                    DisableUSB();
                    responseContent = "<html><body>USB devices have been disabled</body></html>";
                }
                else if (request.RawUrl == "/enableUSB")
                {
                    EnableUSB();
                    responseContent = "<html><body>USB devices have been enabled</body></html>";
                }
                else if (request.RawUrl == "/getUSBHUB3Start")
                {
                    string startValue = GetUSBHUB3Start();
                    responseContent = $"<html><body>USBHUB3 Start value is: {startValue}</body></html>";
                }

                HttpListenerResponse response = context.Response;
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseContent);
                response.ContentLength64 = buffer.Length;

                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }

        static void DisableUSB()
        {
            var keynames = new[] { @"SYSTEM\CurrentControlSet\Services\USBSTOR", 
                @"SYSTEM\CurrentControlSet\Services\USBHUB3", 
                @"SYSTEM\CurrentControlSet\Services\usbhub" };

            foreach (var keyName in keynames)
            {
                try
                {
                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyName, true))
                    {
                        if (key != null)
                        {
                            key.SetValue("Start", 4, RegistryValueKind.DWord);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to disable {keyName}: {ex.Message}");
                }
            }
            Console.WriteLine("USB Disabled");
        }

        static void EnableUSB()
        {
            var keynames = new[] { @"SYSTEM\CurrentControlSet\Services\USBSTOR", 
                @"SYSTEM\CurrentControlSet\Services\USBHUB3", 
                @"SYSTEM\CurrentControlSet\Services\usbhub" };

            foreach (var keyName in keynames)
            {
                try
                {
                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyName, true))
                    {
                        if (key != null)
                        {
                            key.SetValue("Start", 3, RegistryValueKind.DWord);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to enable {keyName}: {ex.Message}");
                }
            }
            Console.WriteLine("USB Enabled");
        }

        static string GetUSBHUB3Start()
        {
            var keyName = @"SYSTEM\CurrentControlSet\Services\USBHUB3";
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyName, true))
                {
                    if (key != null)
                    {
                        var startValue = key.GetValue("Start");
                        return startValue != null ? startValue.ToString() : "Value not found";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get Start of {keyName}: {ex.Message}");
            }
            return "Failed to access registry key";
        }
    }
}