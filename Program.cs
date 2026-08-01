using System;


namespace Chimera
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Chimera";
            Console.WriteLine("Bienvenido a Chimera");
            Console.WriteLine("iniciando...");

            Services.HidScanner.Scan();
            
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();

        }
    }
}