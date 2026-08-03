using HidSharp;
using Chimera.Models;

namespace Chimera.Services
{
    internal static class DualshockConnection
    {
        public static HidStream? Open(DualShockDevice controller)
        {
            if (controller.Device.TryOpen(out HidStream? stream))
            {
                byte[] buffer = new byte[64];
                stream.ReadTimeout = 100;

                try
                {
                    stream.Read(buffer);
                    Console.WriteLine("Initial input report received");
                }
                catch (TimeoutException){
                    Console.WriteLine("No initial input report");
                }

                Thread.Sleep(1000);

                return stream;
            }
            return null;
        }

    }
}