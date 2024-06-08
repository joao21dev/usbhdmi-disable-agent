using System.Management;
using System.Collections.Generic;

namespace USBStateProgram
{
    public class MonitorService
    {
        public bool IsSecondMonitorConnected()
        {
            List<string> monitors = new List<string>();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE Description LIKE 'Monitor Genérico PnP%'");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                monitors.Add(queryObj["Description"] + " (" + queryObj["DeviceID"] + ")");
            }

            return monitors.Count > 1;
        }
    }
}