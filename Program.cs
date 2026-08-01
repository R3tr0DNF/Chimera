using System;
using System.Diagnostics;
using Chimera.Models;
using Chimera.Services;
using HidSharp;

namespace Chimera
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Chimera - DualShock Analyzer";

            Console.WriteLine("Buscando DualShock...\n");

            DualShockDevice? controller = HidScanner.FindDualShock();

            if (controller == null)
            {
                Console.WriteLine("No se encontró el DualShock.");
                return;
            }

            Console.WriteLine("DualShock encontrado.");
            Console.WriteLine();

            HidStream? stream = DualshockConnection.Open(controller);

            if (stream == null)
            {
                Console.WriteLine("No se pudo abrir el stream.");
                return;
            }

            byte[] report = new byte[64];
            byte[] previousReport = new byte[64];

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Midiendo tiempos...");
            Console.WriteLine("Presiona ESC para salir.\n");

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                        break;
                }

                stopwatch.Restart();

                int bytesRead = stream.Read(report);

                stopwatch.Stop();

                var changes = ChangeDetector.DetectChanges(previousReport, report);

                // Solo mostrar cuando haya cambios reales
                if (changes.Count > 0)
                {
                    Console.Clear();

                    Console.WriteLine($"Tiempo de lectura HID : {stopwatch.Elapsed.TotalMilliseconds:F3} ms");
                    Console.WriteLine($"Bytes leídos          : {bytesRead}");
                    Console.WriteLine($"Cambios detectados    : {changes.Count}");
                    Console.WriteLine();

                    foreach (ByteChange change in changes)
                    {
                        if (ReportFilter.ShouldIgnore(change.Index))
                            continue;

                        Console.WriteLine($"Byte {change.Index:D2}");

                        Console.WriteLine($"Anterior : {change.PreviousValue:X2}");
                        Console.WriteLine($"Actual   : {change.CurrentValue:X2}");
                        Console.WriteLine($"XOR      : {change.Difference:X2}");

                        var bits = BitAnalyzer.GetChangedBits(change.Difference);

                        Console.Write("Bits     : ");

                        foreach (int bit in bits)
                        {
                            Console.Write(bit + " ");
                        }

                        Console.WriteLine();
                        Console.WriteLine();
                    }
                }

                Array.Copy(report, previousReport, bytesRead);
            }

            stream.Close();
        }
    }
}