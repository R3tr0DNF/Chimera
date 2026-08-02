using System;
using System.Collections.Generic;
using Chimera.Models;
using Chimera.Modules;
using Chimera.Services.Conection;


namespace Chimera
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<IModule> modules = new List<IModule>
            {
                new RawAnalyzer(),
                new BenchmarkAnalyzer(),
                new InputAnalyzer(),
                new PlayMode(),
                new HidHideTest()
            };

            DualShockManager dualShock = new DualShockManager();

            if (!dualShock.Initialize())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Can't connect to dualshock 4 :c");
                Console.ResetColor();

                return;
            }

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

                modules[option - 1].Run(dualShock);

                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);



            }
        }
    }
}