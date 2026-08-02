using System.Diagnostics;
using System.Resources;
using Chimera.Models;
using Chimera.Services;
using Chimera.Services.Input;
using HidSharp;
using HidSharp.Reports;

namespace Chimera.Modules
{
    internal class BenchmarkAnalyzer : IModule
    {
        public string Name => "Benchmark Analyzer";

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("Searching for DualShock 4... ");

            DualShockDevice? dualShock = HidScanner.FindDualShock();

            if (dualShock == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("DualShock 4 not found :c");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("DualShock 4 found... ");
            Console.ResetColor();

            HidStream? stream = DualshockConnection.Open(dualShock);

            if (stream == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to open controller :c");
                Console.ResetColor();
                return;
            }

            InputMonitor monitor = new (stream);

            Console.WriteLine();
            Console.WriteLine("Benchmark running for 10 seconds ...");
            Console.WriteLine();

            Stopwatch stopwatch = Stopwatch.StartNew();

            int reports = 0;
            int changes = 0;

            while (stopwatch.Elapsed < TimeSpan.FromSeconds(10))
            {
                reports++;

                if (monitor.TryRead())
                {
                    changes++;
                }
            }

            stopwatch.Stop();
            stream.Close();
            
            double seconds = stopwatch.Elapsed.TotalSeconds;
            double hz = reports / seconds;

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Results");
            Console.WriteLine($"Durations: {seconds:F2}");
            Console.WriteLine($"Reports: {reports}");
            Console.WriteLine($"Changes: {changes}");
            Console.WriteLine($"Frequency: {hz:F2} Hz");

            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("Press key to continue ...");
            Console.ReadKey(true);
            
        }
    }
}