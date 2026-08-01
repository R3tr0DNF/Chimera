using System;
using HidSharp;

namespace Chimera.Services
{
    internal static class HidScanner
    {

        public static void Scan()
        {
            Console.WriteLine("Escaneando dispositivos HID...");

            foreach (var device in DeviceList.Local.GetHidDevices())
            {
                Console.WriteLine($"Dispositivo encontrado: {device.DevicePath}");
                Console.WriteLine($"  Vendor ID: {device.VendorID}");
                Console.WriteLine($"  Product ID: {device.ProductID}");
                Console.WriteLine($"  Manufacturer: {device.GetManufacturer()}");
                Console.WriteLine($"  Product Name: {device.GetProductName()}");
                Console.WriteLine($"  Serial Number: {device.GetSerialNumber()}");
                Console.WriteLine();
            }
        }
    }
}