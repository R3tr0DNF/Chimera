using Chimera.Models;
using Chimera.Services.Input;
using HidSharp;

namespace Chimera.Services.Conection
{
    internal class DualShockManager
    {
        public DualShockDevice? Device { get; private set; }
        public HidStream? Stream { get; private set; }
        public InputMonitor? Monitor { get; private set; }

        public bool Initialize()
        {
            Console.WriteLine("Searching...");

            Device = HidScanner.FindDualShock();

            Console.WriteLine(Device == null? "Device NOT found" : "Device found"); 

            if (Device == null)
            {
                return false;
            }

            Console.WriteLine("Openning stream...");
            
            Stream = DualshockConnection.Open(Device);

            Console.WriteLine(Stream == null? "Stream NOT opened" : "Stream opened"); 

            if (Stream == null)
            {
                Device = null;
                return false;
            }

            Monitor = new InputMonitor(Stream);
            
            return true;
        }

        public void Disconnect()
        {
            Stream?.Close();

            Stream = null;
            Monitor = null;
            Device = null;
        }
    }
}