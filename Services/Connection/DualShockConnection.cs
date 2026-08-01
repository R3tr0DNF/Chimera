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
                return stream;
            }
            return null;
        }

    }
}