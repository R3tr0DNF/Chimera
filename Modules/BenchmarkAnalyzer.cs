namespace Chimera.Modules
{
    internal class BenchmarkAnalyzer : IModule
    {
        public string Name => "Benchmark Analyzer";

        public void Run()
        {
            Console.WriteLine(" the benchmark is running... :)");
            
        }
    }
}