using System.Diagnostics;
using Chimera.Services.Conection;
using Chimera.Services.Input;


namespace Chimera.Modules
{
    internal class BenchmarkAnalyzer : IModule
    {
        public string Name => "Benchmark Analyzer";

        public void Run(DualShockManager dualShock)
        {
            Console.Clear();

            InputMonitor monitor = dualShock.Monitor!;

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