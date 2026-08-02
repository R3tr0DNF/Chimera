using System;
using HidSharp;
using Chimera.Models;

namespace Chimera.Services
{
    // Scans all HID devices connected to windows and select
    // the first supported DualShock 4 controller found
    internal static class HidScanner
    {
        private const int SONY_VENDOR_ID = 0x054C;

        private static readonly HashSet<int> SupportedProducts =
        [
            0x05C4, // DualShock 4 USB (CUH-ZCT1)
            0x09CC, // DualShock 4 Bluetooth
            0x09CD  // DualShock 4 USB (CUH-ZCT2)
        ];

        private static string SafeGet(Func<string> getter)
        {
            try
            {
                return getter();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static DualShockDevice? FindDualShock()
        {
            foreach (HidDevice device in DeviceList.Local.GetHidDevices())
            {
                // Only Sony devices
                if (device.VendorID != SONY_VENDOR_ID)
                {
                    continue;
                }

                // Only supported DualShock 4 
                if (!SupportedProducts.Contains(device.ProductID))
                {
                    continue;
                }

                return new DualShockDevice
                {
                    Device = device,
                    DevicePath = device.DevicePath,
                    ProductName = SafeGet(device.GetProductName),
                    Manufacturer = SafeGet(device.GetManufacturer),
                    SerialNumber = SafeGet(device.GetSerialNumber)
                };
            }

            return null;
        }
    }
}