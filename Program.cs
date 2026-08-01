using System;
using Chimera.Models;
using Chimera.Services;

namespace Chimera
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Chimera";

            Console.WriteLine("         Bienvenido a Chimera");

            Console.WriteLine("Buscando DualShock...\n");

            DualShockDevice? controller = HidScanner.FindDualShock();

            if (controller == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" No se encontro ningún DualShock compatible.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("DualShock encontrado\n");
            Console.ResetColor();

            Console.WriteLine($"Fabricante : {controller.Manufacturer}");
            Console.WriteLine($"Producto   : {controller.ProductName}");
            Console.WriteLine($"Serie      : {controller.SerialNumber}");
            Console.WriteLine($"Vendor ID  : 0x{controller.Device.VendorID:X4}");
            Console.WriteLine($"Product ID : 0x{controller.Device.ProductID:X4}");

            Console.WriteLine("\nPresiona una tecla para salir...");
            Console.ReadKey();
        }
    }
}