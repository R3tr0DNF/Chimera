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
            Device = HidScanner.FindDualShock();

            if (Device == null)
            {
                return false;
            }
            
            Stream = DualshockConnection.Open(Device);

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