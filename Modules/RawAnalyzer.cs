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

            HidStream? stream = DualshockConnection.Open(controller);

            if (stream == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to open the controller stream.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Waiting for input... Press esc to exit." );

            byte[] report = new byte[64];
            byte[] previousReport = new byte[64];

            int reportCount = 0;

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }

                int bytesRead = stream.Read(report);

                List<ByteChange> changes = ChangeDetector.DetectChanges(previousReport, report);

                List<ByteChange> visibleChanges = new List<ByteChange>();

                foreach (ByteChange change in changes)
                {
                    if (!ReportFilter.ShouldIgnore(change.Index))
                    {
                        visibleChanges.Add(change);
                    }
                }

                if (visibleChanges.Count == 0)
                {
                    Array.Copy(report, previousReport, report.Length);
                    reportCount++;
                    continue;
                }

                
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Report #{reportCount:D4}");
                Console.ResetColor();

                foreach (ByteChange change in visibleChanges)
                {
                    if (ReportFilter.ShouldIgnore(change.Index))
                    {
                        continue;
                    }

                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine(
                        $"Byte {change.Index:D2}: {change.PreviousValue:X2} -> {change.CurrentValue:X2}");

                    Console.ForegroundColor = ConsoleColor.DarkGray;

                    Console.WriteLine(
                        $"    Before: {Convert.ToString(change.PreviousValue, 2).PadLeft(8, '0')}");

                    Console.WriteLine(
                        $"    After : {Convert.ToString(change.CurrentValue, 2).PadLeft(8, '0')}");

                    Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.WriteLine(
                        $"    XOR   : {Convert.ToString(change.Difference, 2).PadLeft(8, '0')}");

                    List<int> changedBits =
                        BitAnalyzer.GetChangedBits(change.Difference);
                    
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Bits: ");

                    foreach (int bit in changedBits)
                    {
                        Console.Write($"{bit} ");
                        string? button = DualShockMapper.GetButtonName(change.Index, bit);
                        if (button != null)
                        {
                            bool pressed = (change.CurrentValue & (1 << bit)) != 0;
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"Button: {button} ");
                            Console.WriteLine($"State: {(pressed ? "Pressed" : "Released")}");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                        }
                    }

                    Console.ResetColor();
                    Console.WriteLine();
                }
                
                Array.Copy(report, previousReport, report.Length);
                reportCount++;

            }

            stream.Close();
            Console.WriteLine("Exiting Raw Analyzer...");
        }
    }
}