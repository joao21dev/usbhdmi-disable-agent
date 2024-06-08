using Microsoft.Win32;
using System;

namespace USBStateProgram
{
    public class USBControlService
    {
        public void DisableUSB()
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
                    Console.WriteLine($"Erro ao desabilitar {keyName}: {ex.Message}");
                }
            }

            Console.WriteLine("USB Desabilitado");
        }

        public void EnableUSB()
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
                    Console.WriteLine($"Erro ao habilitar {keyName}: {ex.Message}");
                }
            }

            Console.WriteLine("USB Habilitado");
        }

        public string GetUSBHUB3Start()
        {
            var keyName = @"SYSTEM\CurrentControlSet\Services\USBHUB3";

            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(keyName, true))
                {
                    if (key != null)
                    {
                        var startValue = key.GetValue("Start");
                        return startValue != null ? startValue.ToString() : "Valor não encontrado";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar o início de {keyName}: {ex.Message}");
            }

            return "Falha ao acessar o sistema";
        }
    }
}