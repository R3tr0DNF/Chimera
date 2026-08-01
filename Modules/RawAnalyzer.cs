namespace Chimera.Modules
{
    internal class RawAnalyzer : IModule
    {
        public string Name => "Raw Analyzer";

        public void Run()
        {
            Console.WriteLine(" the raw analyzer is running... :)");
            
        }
    }
}