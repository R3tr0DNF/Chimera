using HidSharp;

namespace Chimera.Models
{
    internal class DualShockDevice
    {
        public HidDevice Device {get; set;}
        public string DevicePath {get; set;}
        public string Manufacturer {get; set;}
        public string SerialNumber {get; set;}
        public string ProductName {get; set;}
    }
}