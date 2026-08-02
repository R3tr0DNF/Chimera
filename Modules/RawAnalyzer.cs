using Chimera.Models;
using Chimera.Services;
using Chimera.Services.Conection;
using HidSharp;

namespace Chimera.Modules
{
    internal class RawAnalyzer : IModule
    {
        public string Name => "Raw Analyzer";

        public void Run(DualShockManager dualShock)
        {
            Console.Clear();

            DualShockDevice controller = dualShock.Device!;
            HidStream stream = dualShock.Stream!;

            Console.WriteLine();
            Console.WriteLine("Waiting for input... Press ESC to exit.");
            Console.WriteLine();

            byte[] report = new byte[64];
            byte[] previousReport = new byte[64];

            int reportCount = 0;

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Escape)
                        break;
                }

                stream.Read(report);

                List<ByteChange> changes =
                    ChangeDetector.DetectChanges(previousReport, report);

                List<ByteChange> visibleChanges = new();

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
                Console.WriteLine($"========== Report #{reportCount:D4} ==========");
                Console.ResetColor();



                if (StickAnalyzer.LeftStickChanged(previousReport, report))
                {
                    StickPosition left =
                        StickAnalyzer.GetLeftStick(report);

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Left Stick");
                    Console.WriteLine($"    X : {left.X}");
                    Console.WriteLine($"    Y : {left.Y}");
                    Console.ResetColor();
                    Console.WriteLine();
                }

                if (StickAnalyzer.RightStickChanged(previousReport, report))
                {
                    StickPosition right =
                        StickAnalyzer.GetRightStick(report);

                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Right Stick");
                    Console.WriteLine($"    X : {right.X}");
                    Console.WriteLine($"    Y : {right.Y}");
                    Console.ResetColor();
                    Console.WriteLine();
                }

                foreach (ByteChange change in visibleChanges)
                {
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

                    Console.ResetColor();

                    // ===========================
                    // DPad
                    // ===========================

                    if (change.Index == 5 &&
                        DPadAnalyzer.HasChanged(change.PreviousValue, change.CurrentValue))
                    {
                        DPadDirection direction =
                            DPadAnalyzer.GetDirection(change.CurrentValue);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"    DPad : {direction}");
                        Console.ResetColor();
                    }

                    // ===========================
                    // Buttons
                    // ===========================

                    List<int> changedBits =
                        BitAnalyzer.GetChangedBits(change.Difference);

                    foreach (int bit in changedBits)
                    {
                        // Los bits 0-3 del Byte 05 pertenecen al DPad
                        if (change.Index == 5 && bit < 4)
                            continue;

                        string? button =
                            DualShockMapper.GetButtonName(change.Index, bit);

                        if (button == null)
                            continue;

                        bool pressed =
                            (change.CurrentValue & (1 << bit)) != 0;

                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"    Button : {button}");

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"    State  : {(pressed ? "Pressed" : "Released")}");

                        Console.ResetColor();
                    }

                    Console.WriteLine();
                }

                Array.Copy(report, previousReport, report.Length);
                reportCount++;
            }

            stream.Close();

            Console.WriteLine();
            Console.WriteLine("Raw Analyzer closed.");
        }
    }
}