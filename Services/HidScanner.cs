using System;
using HidSharp;
using Chimera.Models;

namespace Chimera.Services
{
    internal static class HidScanner
    {
        private const int SONY_VENDOR_ID = 0x054C;

        private static readonly int[] SupportedProducts =
        {
            0x09CC, // DualShock 4 V1
            0x09CD  // DualShock 4 V2
        };

        public static DualShockDevice? FindDualShock()
        {
            foreach (var device in DeviceList.Local.GetHidDevices())
            {
                // es mando de ps4?
                if (device.VendorID != SONY_VENDOR_ID)
                {
                    continue;
                }

                bool supported = false;

                foreach (int productId in SupportedProducts)
                {
                    if (productId == device.ProductID)
                    {
                        supported = true;
                        break;
                    }
                }

                if (!supported)
                {
                    continue;
                }

                try
                {
                    return new DualShockDevice
                    {
                        Device = device,
                        ProductName = device.GetProductName(),
                        Manufacturer = device.GetManufacturer(),
                        SerialNumber = device.GetSerialNumber()
                    };
                }
                catch
                {

                    continue;
                }
            }
            return null;
        }
    }
}