using System;
using System.Collections.Generic;
using System.Threading;
using Chimera.Models;
using Chimera.Services;
using HidSharp;

namespace Chimera.Modules
{
    internal class RawAnalyzer : IModule
    {
        public string Name => "Raw Analyzer";

        public void Run()
        {
            Console.Clear();

            Console.WriteLine("Searching for DualShock 4 controllers...");

            DualShockDevice? controller = HidScanner.FindDualShock();

            if(controller == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No DualShock 4 controller found. Make sure it's connect to bluethooth");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("DualShock 4 controller found!");
            Console.ResetColor();

            Console.WriteLine($"Publisher: {controller.Manufacturer}");
            Console.WriteLine($"Product: {controller.ProductName}");
            Console.WriteLine($"Serial Number: {controller.SerialNumber}");
            Console.WriteLine($"Vendor ID: 0x{controller.Device.VendorID}");
            Console.WriteLine($"Product ID: 0x{controller.Device.ProductID}");

            
            
        }
    }
}