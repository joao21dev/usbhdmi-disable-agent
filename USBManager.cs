using System;
using Microsoft.Win32;

namespace USBStateProgram
{
    public class USBManager
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
                    Console.WriteLine($"Failed to disable {keyName}: {ex.Message}");
                }
            }
            Console.WriteLine("USB Disabled");
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
                    Console.WriteLine($"Failed to enable {keyName}: {ex.Message}");
                }
            }
            Console.WriteLine("USB Enabled");
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