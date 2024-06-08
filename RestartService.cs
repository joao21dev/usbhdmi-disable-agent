using System.Diagnostics;

namespace USBStateProgram
{
    public class RestartService
    {
        public void RestartPC()
        {
            var psi = new ProcessStartInfo("shutdown", "/r /t 0")
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };
            
            Process.Start(psi);
        }
    }
}