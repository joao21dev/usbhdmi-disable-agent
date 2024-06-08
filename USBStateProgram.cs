namespace USBStateProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            USBManager usbManager = new USBManager();
            
            HTTPServer httpServer = new HTTPServer(usbManager);
            httpServer.Run();
        }
    }
}