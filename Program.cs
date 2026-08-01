using System;
using System.Collections.Generic;
using Chimera.Modules;


namespace Chimera
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<IModule> modules = new List<IModule>
            {
                new ButtonAnalyzer(),
                new RawAnalyzer(),
                new BenchmarkAnalyzer(),
                new InputAnalyzer()
            };

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Chimera!");

                Console.WriteLine();

                for(int i = 0; i < modules.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {modules[i].Name}");
                }

                Console.WriteLine();
                Console.WriteLine("Select 0 to exit...");
                Console.WriteLine();

                Console.Write("Select a option:");

                string? input = Console.ReadLine();

                if (!int.TryParse(input, out int option))
                {
                    continue;
                }

                if (option == 0)
                {
                    break;
                }

                if (option < 1 || option > modules.Count)
                {
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }

                Console.Clear();

                modules[option - 1].Run();

                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);



            }
        }
    }
}