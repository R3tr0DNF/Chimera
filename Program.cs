using System;
using System.Threading;
using Chimera.Models;
using Chimera.Services;
using HidSharp;

namespace Chimera
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Chimera";

            Console.WriteLine("         Bienvenido a Chimera\n");

            Console.WriteLine("Buscando DualShock...\n");

            DualShockDevice? controller = HidScanner.FindDualShock();

            if (controller == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No se encontró ningún DualShock compatible.");
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

            HidStream? stream = DualshockConnection.Open(controller);

            if (stream == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nNo se pudo abrir el stream del DualShock.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("\nEsperando datos del mando de PS4...");
            Console.WriteLine("Presiona ESC para salir.\n");

            byte[] report = new byte[64];
            byte[] previousReport = new byte[64];

            int reportCount = 0;

            while (true)
            {
                // Salir con ESC
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }

                int bytesRead = stream.Read(report);

                Console.Write($"Report #{reportCount:D4} -> ");

                for (int i = 0; i < bytesRead; i++)
                {
                    if (report[i] != previousReport[i])
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }

                    Console.Write($"{i:D2}:{report[i]:X2} ");

                    previousReport[i] = report[i];
                }

                Console.ResetColor();
                Console.WriteLine();

                reportCount++;

                if (reportCount % 30 == 0)
                {
                    Console.Clear();

                    Console.WriteLine("=================================");
                    Console.WriteLine("      Chimera HID Monitor");
                    Console.WriteLine("=================================\n");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Verde");
                    Console.ResetColor();
                    Console.WriteLine(" = Byte cambiado");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Gris");
                    Console.ResetColor();
                    Console.WriteLine(" = Byte sin cambios\n");
                }

                Thread.Sleep(100);
            }

            stream.Close();

            Console.WriteLine("\nConexión cerrada.");
            Console.WriteLine("Programa finalizado. Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}