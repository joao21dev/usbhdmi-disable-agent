using Microsoft.Win32;
using System;

namespace USBStateProgram
{
    public class USBControlService
    {
        private const string USBHUB3KeyName = @"SYSTEM\CurrentControlSet\Services\USBHUB3";

        public void DisableUSB()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(USBHUB3KeyName, true))
                {
                    if (key != null)
                    {
                        key.SetValue("Start", 4, RegistryValueKind.DWord);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao desabilitar USBHUB3: {ex.Message}");
            }

            Console.WriteLine("USBHUB3 desabilitado");
        }

        public void EnableUSB()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(USBHUB3KeyName, true))
                {
                    if (key != null)
                    {
                        key.SetValue("Start", 3, RegistryValueKind.DWord);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao habilitar USBHUB3: {ex.Message}");
            }

            Console.WriteLine("USBHUB3 habilitado");
        }

        public string GetUSBHUB3Start()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(USBHUB3KeyName, true))
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
                Console.WriteLine($"Erro ao buscar o valor inicial de USBHUB3: {ex.Message}");
            }

            return "Falha ao acessar o sistema";
        }
    }
}