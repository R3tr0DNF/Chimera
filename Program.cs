using System;
using System.Collections.Generic;
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

                // Leer reporte HID
                int bytesRead = stream.Read(report);

                // Detectar cambios
                List<ByteChange> changes = ChangeDetector.DetectChanges(previousReport, report);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"========== Report #{reportCount:D4} ==========");
                Console.ResetColor();

                if (changes.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("Sin cambios.");
                    Console.ResetColor();
                }
                else
                {
                    foreach (ByteChange change in changes)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;

                        Console.WriteLine(
                            $"Byte {change.Index:D2}: {change.PreviousValue:X2} -> {change.CurrentValue:X2}");

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine(
                            $"    Antes : {Convert.ToString(change.PreviousValue, 2).PadLeft(8, '0')}");

                        Console.WriteLine(
                            $"    Ahora : {Convert.ToString(change.CurrentValue, 2).PadLeft(8, '0')}");

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(
                            $"    XOR   : {Convert.ToString(change.Difference, 2).PadLeft(8, '0')}");

                        List<int> changedBits = BitAnalyzer.GetChangedBits(change.Difference);
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("Bits: ");
                        
                        foreach (int bit in changedBits)
                        {
                            Console.Write($"{bit} ");
                        }

                        Console.WriteLine();
                        
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                }

                // Guardar el reporte actual
                for (int i = 0; i < bytesRead; i++)
                {
                    previousReport[i] = report[i];
                }

                reportCount++;

                Thread.Sleep(100);
            }

            stream.Close();

            Console.WriteLine("\nConexión cerrada.");
            Console.WriteLine("Programa finalizado. Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}